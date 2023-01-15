using System.Collections.Generic;
using System.Threading.Tasks;

namespace CraftsApi.DataAccess
{
    public interface IDataAccess
    {
        Task<List<T>> LoadData<T, U>(string sql, U parameters);

        Task<T> LoadSingleData<T, U>(string sql, U parameters);

        Task<int> SaveData<T>(string sql, T parameters);

        Task<int> SaveDataWithReturn<T>(string sql, T parameters);
    }
}