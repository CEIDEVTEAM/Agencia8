// See https://aka.ms/new-console-template for more information
using ETLProcess.Configuration;
using ETLProcess.Services;
using ETLProcess.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;


var logger = LogManager.GetCurrentClassLogger();

try
{
    logger.Debug("Inicio de proceso ETL");

    IHost host = Host.CreateDefaultBuilder(args)
        .ConfigureServices((hostContext, services) =>
        {
            services.Configure<DatabaseSettings>(hostContext.Configuration.GetSection(DatabaseSettings.Position));
            services.Configure<Documents>(hostContext.Configuration.GetSection(Documents.Position));
            services.AddScoped<IDapper, DapperService>();
            services.AddScoped<IStartProcess, StartProcess>();

            services.AddLogging(builder =>
            {
                builder.SetMinimumLevel(LogLevel.Trace);
                builder.AddNLog("Nlog.config");
            });

        })
        .Build();

    IStartProcess service = host.Services.GetService<IStartProcess>();
    service.Start();
}
catch (Exception ex)
{
    logger.Error(ex, "Error inesperado. Fin del proceso");
    LogManager.Shutdown();
}