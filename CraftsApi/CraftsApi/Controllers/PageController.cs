﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CraftsApi.Service;
using CraftsApi.Service.Hubs;
using CraftsApi.Service.Requests;
using CraftsApi.Service.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CraftsApi.Controllers
{
    public class PageController : BaseController
    {
        private readonly IPageService _pageService;
        private readonly IHubContext<PageHub> _pageHubContext;

        public PageController(
            IPageService pageService,
            IHubContext<PageHub> pageHubContext)
        {
            _pageService = pageService;
            _pageHubContext = pageHubContext;
        }

        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Page>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPages()
        {
            var pages = await _pageService.GetPagesAsync();
            return new OkObjectResult(pages.ToList());
        }

        [HttpGet("[action]/{pageId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Page))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPage(int pageId)
        {
            var page = await _pageService.GetPageAsync(pageId);
            return new OkObjectResult(page);
        }

        [HttpGet("[action]/{pageLink}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Page))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPageByLink(string pageLink)
        {
            var page = await _pageService.GetPageByLinkAsync(pageLink);
            return new OkObjectResult(page);
        }

        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Page))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDefaultPage()
        {
            var page = await _pageService.GetDefaultPageAsync();
            return new OkObjectResult(page);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddPage(AddPageRequest addPageRequest)
        {
            bool pageAdded = await _pageService.AddPageAsync(addPageRequest);
            await _pageHubContext.Clients.All.SendAsync("pagesReceived", _pageService.GetPagesAsync());
            return new OkObjectResult(pageAdded);
        }

        [HttpPut("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdatePage(UpdatePageRequest updatePageRequest)
        {
            bool pageUpdated = await _pageService.UpdatePageAsync(updatePageRequest);
            return new OkObjectResult(pageUpdated);
        }

        [HttpDelete("[action]/{pageId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePage(int pageId)
        {
            bool pageDeleted = await _pageService.DeletePageAsync(pageId);
            return new OkObjectResult(pageDeleted);
        }
    }
}
 