using System.Collections.Generic;
using System.Threading.Tasks;
using CraftsApi.DataAccess;
using CraftsApi.Helpers;

namespace CraftsApi.Application
{
    public class PageApplication : IPageApplication
    {
        private readonly IDataAccess _dataAccess;

        public PageApplication(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<List<DomainModels.Page>> GetPagesAsync()
        {
            return await _dataAccess.LoadData<DomainModels.Page, dynamic>(
                "SELECT * FROM Page",
                new { }
            );
        }

        public async Task<DomainModels.Page> GetPageAsync(int pageId)
        {
            return await _dataAccess.LoadSingleData<DomainModels.Page, dynamic>(
                @"SELECT * FROM Page
                WHERE Id = @PageId",
                new {
                    PageId = pageId
                }
            );
        }

        public async Task<DomainModels.Page> GetPageByLinkAsync(string pageLink)
        {
            return await _dataAccess.LoadSingleData<DomainModels.Page, dynamic>(
                @"SELECT * FROM Page
                WHERE Active = 1
                AND Link = @PageLink",
                new
                {
                    PageLink = pageLink
                }
            );
        }

        public async Task<DomainModels.Page> GetPageByUidAsync(string pageUid)
        {
            return await _dataAccess.LoadSingleData<DomainModels.Page, dynamic>(
                @"SELECT * FROM Page
                WHERE Active = 1
                AND Uid = @PageUid",
                new
                {
                    PageUid = pageUid
                }
            );
        }

        public async Task<DomainModels.Page> GetDefaultPageAsync()
        {
            return await _dataAccess.LoadSingleData<DomainModels.Page, dynamic>(
                @"SELECT *, MIN(PageRank) FROM Page
                WHERE Active = 1
                LIMIT 1",
                new {}
            );
        }

        public async Task<bool> AddPageAsync(DomainModels.Page page)
        {
            string sql = @"
            INSERT INTO Page (uid,title,parent,content,pagerank,link,active,createdby)
            VALUES (@uid,@title,@parent,@content,@pagerank,@link,@active,@createdby)
            ";
            int added = await _dataAccess.SaveData<dynamic>(sql, new {
                @uid = page.Uid,
                @title = page.Title,
                @parent = page.ParentUid,
                @content = page.Content,
                @pagerank = page.PageRank,
                @link = page.Link,
                @active = page.Active,
                @createdby = page.CreatedBy
            });
            return added > 0;
        }

        public async Task<bool> UpdatePageAsync(DomainModels.Page page)
        {
            string sql = @"
            UPDATE Page SET
            uid = @uid,
            title = @title,
            parent = IFNULL(@parent, ''),
            content = @content,
            pagerank = @pagerank,
            link = @link,
            active = @active,
            updateddate = @updateddate,
            updatedby = @updatedby
            WHERE Id = @id
            ";
            int updated = await _dataAccess.SaveData<dynamic>(sql, new
            {
                id = page.Id,
                uid = page.Uid,
                title = page.Title,
                parent = page.ParentUid,
                content = page.Content,
                pagerank = page.PageRank,
                link = page.Link,
                active = page.Active,
                updateddate = page.UpdatedDate.ConvertToMySqlDateTime(),
                updatedby = page.UpdatedBy
            });
            return updated > 0;
        }

        public async Task<bool> DeletePageAsync(int pageId)
        {
            string sql = @"
            DELETE FROM Page
            WHERE Id = @PageId
            ";
            int deleted = await _dataAccess.SaveData<dynamic>(sql, new
            {
                PageId = pageId
            });
            return deleted > 0;
        }
    }
}
