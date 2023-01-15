using System.Collections.Generic;
using System.Threading.Tasks;
using CraftsApi.DataAccess;
using CraftsApi.Helpers;

namespace CraftsApi.Repository
{
    public class PageTypeRepository : IPageTypeRepository
    {
        private readonly IDataAccess _dataAccess;

        public PageTypeRepository(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<List<DomainModels.PageType>> GetPageTypesAsync()
        {
            return await _dataAccess.LoadData<DomainModels.PageType, dynamic>(
                "SELECT * FROM PageType",
                new { }
            );
        }

        public async Task<DomainModels.PageType> GetPageTypeAsync(int pageTypeId)
        {
            return await _dataAccess.LoadSingleData<DomainModels.PageType, dynamic>(
                @"SELECT * FROM PageType
                WHERE Id = @PageTypeId",
                new {
                    PageTypeId = pageTypeId
                }
            );
        }

        public async Task<bool> InsertPageTypeAsync(DomainModels.PageType pageType)
        {
            string sql = @"
            INSERT INTO PageType ([name],active,createdby)
            VALUES (@name,@active,@createdby)
            ";
            int added = await _dataAccess.SaveData<dynamic>(sql, new {
                name = pageType.Name,
                @active = pageType.Active,
                @createdby = pageType.CreatedBy
            });
            return added > 0;
        }

        public async Task<bool> UpdatePageTypeAsync(DomainModels.PageType pageType)
        {
            string sql = @"
            UPDATE PageType SET
            [name] = @name,
            active = @active,
            updateddate = @updateddate,
            updatedby = @updatedby
            WHERE Id = @id
            ";
            int updated = await _dataAccess.SaveData<dynamic>(sql, new
            {
                id = pageType.Id,
                uid = pageType.Name,
                active = pageType.Active,
                updateddate = pageType.UpdatedDate.ConvertToMySqlDateTime(),
                updatedby = pageType.UpdatedBy
            });
            return updated > 0;
        }

        public async Task<bool> DeletePageTypeAsync(int pageTypeId)
        {
            string sql = @"
            DELETE FROM PageType
            WHERE Id = @PageTypeId
            ";
            int deleted = await _dataAccess.SaveData<dynamic>(sql, new
            {
                PageTypeId = pageTypeId
            });
            return deleted > 0;
        }
    }
}
