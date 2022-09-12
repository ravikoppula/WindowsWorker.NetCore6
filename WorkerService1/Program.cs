using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Logging.EventLog;
using WorkerService1;
using WorkerService1.Models;

IHost host = Host.CreateDefaultBuilder(args)

    .UseWindowsService(options =>
        {
        options.ServiceName = ".NET Joke Service";
        }) 
    .ConfigureServices((ctx, services) =>
        {

        LoggerProviderOptions.RegisterProviderOptions<EventLogSettings,
        EventLogLoggerProvider>(services);

        services.AddSingleton<JokeService>();
        services.AddHostedService<WindowsBackgroundService>();

            //Entity Framework DB connection  
            //services.AddDbContext<EmployeeContext>(
            //options => options.UseSqlServer(ctx.Configuration.GetConnectionString("InitialDBConn")));
            services.AddDbContext<EmployeeContext>(options => options.UseSqlServer("Server=(localdb)\\MSSqlLocalDb;Database=InitialTestDB;Trusted_Connection=True;"));

        })
    .ConfigureLogging((context, logging) =>
        {
        // See: https://github.com/dotnet/runtime/issues/47303
        logging.AddConfiguration(
        context.Configuration.GetSection("Logging"));
        })

    .Build();

await host.RunAsync();
