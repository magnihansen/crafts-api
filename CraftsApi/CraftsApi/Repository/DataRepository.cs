using System.Collections.Generic;
using System.Threading.Tasks;
using CraftsApi.DataAccess;

namespace CraftsApi.Repository
{
    public class DataRepository : IDataRepository
    {
        private readonly IDataAccess _dataAccess;

        public DataRepository(IDataAccess dataAccess)
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
