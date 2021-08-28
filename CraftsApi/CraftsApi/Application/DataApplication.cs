using System.Collections.Generic;
using System.Threading.Tasks;
using CraftsApi.DataAccess;

namespace CraftsApi.Application
{
    public class DataApplication : IDataApplication
    {
        private readonly IDataAccess _dataAccess;

        public DataApplication(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<DomainModels.Contact> GetContactAsync(int contactId)
        {
            return await _dataAccess.LoadSingleData<DomainModels.Contact, dynamic>(
                @"SELECT * FROM Contact
                WHERE Id = @contactId",
                new
                {
                    contactId
                }
            );
        }

        public async Task<List<DomainModels.Contact>> GetContactsAsync()
        {
            return await _dataAccess.LoadData<DomainModels.Contact, dynamic>(
                @"SELECT * FROM Contact",
                new { }
            );
        }
    }
}
