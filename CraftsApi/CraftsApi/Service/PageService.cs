using System.Collections.Generic;
using System.Threading.Tasks;
using CraftsApi.Application;
using CraftsApi.Service.Mappings;
using CraftsApi.Service.Requests;

namespace CraftsApi.Service
{
    public class PageService : IPageService
    {
        private readonly IPageApplication _pageApplication;

        public PageService(IPageApplication pageApplication)
        {
            _pageApplication = pageApplication;
        }

        public async Task<List<ViewModels.Page>> GetPagesAsync()
        {
            List<DomainModels.Page> pages = await _pageApplication.GetPagesAsync();
            return pages.MapListOfDomainPagesToListOfViewPages();
        }

        public async Task<ViewModels.Page> GetPageAsync(int pageId)
        {
            DomainModels.Page page = await _pageApplication.GetPageAsync(pageId);
            return page.MapDomainPageToViewPage();
        }

        public async Task<ViewModels.Page> GetPageByLinkAsync(string pageLink)
        {
            DomainModels.Page page = await _pageApplication.GetPageByLinkAsync(pageLink);
            return page.MapDomainPageToViewPage();
        }

        public async Task<ViewModels.Page> GetDefaultPageAsync()
        {
            DomainModels.Page page = await _pageApplication.GetDefaultPageAsync();
            return page.MapDomainPageToViewPage();
        }

        public async Task<bool> AddPageAsync(AddPageRequest addPageReqest)
        {
            DomainModels.Page page = addPageReqest.MapAddPageRequestToPage();
            var added = await _pageApplication.AddPageAsync(page);
            return added;
        }

        public async Task<bool> UpdatePageAsync(UpdatePageRequest updatePageRequest)
        {
            var page = updatePageRequest.MapUpdatePageRequestToPage();
            var updated = await _pageApplication.UpdatePageAsync(page);
            return updated;
        }

        public async Task<bool> DeletePageAsync(int pageId)
        {
            bool deleted = await _pageApplication.DeletePageAsync(pageId);
            return deleted;
        }
    }
}
