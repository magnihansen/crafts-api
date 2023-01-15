using System.Collections.Generic;
using System.Threading.Tasks;

namespace CraftsApi.Repository
{
    public interface IPageRepository
    {
        Task<List<DomainModels.Page>> GetPagesAsync(string host);

        Task<DomainModels.Page> GetPageAsync(string host, int pageId);

        Task<DomainModels.Page> GetPageByLinkAsync(string host, string pageLink);

        Task<DomainModels.Page> GetDefaultPageAsync(string host);

        Task<DomainModels.Page> InsertPageAsync(string host, int domainId, string uid, int? parentId, int pageTypeId, string title, string content, int sort, string link, bool isRouterLink, bool active, string createdBy);

        Task<bool> UpdatePageAsync(string host, DomainModels.Page page);

        Task<bool> DeletePageAsync(string host, int pageId);
    }
}
