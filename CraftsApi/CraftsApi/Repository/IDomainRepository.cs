using System;
using System.Threading.Tasks;

namespace CraftsApi.Repository
{
	public interface IDomainRepository
	{
        Task<DomainModels.Domain> GetDomainAsync(string host);

    }
}
