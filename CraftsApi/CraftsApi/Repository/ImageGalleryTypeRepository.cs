using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CraftsApi.DataAccess;
using CraftsApi.DomainModels;

namespace CraftsApi.Repository
{
    public class ImageGalleryTypeRepository : IImageGalleryTypeRepository
    {
        private readonly IDataAccess _dataAccess;

        public ImageGalleryTypeRepository(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<List<DomainModels.ImageGalleryType>> GetImageGalleryTypesAsync(string host)
        {
            return await _dataAccess.LoadData<DomainModels.ImageGalleryType, dynamic>(
                @"SELECT igt.* FROM ImageGalleryType igt
                INNER JOIN Domain d
                ON igt.DomainId = d.Id AND (d.Host = @host OR 0 = STRCMP(@host,'localhost')) AND d.Active = 1",
                new {
                    @host = host
                }
            );
        }

        public async Task<DomainModels.ImageGalleryType> GetImageGalleryTypeAsync(int imageGalleryTypeId)
        {
            return await _dataAccess.LoadSingleData<DomainModels.ImageGalleryType, dynamic>(
                @"SELECT * FROM ImageGalleryType WHERE Id = @imageGalleryTypeId",
                new
                {
                    @imageGalleryTypeId = imageGalleryTypeId
                }
            );
        }

        public async Task<bool> RowExsits(int domainId, string name)
        {
            return await _dataAccess.LoadSingleData<bool, dynamic>(
                @"SELECT * FROM ImageGalleryType WHERE domainId = @domainId AND Name = @name",
                new
                {
                    @domainId = domainId,
                    @name = name
                }
            );
        }

        public async Task<bool> InsertImageGalleryTypeAsync(DomainModels.ImageGalleryType imageGalleryType)
        {
            string sql = @"
            INSERT INTO ImageGalleryType (DomainId,Name)
            VALUES (@domainId,@name)
            ";

            int inserted = await _dataAccess.SaveData<dynamic>(sql, new
            {
                @domainId = imageGalleryType.DomainId,
                @name = imageGalleryType.Name
            });
            return inserted > 0;
        }

        public async Task<bool> UpdateImageGalleryTypeAsync(DomainModels.ImageGalleryType imageGalleryType)
        {
            string sql = @"
            UPDATE ImageGalleryType SET
            Name = @name
            WHERE Id = @id
            ";
            int updated = await _dataAccess.SaveData<dynamic>(sql, new
            {
                @id = imageGalleryType.Id,
                @name = imageGalleryType.Name
            });
            return updated > 0;
        }

        public async Task<bool> DeleteImageGalleryTypeAsync(int imageGalleryTypeId)
        {
            string sql = @"
            DELETE FROM ImageGalleryType WHERE Id = @imageGalleryTypeId
            ";
            int deleted = await _dataAccess.SaveData<dynamic>(sql, new
            {
                @imageGalleryTypeId = imageGalleryTypeId
            });
            return deleted > 0;
        }
    }
}