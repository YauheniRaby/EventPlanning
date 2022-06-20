using AutoMapper;
using EventPlanning.AutoMap;
using EventPlanning.Bl.Services;
using EventPlanning.Bl.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace EventPlanning.Extensions
{
    public static class ServicesRegistration
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IEmailService, EmailService>();
            services.AddSingleton<ICaseService, CaseService>();
            services.AddSingleton<IParticipationService, ParticipationService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<ISmsService, SmsService>(); 
            services.AddSingleton<IMapper>(service => new Mapper(MapperConfig.GetConfiguration()));

            services.AddHttpClient<ISmsService, SmsService>();
        }
    }
}
