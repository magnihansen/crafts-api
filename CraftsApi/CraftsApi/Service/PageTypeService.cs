using System.Collections.Generic;
using System.Threading.Tasks;
using CraftsApi.Repository;
using CraftsApi.Service.Mappings;
using CraftsApi.Controllers.V1.Requests;

namespace CraftsApi.Service
{
    public class PageTypeService : IPageTypeService
    {
        private readonly IPageTypeRepository _pageTypeApplication;

        public PageTypeService(IPageTypeRepository pageTypeApplication)
        {
            _pageTypeApplication = pageTypeApplication;
        }

        public async Task<List<ViewModels.PageTypeVM>> GetPageTypesAsync()
        {
            List<DomainModels.PageType> pageTypes = await _pageTypeApplication.GetPageTypesAsync();
            return pageTypes.MapPageTypeToPageTypeVM();
        }

        public async Task<ViewModels.PageTypeVM> GetPageTypeAsync(int pageTypeId)
        {
            DomainModels.PageType pageType = await _pageTypeApplication.GetPageTypeAsync(pageTypeId);
            return pageType.MapPageTypeToPageTypeVM();
        }

        public async Task<bool> InsertPageTypeAsync(DomainModels.PageType pageType)
        {
            bool added = await _pageTypeApplication.InsertPageTypeAsync(pageType);
            return added;
        }

        public async Task<bool> UpdatePageTypeAsync(DomainModels.PageType pageType)
        {
            bool updated = await _pageTypeApplication.UpdatePageTypeAsync(pageType);
            return updated;
        }

        public async Task<bool> DeletePageTypeAsync(int pageTypeId)
        {
            bool deleted = await _pageTypeApplication.DeletePageTypeAsync(pageTypeId);
            return deleted;
        }
    }
}
