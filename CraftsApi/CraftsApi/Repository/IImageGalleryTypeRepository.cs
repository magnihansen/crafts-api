using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CraftsApi.Repository
{
    public interface IImageGalleryTypeRepository
    {
        Task<List<DomainModels.ImageGalleryType>> GetImageGalleryTypesAsync(string host);
        Task<DomainModels.ImageGalleryType> GetImageGalleryTypeAsync(int imageGalleryTypeId);
        Task<bool> RowExsits(int domainId, string name);
        Task<bool> InsertImageGalleryTypeAsync(DomainModels.ImageGalleryType imageGalleryType);
        Task<bool> UpdateImageGalleryTypeAsync(DomainModels.ImageGalleryType imageGalleryType);
        Task<bool> DeleteImageGalleryTypeAsync(int imageGalleryTypeId);
    }
}

