using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CraftsApi.DataAccess;

namespace CraftsApi.Repository
{
    public class ImageGalleryRepository : IImageGalleryRepository
    {
        private readonly IDataAccess _dataAccess;

        public ImageGalleryRepository(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<List<DomainModels.ImageGallery>> GetImageGalleriesAsync()
        {
            return await _dataAccess.LoadData<DomainModels.ImageGallery, dynamic>(
                "SELECT * FROM imagegallery",
                new { }
            );
        }

        public async Task<DomainModels.ImageGallery> GetImageGalleryAsync(int imageGalleryId)
        {
            return await _dataAccess.LoadSingleData<DomainModels.ImageGallery, dynamic>(
                @"SELECT * FROM imagegallery WHERE Id = @imageGalleryId",
                new
                {
                    @imageGalleryId = imageGalleryId
                }
            );
        }

        public async Task<bool> InsertImageGalleryAsync(DomainModels.ImageGallery imageGallery)
        {
            string sql = @"
            INSERT INTO imagegallery (DomainId,Name,Description,ImageGalleryTypeId,ImageGalleryTemplateId,Active,CreatedBy)
            VALUES (@domainId,@name,@description,@imageGalleryTypeId,@imageGalleryTemplateId,@active,@createdBy)
            ";

            int inserted = await _dataAccess.SaveData<dynamic>(sql, new
            {
                @domainId = imageGallery.DomainId,
                @name = imageGallery.Name,
                @description = imageGallery.Description,
                @imageGalleryTypeId = imageGallery.ImageGalleryTypeId,
                @imageGalleryTemplateId = imageGallery.ImageGalleryTemplateId,
                @active = imageGallery.Active,
                @createdBy = imageGallery.CreatedBy
            });
            return inserted > 0;
        }

        public async Task<bool> UpdateImageGalleryAsync(DomainModels.ImageGallery imageGallery)
        {
            string sql = @"
            UPDATE imagegallery SET
            Name = @name,
            Description = @imageGalleryId,
            ImageGalleryTypeId = @imageGalleryTypeId,
            ImageGalleryTemplateId = @imageGalleryTemplateId,
            UpdatedDate = @updatedDate,
            UpdatedBy = @updatedBy
            WHERE Id = @id
            ";
            int updated = await _dataAccess.SaveData<dynamic>(sql, new
            {
                @id = imageGallery.Id,
                @name = imageGallery.Name,
                @description = imageGallery.Description,
                @imageGalleryTypeId = imageGallery.ImageGalleryTypeId,
                @imageGalleryTemplateId = imageGallery.ImageGalleryTemplateId,
                @updatedDate = imageGallery.UpdatedDate,
                @updatedBy = imageGallery.UpdatedBy
            });
            return updated > 0;
        }

        public async Task<bool> DeleteImageGalleryAsync(int imageGalleryId)
        {
            string sql = @"
            DELETE FROM imagegallery WHERE Id = @imageId
            ";
            int deleted = await _dataAccess.SaveData<dynamic>(sql, new
            {
                @imageGalleryId = imageGalleryId
            });
            return deleted > 0;
        }
    }
}

