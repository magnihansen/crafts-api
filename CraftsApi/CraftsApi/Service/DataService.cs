using System.Collections.Generic;
using System.Threading.Tasks;
using CraftsApi.Repository;
using CraftsApi.Service.Mappings;

namespace CraftsApi.Service
{
    public class DataService : IDataService
    {
        private readonly IDataRepository _dataApplication;

        public DataService(IDataRepository dataApplication)
        {
            _dataApplication = dataApplication;
        }

        public async Task<ViewModels.ContactVM> GetContactAsync(int contactId)
        {
            DomainModels.Contact contact = await _dataApplication.GetContactAsync(contactId);
            return contact.MapDomainContactToViewContact();
        }

        public async Task<List<ViewModels.ContactVM>> GetContactsAsync()
        {
            List<DomainModels.Contact> contacts = await _dataApplication.GetContactsAsync();
            return contacts.MapContactToContactVM();
        }
    }
}
