using System.Collections.Generic;
using System.Threading.Tasks;
using CraftsApi.Application;
using CraftsApi.Service.Mappings;

namespace CraftsApi.Service
{
    public class DataService : IDataService
    {
        private readonly IDataApplication _dataApplication;

        public DataService(IDataApplication dataApplication)
        {
            _dataApplication = dataApplication;
        }

        public async Task<ViewModels.Contact> GetContactAsync(int contactId)
        {
            DomainModels.Contact contact = await _dataApplication.GetContactAsync(contactId);
            return contact.MapDomainContactToViewContact();
        }

        public async Task<List<ViewModels.Contact>> GetContactsAsync()
        {
            List<DomainModels.Contact> contacts = await _dataApplication.GetContactsAsync();
            return contacts.MapListOfDomainContactsToListOfViewContacts();
        }
    }
}
