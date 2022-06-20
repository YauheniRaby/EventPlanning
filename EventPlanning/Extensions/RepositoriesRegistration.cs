using EventPlanning.DA.Repositories;
using EventPlanning.DA.Repositories.Abstract;
using Microsoft.Extensions.DependencyInjection;

namespace EventPlanning.Extensions
{
    public static class RepositoriesRegistration
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddSingleton<ICaseRepository, CaseRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IParticipationRepository, ParticipationRepository>();            
        }
    }
}
