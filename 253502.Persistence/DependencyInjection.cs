using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _253502.Persistence.Repository;
using _253502.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace _253502.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddSingleton<IUnitOfWork, FakeUnitOfWork>();
            return services;
        }
        public static IServiceCollection AddPersistence(this IServiceCollection services, DbContextOptions options)
        {
            services.AddPersistence()
            .AddSingleton<AppDbContext>(new AppDbContext((DbContextOptions<AppDbContext>)options));
            return services;
        }
    }
}
