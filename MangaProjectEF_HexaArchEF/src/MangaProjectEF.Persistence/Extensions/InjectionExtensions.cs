using MangaProjectEF.Interface;
using MangaProjectEF.Persistence.Context;
using MangaProjectEF.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace MangaProjectEF.Persistence.Extensions
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddInjectionPersistence(this IServiceCollection services)
        {
            services.AddSingleton<BdmangaContext>();
            services.AddScoped<IMangaRepository, MangaRepository>();
            return services;
        }
    }
}
