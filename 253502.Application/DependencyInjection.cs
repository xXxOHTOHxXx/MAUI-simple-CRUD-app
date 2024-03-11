using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _253502.Persistence.Repository;
using _253502.Persistence.Data;
using _253502.Domain.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using _253502.Application.Queries;
using Microsoft.EntityFrameworkCore;


namespace _253502.Application.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this
        IServiceCollection services)
        {
            services.AddMediatR(conf =>
            conf.RegisterServicesFromAssembly(typeof(DependencyInjection)
            .Assembly));
            return services;
        }
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddSingleton<IUnitOfWork, EfUnitOfWork>();
            return services;
        }

        public static IServiceCollection AddPersistence(this IServiceCollection services, DbContextOptions options)
        {
            services.AddPersistence().AddSingleton<AppDbContext>(new AppDbContext((DbContextOptions<AppDbContext>)options));
            return services;
        }
    }
}
