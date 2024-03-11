using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _253502.Persistence.Repository;
using _253502.Persistence.Data;
using _253502.Garnik.ViewModels;
using Microsoft.EntityFrameworkCore;
using _253502.Domain.Abstractions;
using _253502.Garnik.Pages;
using Microsoft.Extensions.DependencyInjection;
using _253502.Application.Queries;
using CommunityToolkit.Maui;


namespace _253502.Garnik
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection RegisterPages(this IServiceCollection services)
        {
            services.AddSingleton<AuthorsPage>();
            services.AddTransient<BooksPage>();
            services.AddTransient<AddOrUpdateAuthor>();
            services.AddTransient<AddOrUpdateBook>();
            return services;
        }
        public static IServiceCollection RegisterViewModels(this IServiceCollection services)
        {
            services.AddSingleton<AuthorViewModel>().AddTransient<BookViewModel>()
                .AddTransient<AddOrUpdateBookViewModel>().AddTransient<AddOrUpdateAuthorViewModel>();
            return services;
        }
    }
}
