using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CraftsApi.DomainModels;
using CraftsApi.Helpers;
using CraftsApi.Service;
using CraftsApi.Service.Hubs;
using CraftsApi.Controllers.V1.Requests;
using CraftsApi.Service.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using CraftsApi.Service.Mappings;

namespace CraftsApi.Controllers.V1
{
    [Version(1)]
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

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PageVM>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPages()
        {
            List<PageVM> pages = await _pageService.GetPagesAsync(Request.GetAddressHost());
            return Ok(pages);
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PageVM))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPage(int pageId)
        {
            PageVM page = await _pageService.GetPageAsync(Request.GetAddressHost(), pageId);
            return Ok(page);
        }

        [AllowAnonymous]
        [HttpGet("{pageLink}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PageVM))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPageByLink(string pageLink)
        {
            PageVM page = await _pageService.GetPageByLinkAsync(Request.GetAddressHost(), pageLink);
            return Ok(page);
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PageVM))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDefaultPage()
        {
            PageVM page = await _pageService.GetDefaultPageAsync(Request.GetAddressHost());
            return Ok(page);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PageVM))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddPage(AddPageRequest addPageRequest)
        {
            DomainModels.Page page = addPageRequest.MapAddPageRequestToPage();
            PageVM insertedPage = await _pageService.InsertPageAsync(Request.GetAddressHost(), page);
            await _pageHubContext.Clients.All.SendAsync(
                "pagesReceived",
                await _pageService.GetPagesAsync(Request.GetAddressHost())
            );
            return Ok(insertedPage);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> UpdatePage(UpdatePageRequest updatePageRequest)
        {
            DomainModels.Page page = updatePageRequest.MapUpdatePageRequestToPage();
            bool pageUpdated = await _pageService.UpdatePageAsync(Request.GetAddressHost(), page);
            return Ok(pageUpdated);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> DeletePage(int pageId)
        {
            bool pageDeleted = await _pageService.DeletePageAsync(Request.GetAddressHost(), pageId);
            return Ok(pageDeleted);
        }
    }
}
 