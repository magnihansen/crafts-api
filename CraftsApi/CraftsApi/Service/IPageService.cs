using System.Collections.Generic;
using System.Threading.Tasks;
using CraftsApi.Controllers.V1.Requests;

namespace CraftsApi.Service
{
    public interface IPageService
    {
        Task<List<ViewModels.PageVM>> GetPagesAsync(string host);

        Task<ViewModels.PageVM> GetPageAsync(string host, int pageId);

        Task<ViewModels.PageVM> GetPageByLinkAsync(string host, string pageLink);

        Task<ViewModels.PageVM> GetDefaultPageAsync(string host);

        Task<ViewModels.PageVM> InsertPageAsync(string host, DomainModels.Page page);

        Task<bool> UpdatePageAsync(string host, DomainModels.Page page);

        Task<bool> DeletePageAsync(string host, int pageId);
    }
}
