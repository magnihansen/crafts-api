using System.Collections.Generic;
using System.Threading.Tasks;
using CraftsApi.Service.Requests;

namespace CraftsApi.Service
{
    public interface IPageService
    {
        Task<List<ViewModels.Page>> GetPagesAsync();

        Task<ViewModels.Page> GetPageAsync(int pageId);

        Task<ViewModels.Page> GetPageByLinkAsync(string pageLink);

        Task<ViewModels.Page> GetPageByUidAsync(string pageUid);

        Task<ViewModels.Page> GetDefaultPageAsync();

        Task<bool> AddPageAsync(AddPageRequest addPageReqest);

        Task<bool> UpdatePageAsync(UpdatePageRequest updatePageRequest);

        Task<bool> DeletePageAsync(int pageId);
    }
}
