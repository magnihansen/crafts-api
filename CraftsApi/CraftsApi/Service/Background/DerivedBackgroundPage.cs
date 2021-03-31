using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace CraftsApi.Service.Background
{
    public class DerivedBackgroundPage : BackgroundService
    {
        private readonly IPageWorker worker;

        public DerivedBackgroundPage(IPageWorker worker)
        {
            this.worker = worker;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await worker.DoWork(stoppingToken);
        }
    }
}
