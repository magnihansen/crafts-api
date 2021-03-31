using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace CraftsApi.Service.Hubs
{
    public class PageHub : Hub
    {
        private readonly ILogger<PageHub> _logger;
        private readonly IPageService _pageService;

        public PageHub(
            IPageService pageService,
            ILogger<PageHub> logger
        )
        {
            _pageService = pageService;
            _logger = logger;
        }

        public async Task GetPages()
        {
            _logger.LogInformation($"Page hub runned on: {DateTime.Now}");
            await Clients.All.SendAsync("pagesReceived", await _pageService.GetPagesAsync());
        }
    }
}
