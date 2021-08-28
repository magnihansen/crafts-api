using System.Collections.Generic;
using System.Threading.Tasks;
using CraftsApi.DomainModels;

namespace CraftsApi.Application
{
    public interface IDataApplication
    {
        Task<Contact> GetContactAsync(int contactId);

        Task<List<DomainModels.Contact>> GetContactsAsync();
    }
}