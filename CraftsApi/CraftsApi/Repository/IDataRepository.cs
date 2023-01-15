using System.Collections.Generic;
using System.Threading.Tasks;
using CraftsApi.DomainModels;

namespace CraftsApi.Repository
{
    public interface IDataRepository
    {
        Task<Contact> GetContactAsync(int contactId);

        Task<List<DomainModels.Contact>> GetContactsAsync();
    }
}