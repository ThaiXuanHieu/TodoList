using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NCrontab;

namespace TodoList.Api.Services
{
    public class TaskHostedService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private CrontabSchedule _schedule;
        private DateTime _nextRun;

        private string Schedule = "*/10 * * * * *"; //Runs every 10 seconds

        public TaskHostedService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
            _schedule = CrontabSchedule.Parse(Schedule, new CrontabSchedule.ParseOptions { IncludingSeconds = true });
            _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            do
            {
                var now = DateTime.Now;
                var nextrun = _schedule.GetNextOccurrence(now);
                if (now > _nextRun)
                {
                    await GetTask();
                    _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
                }
                await Task.Delay(1000, stoppingToken); 
            }
            while (!stoppingToken.IsCancellationRequested);
        }

        private void Process()
        {
            Console.WriteLine(DateTime.Now.ToString("F"));
        }

        public async Task GetTask()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<TodoListDbContext>();

                System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("vi-VN");
                var task = await _context.Tasks
                .Where(x => x.DueDate >= DateTime.Now)
                .OrderBy(x => x.DueDate)
                .FirstOrDefaultAsync();
                // var cronItem = Convert.ToDateTime(task.DueDate).ToString("yyyy/MM/dd HH:mm:ss").Split('/', ':', ' ');
                // string dayOfWeek = Convert.ToDateTime(task.DueDate).DayOfWeek.ToString("d");
                // string cronEx = cronItem[5] + " " + cronItem[4] + " " + cronItem[3] + " " + cronItem[2] + " " + cronItem[1] + " " + dayOfWeek;
                // Console.WriteLine("Task: " + Convert.ToDateTime(task.DueDate).ToString("MM/dd/yyyy HH:mm"));
                // Console.WriteLine("Now: " + DateTime.Now.ToString("MM/dd/yyyy HH:mm"));
                // if (Convert.ToDateTime(task.DueDate).ToString("MM/dd/yyyy HH:mm").Equals(DateTime.Now.ToString("MM/dd/yyyy HH:mm")))
                // {
                //     Console.WriteLine("Bằng nhau rồi");
                // }
            }
        }
    }
}
