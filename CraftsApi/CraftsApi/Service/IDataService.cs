using System.Collections.Generic;
using System.Threading.Tasks;
using CraftsApi.Service.ViewModels;

namespace CraftsApi.Service
{
    public interface IDataService
    {
        Task<ContactVM> GetContactAsync(int contactId);
        Task<List<ContactVM>> GetContactsAsync();
    }
}