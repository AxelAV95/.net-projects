using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ventas.Infrastructure.Persistences.Contexts;
using Ventas.Infrastructure.Persistences.Interfaces;
using Ventas.Infrastructure.Persistences.Repositories;

namespace Ventas.Infrastructure.Extensions
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddInjectionInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = typeof(PosContext).Assembly.FullName;
            services.AddDbContext<PosContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("VentasConnection"), b => b.MigrationsAssembly(assembly)), ServiceLifetime.Transient);
            //Se establece como ciclo de vida de la transacción
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }

}
