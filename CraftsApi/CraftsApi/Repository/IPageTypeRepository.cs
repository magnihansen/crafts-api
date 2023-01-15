using System.Collections.Generic;
using System.Threading.Tasks;

namespace CraftsApi.Repository
{
    public interface IPageTypeRepository
    {
        Task<List<DomainModels.PageType>> GetPageTypesAsync();

        Task<DomainModels.PageType> GetPageTypeAsync(int pageTypeId);

        Task<bool> InsertPageTypeAsync(DomainModels.PageType pageType);

        Task<bool> UpdatePageTypeAsync(DomainModels.PageType pageType);

        Task<bool> DeletePageTypeAsync(int pageTypeId);
    }
}
