using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace TodoList.Api.Services
{
    public class TaskCronJob : CronJobService
    {
        private readonly ILogger<TaskCronJob> _logger;

        public TaskCronJob(IScheduleConfig<TaskCronJob> config, ILogger<TaskCronJob> logger)
            : base(config.CronExpression, config.TimeZoneInfo)
        {
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Task starts.");
            return base.StartAsync(cancellationToken);
        }

        public override Task DoWork(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{DateTime.Now:hh:mm:ss} Task is working.");
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Task is stopping.");
            return base.StopAsync(cancellationToken);
        }
    }
}