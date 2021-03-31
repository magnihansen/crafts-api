using System.Collections.Generic;
using System.Threading.Tasks;

namespace CraftsApi.Application
{
    public interface IPageApplication
    {
        Task<List<DomainModels.Page>> GetPagesAsync();

        Task<DomainModels.Page> GetPageAsync(int pageId);

        Task<DomainModels.Page> GetPageByLinkAsync(string pageLink);

        Task<DomainModels.Page> GetDefaultPageAsync();

        Task<bool> AddPageAsync(DomainModels.Page page);

        Task<bool> UpdatePageAsync(DomainModels.Page page);

        Task<bool> DeletePageAsync(int pageId);
    }
}
