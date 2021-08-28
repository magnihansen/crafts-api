using System.Collections.Generic;
using System.Threading.Tasks;
using CraftsApi.Service.ViewModels;

namespace CraftsApi.Service
{
    public interface IDataService
    {
        Task<Contact> GetContactAsync(int contactId);
        Task<List<Contact>> GetContactsAsync();
    }
}