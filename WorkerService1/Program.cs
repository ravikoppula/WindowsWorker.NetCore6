using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Logging.EventLog;
using System;
using WorkerService1;
using WorkerService1.Models;



IHost host = Host.CreateDefaultBuilder(args)

    .UseWindowsService(options =>
        {
        options.ServiceName = "e Service";
        })
        

    .ConfigureServices((hostContext, services) =>
        {

        LoggerProviderOptions.RegisterProviderOptions<EventLogSettings,
        EventLogLoggerProvider>(services);

        services.AddSingleton<JokeService>(); 
        services.AddHostedService<WindowsBackgroundService>();

            //Entity Framework DB connection 
            services.AddDbContext<EmployeeContext>(
            options => options.UseSqlServer("Server=(localdb)\\MSSqlLocalDb;Database=InitialTestDB;Trusted_Connection=True;"));

            services.AddTransient<EmplyeeService>();

        }) 

    .ConfigureLogging((context, logging) =>
        {
        // See: https://github.com/dotnet/runtime/issues/47303
        logging.AddConfiguration(
        context.Configuration.GetSection("Logging"));
        })

    .Build();
 

await host.RunAsync();
