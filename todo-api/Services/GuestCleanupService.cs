using Microsoft.EntityFrameworkCore;
using todo_api.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace todo_api.Services
{
    public class GuestCleanupService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<GuestCleanupService> _logger;

        public GuestCleanupService(
            IServiceScopeFactory scopeFactory,
            ILogger<GuestCleanupService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("GuestCleanupService started");

            // Run immediately on startup
            await Cleanup(stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
                await Cleanup(stoppingToken);
            }
        }

        private async Task Cleanup(CancellationToken token)
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<TodoContext>();

                var cutoff = DateTime.UtcNow.AddHours(-12);

                var guests = await context.Users
                    .Where(u => u.Role == "Guest" && u.CreatedAt < cutoff)
                    .ToListAsync(token);

                if (guests.Any())
                {
                    context.Users.RemoveRange(guests);
                    await context.SaveChangesAsync(token);

                    _logger.LogInformation(
                        "Deleted {Count} guest users older than 24h",
                        guests.Count);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Guest cleanup failed");
            }
        }
    }
}
