using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CraftsApi.Repository
{
    public interface IImageGalleryRepository
    {
        Task<List<DomainModels.ImageGallery>> GetImageGalleriesAsync();
        Task<DomainModels.ImageGallery> GetImageGalleryAsync(int imageGalleryId);
        Task<bool> InsertImageGalleryAsync(DomainModels.ImageGallery imageGallery);
        Task<bool> UpdateImageGalleryAsync(DomainModels.ImageGallery imageGallery);
        Task<bool> DeleteImageGalleryAsync(int imageGalleryId);
    }
}

