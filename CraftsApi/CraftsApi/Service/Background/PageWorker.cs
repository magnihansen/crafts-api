using System.Threading;
using System.Threading.Tasks;
using CraftsApi.Service.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace CraftsApi.Service.Background
{
    public class PageWorker : IPageWorker
    {
        private readonly ILogger<PageWorker> _logger;
        private readonly IPageService _pageService;
        private readonly IHubContext<PageHub> _hubContext;
        private int number = 0;

        public PageWorker(
            ILogger<PageWorker> logger,
            IPageService pageService,
            IHubContext<PageHub> hubContext)
        {
            _logger = logger;
            _pageService = pageService;
            _hubContext = hubContext;
        }

        public async Task DoWork(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                Interlocked.Increment(ref number);
                _logger.LogInformation($"Worker printing number {number}");

                await _hubContext.Clients.All.SendAsync("pagesReceived", await _pageService.GetPagesAsync());

                await Task.Delay(1000 * 5, cancellationToken);
            }
        }
    }
}
