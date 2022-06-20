using EventPlanning.Bl.Configuration;
using EventPlanning.DA.Configuration;
using EventPlanning.Extensions;
using FluentValidation.AspNetCore;
using Hangfire;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace EventPlanning
{
    public class Startup
    {
        public static HttpMessageHandler Handler { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            services.AddDbContext<RepositoryContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Singleton);

            services.Configure<AppMessages>(Configuration.GetSection(nameof(AppMessages)));
            services.Configure<MessageTemplates>(Configuration.GetSection(nameof(MessageTemplates)));
            services.Configure<MailSettings>(Configuration.GetSection(nameof(MailSettings)));
            services.Configure<AppConfiguration>(Configuration.GetSection(nameof(AppConfiguration)));
            services.Configure<SmsApiConfiguration>(Configuration.GetSection(nameof(SmsApiConfiguration)));
            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

            services.AddRepositories();
            services.AddServices();
            services.AddValidators();
            services.AddControllers();

            services.AddHangfire(opt => opt.UseSqlServerStorage(connectionString));
            services.AddHangfireServer();            

            var authority = Configuration["IdentityUrl"];
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EventPlanningAPI", Version = "v1" });
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Password = new OpenApiOAuthFlow
                        {
                            TokenUrl = new Uri($"{authority}/connect/token"),
                            Scopes = new Dictionary<string, string>
                            {
                                { "eventPlanning_api", "EventPlanningAPI" },
                            },
                        },
                    }
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "oauth2",
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    },
                });
            });
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
            .AddIdentityServerAuthentication(options =>
            {
                options.Authority = authority;
                options.ApiName = "eventPlanning_api";
                options.JwtBackChannelHandler = Handler;
            });
            services.AddAuthorization();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "EventPlanningAPI v1");
                    c.OAuthAppName("Swagger API");
                    c.OAuthClientId("swagger_api");
                });

                app.UseHangfireDashboard("/dashboard");
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
