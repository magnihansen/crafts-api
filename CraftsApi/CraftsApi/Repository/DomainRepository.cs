using System;
using System.Threading.Tasks;
using CraftsApi.DataAccess;

namespace CraftsApi.Repository
{
	public class DomainRepository : IDomainRepository
    {
        private readonly IDataAccess _dataAccess;

        public DomainRepository(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<DomainModels.Domain> GetDomainAsync(string host)
        {
            DomainModels.Domain domain = await _dataAccess.LoadSingleData<DomainModels.Domain, dynamic>(
                @"SELECT * FROM Domain d
                WHERE d.Host = @host
                AND d.Active = 1",
                new
                {
                    @host = host
                }
            );

            return domain;
        }
    }
}

