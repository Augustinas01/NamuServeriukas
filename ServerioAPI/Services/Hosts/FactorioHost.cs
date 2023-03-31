namespace ServerioAPI.Services.Hosts
{
    public class FactorioHost : BackgroundService
    {
        private readonly ILogger<FactorioHost> _logger;
        public IServiceProvider Services { get; }

        public FactorioHost(IServiceProvider services, ILogger<FactorioHost> logger) 
        {
            _logger = logger;
            Services = services;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation( "Consume Scoped Service Hosted Service running." );

            await DoWork(stoppingToken);
            _logger.LogInformation("Po await Dowork Execute");
        }
        private async Task DoWork(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                "Consume Scoped Service Hosted Service is working.");

            using (var scope = Services.CreateScope())
            {
                var scopedProcessingService =
                    scope.ServiceProvider
                        .GetRequiredService<ProcessService>();

                await scopedProcessingService.Start();
            }
        }
        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                "Consume Scoped Service Hosted Service is stopping.");

            await base.StopAsync(stoppingToken);
        }
    }
}
