using MangaProject.Application.Interface.Interfaces;
using MangaProject.Persistence.Context;
using MangaProject.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace MangaProject.Persistence.Extensions
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddInjectionPersistence(this IServiceCollection services)
        {
            services.AddSingleton<ApplicationDbContext>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IMangaRepository, MangaRepository>();
            return services;
        }
    }
}
