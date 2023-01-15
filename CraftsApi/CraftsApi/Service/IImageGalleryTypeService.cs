using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CraftsApi.Service
{
    public interface IImageGalleryTypeService
    {
        Task<List<DomainModels.ImageGalleryType>> GetImageGalleryTypesAsync(string host);
        Task<DomainModels.ImageGalleryType> GetImageGalleryTypeAsync(int imageGalleryTypeId);
        Task<bool> InsertImageGalleryTypeAsync(string host, DomainModels.ImageGalleryType imageGalleryType);
        Task<bool> UpdateImageGalleryTypeAsync(DomainModels.ImageGalleryType imageGalleryType);
        Task<bool> DeleteImageGalleryTypeAsync(int imageGalleryTypeId);
    }
}

