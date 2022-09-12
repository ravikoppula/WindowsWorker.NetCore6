using WorkerService1.Models;

namespace WorkerService1
{
    public sealed class WindowsBackgroundService : BackgroundService
    {
        private readonly JokeService _jokeService;
        private readonly ILogger<WindowsBackgroundService> _logger;
        private readonly EmployeeContext _db;

        public WindowsBackgroundService( JokeService jokeService,
                                        ILogger<WindowsBackgroundService> logger, 
                                        IServiceScopeFactory factory)
        {
            _jokeService = jokeService;
            _logger = logger; 
            _db = factory.CreateScope().ServiceProvider.GetRequiredService<EmployeeContext>();
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                    await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);

                    string joke = _jokeService.GetJoke();
                    _logger.LogWarning("{Joke}", joke);

                    tblEmployee item = new tblEmployee();
                    //item.EmployeeID = 7;
                    item.EmployeeName = "test";
                    item.PhoneNumber = "01128102433";
                    item.SkillID = 5;
                    item.YearsExperience = 8;

                    _db.tblEmployees.Add(item);
                    await _db.SaveChangesAsync();

                    await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Message}", ex.Message);

                // Terminates this process and returns an exit code to the operating system.
                // This is required to avoid the 'BackgroundServiceExceptionBehavior', which
                // performs one of two scenarios:
                // 1. When set to "Ignore": will do nothing at all, errors cause zombie services.
                // 2. When set to "StopHost": will cleanly stop the host, and log errors.
                //
                // In order for the Windows Service Management system to leverage configured
                // recovery options, we need to terminate the process with a non-zero exit code.
                Environment.Exit(1);

            }
            
        }
    }
}