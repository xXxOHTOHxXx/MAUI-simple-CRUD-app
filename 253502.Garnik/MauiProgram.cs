using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using _253502.Application;
using _253502.Garnik;
using _253502.Persistence;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using _253502.Persistence.Data;
using _253502.Application.DI;

namespace _253502.Garnik
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            string settingsStream = "_253502.Garnik.appsettings.json";

            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });


            var a = Assembly.GetExecutingAssembly();
            using var stream = a.GetManifestResourceStream(settingsStream);
            builder.Configuration.AddJsonStream(stream);
            var connStr = builder.Configuration.GetConnectionString("SqliteConnection");
            string dataDirectory = FileSystem.Current.AppDataDirectory + "/";
            connStr = String.Format(connStr, dataDirectory);
            var options = new DbContextOptionsBuilder<AppDbContext>().UseSqlite(connStr).Options;


            try
            {
                builder.Services.AddApplication();
                builder.Services.AddPersistence(options);//
                builder.Services.RegisterPages();
                builder.Services.RegisterViewModels();
            }
            catch (Exception ex)
            {
                Console.WriteLine("DI threw "+ex.ToString());
            }

            DbInitializer.Initialize(builder.Services.BuildServiceProvider()).Wait();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
