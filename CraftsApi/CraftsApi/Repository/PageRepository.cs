using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CraftsApi.DataAccess;
using CraftsApi.Helpers;

namespace CraftsApi.Repository
{
    public class PageRepository : IPageRepository
    {
        private readonly IDataAccess _dataAccess;

        public PageRepository(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<List<DomainModels.Page>> GetPagesAsync(string host)
        {
            List<DomainModels.Page> pages = await _dataAccess.LoadData<DomainModels.Page, dynamic>(
                @"SELECT p.* FROM Page p
                INNER JOIN Domain d
                ON p.DomainId = d.Id AND (d.Host = @host OR 0 = STRCMP(@host,'localhost')) AND d.Active = 1",
                new {
                    @host = host
                }
            );
            return pages;
        }

        public async Task<DomainModels.Page> GetPageAsync(string host, int pageId)
        {
            DomainModels.Page page = await _dataAccess.LoadSingleData<DomainModels.Page, dynamic>(
                @"SELECT p.* FROM Page p
                INNER JOIN Domain d
                ON p.DomainId = d.Id AND (d.Host = @host OR 0 = STRCMP(@host,'localhost')) AND d.Active = 1
                WHERE p.Id = @PageId",
                new {
                    @host = host,
                    @PageId = pageId
                }
            );
            return page;
        }

        public async Task<DomainModels.Page> GetPageByLinkAsync(string host, string pageLink)
        {
            DomainModels.Page page = await _dataAccess.LoadSingleData<DomainModels.Page, dynamic>(
                @"SELECT p.* FROM Page p
                INNER JOIN Domain d
                ON p.DomainId = d.Id AND (d.Host = @host OR 0 = STRCMP(@host,'localhost')) AND d.Active = 1
                WHERE p.Active = 1
                AND p.Link = @PageLink",
                new
                {
                    @host = host,
                    @PageLink = pageLink
                }
            );
            return page;
        }

        public async Task<DomainModels.Page> GetDefaultPageAsync(string host)
        {
            DomainModels.Page page = await _dataAccess.LoadSingleData<DomainModels.Page, dynamic>(
                @"SELECT p.* FROM Page p
                INNER JOIN Domain d
                ON p.DomainId = d.Id AND (d.Host = @host OR 0 = STRCMP(@host,'localhost')) AND d.Active = 1
                WHERE p.Active = 1
                ORDER BY p.Sort ASC
                LIMIT 1",
                new {
                    @host = host
                }
            );

            return page;
        }

        public async Task<DomainModels.Page> InsertPageAsync(
            string host,
            int domainId,
            string uid,
            int? parentId,
            int pageTypeId,
            string title,
            string content,
            int sort,
            string link,
            bool isRouterLink,
            bool active,
            string createdBy)
        {
            string sql = @"
            INSERT INTO Page (DomainId,Uid,ParentId,PageTypeId,Title,Content,Sort,Link,IsRouterLink,Active,Createdby)
            VALUES (@DomainId,@Uid,@ParentId,@PageTypeId,@Title,@Content,@Sort,@Link,@IsRouterLink,@Active,@Createdby);
            SELECT LAST_INSERT_ID();
            ";
            int insertId = await _dataAccess.SaveDataWithReturn<dynamic>(sql, new {
                @DomainId = domainId,
                @Uid = uid,
                @ParentId = parentId,
                @PageTypeId = pageTypeId,
                @Title = title,
                @Content = content,
                @Sort = sort,
                @Link = link,
                @IsRouterLink = isRouterLink,
                @Active = active,
                @Createdby = createdBy
            });

            DomainModels.Page insertedPage = await GetPageAsync(host, insertId);
            return insertedPage;
        }

        public async Task<bool> UpdatePageAsync(string host, DomainModels.Page page)
        {
            string sql = @"
            UPDATE Page p SET
            p.ParentId = @ParentId,
            p.PageTypeId = @PageTypeId,
            p.Title = @Title,
            p.Content = @Content,
            p.Sort = @Sort,
            p.Link = @Link,
            p.IsRouterLink = @IsRouterLink,
            p.Active = @Active,
            p.UpdatedDate = NOW() + INTERVAL 1 HOUR,
            p.UpdatedBy = @UpdatedBy
            WHERE p.Id = @Id
            ";
            int updated = await _dataAccess.SaveData<dynamic>(sql, new
            {
                @host = host,
                @Id = page.Id,
                @ParentId = page.ParentId,
                @PageTypeId = page.PageTypeId,
                @Title = page.Title,
                @Content = page.Content,
                @Sort = page.Sort,
                @Link = page.Link,
                @IsRouterLink = page.IsRouterLink,
                @Active = page.Active,
                @UpdatedBy = page.UpdatedBy
            });
            return updated > 0;
        }

        public async Task<bool> DeletePageAsync(string host, int pageId)
        {
            string sql = @"
            DELETE p FROM Page p
            INNER JOIN Domain d
            ON p.DomainId = d.Id AND (d.Host = @host OR 0 = STRCMP(@host,'localhost')) AND d.Active = 1
            WHERE p.Id = @Id
            ";
            int deleted = await _dataAccess.SaveData<dynamic>(sql, new
            {
                @Id = pageId,
                @host = host
            });
            return deleted > 0;
        }
    }
}
