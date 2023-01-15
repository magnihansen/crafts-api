using System.Collections.Generic;
using System.Threading.Tasks;
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
    public class PageTypeController : BaseController
    {
        private readonly IPageTypeService _pageTypeService;

        public PageTypeController(
            IPageTypeService pageTypeService)
        {
            _pageTypeService = pageTypeService;
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PageTypeVM>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPageTypes()
        {
            List<PageTypeVM> pageTypes = await _pageTypeService.GetPageTypesAsync();
            return Ok(pageTypes);
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PageTypeVM))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPageType(int pageTypeId)
        {
            PageTypeVM page = await _pageTypeService.GetPageTypeAsync(pageTypeId);
            return Ok(page);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> AddPageType(AddPageTypeRequest addPageTypeRequest)
        {
            DomainModels.PageType pageType = addPageTypeRequest.MapAddPageTypeRequestToPageType();
            bool pageTypeAdded = await _pageTypeService.InsertPageTypeAsync(pageType);
            return Ok(pageTypeAdded);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> UpdatePageType(UpdatePageTypeRequest updatePageTypeRequest)
        {
            DomainModels.PageType pageType = updatePageTypeRequest.MapUpdatePageTypeRequestToPageType();
            bool pageTypeUpdated = await _pageTypeService.UpdatePageTypeAsync(pageType);
            return Ok(pageTypeUpdated);
        }

        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> DeletePageType(int pageTypeId)
        {
            bool pageTypeDeleted = await _pageTypeService.DeletePageTypeAsync(pageTypeId);
            return Ok(pageTypeDeleted);
        }
    }
}
 