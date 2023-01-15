using System.Collections.Generic;
using System.Threading.Tasks;
using CraftsApi.Repository;
using CraftsApi.Service.Mappings;
using CraftsApi.Controllers.V1.Requests;

namespace CraftsApi.Service
{
    public class PageService : IPageService
    {
        private readonly IPageRepository _pageRepository;
        private readonly IDomainRepository _domainRepository;

        public PageService(IPageRepository pageApplication, IDomainRepository domainRepository)
        {
            _pageRepository = pageApplication;
            _domainRepository = domainRepository;
        }

        public async Task<List<ViewModels.PageVM>> GetPagesAsync(string host)
        {
            List<DomainModels.Page> pages = await _pageRepository.GetPagesAsync(host);
            return pages.MapPageToPageVM();
        }

        public async Task<ViewModels.PageVM> GetPageAsync(string host, int pageId)
        {
            DomainModels.Page page = await _pageRepository.GetPageAsync(host, pageId);
            return page.MapPageToPageVM();
        }

        public async Task<ViewModels.PageVM> GetPageByLinkAsync(string host, string pageLink)
        {
            DomainModels.Page page = await _pageRepository.GetPageByLinkAsync(host, pageLink);
            return page.MapPageToPageVM();
        }

        public async Task<ViewModels.PageVM> GetDefaultPageAsync(string host)
        {
            DomainModels.Page page = await _pageRepository.GetDefaultPageAsync(host);
            return page.MapPageToPageVM();
        }

        public async Task<ViewModels.PageVM> InsertPageAsync(string host, DomainModels.Page page)
        {
            DomainModels.Domain domain = await _domainRepository.GetDomainAsync(host);
            DomainModels.Page insertedPage = await _pageRepository.InsertPageAsync(
                host: host,
                domainId: domain.Id,
                uid: page.Uid,
                parentId: page.ParentId,
                pageTypeId: page.PageTypeId,
                title: page.Title,
                content: page.Content,
                sort: page.Sort,
                link: page.Link,
                isRouterLink: page.IsRouterLink,
                active: page.Active,
                createdBy: page.CreatedBy
            );
            return insertedPage.MapPageToPageVM();
        }

        public async Task<bool> UpdatePageAsync(string host, DomainModels.Page page)
        {
            bool updated = await _pageRepository.UpdatePageAsync(host, page);
            return updated;
        }

        public async Task<bool> DeletePageAsync(string host, int pageId)
        {
            bool deleted = await _pageRepository.DeletePageAsync(host, pageId);
            return deleted;
        }
    }
}
