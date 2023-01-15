using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CraftsApi.Repository;
using Microsoft.Extensions.Hosting;

namespace CraftsApi.Service
{
    public class ImageGalleryTypeService : IImageGalleryTypeService
    {
        private readonly IDomainRepository _domainRepository;
        private readonly IImageGalleryTypeRepository _imageGalleryTypeRepository;

        public ImageGalleryTypeService(IImageGalleryTypeRepository imageGalleryTypeRepository, IDomainRepository domainRepository)
        {
            _imageGalleryTypeRepository = imageGalleryTypeRepository;
            _domainRepository = domainRepository;
        }

        public async Task<List<DomainModels.ImageGalleryType>> GetImageGalleryTypesAsync(string host)
        {
            List<DomainModels.ImageGalleryType> imageGalleryTypes = await _imageGalleryTypeRepository.GetImageGalleryTypesAsync(host);
            return imageGalleryTypes;
        }

        public async Task<DomainModels.ImageGalleryType> GetImageGalleryTypeAsync(int imageGalleryTypeId)
        {
            DomainModels.ImageGalleryType imageGalleryType = await _imageGalleryTypeRepository.GetImageGalleryTypeAsync(imageGalleryTypeId);
            return imageGalleryType;
        }

        public async Task<bool> InsertImageGalleryTypeAsync(string host, DomainModels.ImageGalleryType imageGalleryType)
        {
            DomainModels.Domain domain = await _domainRepository.GetDomainAsync(host);
            if (domain is not null)
            {
                bool rowExists = await _imageGalleryTypeRepository.RowExsits(domain.Id, imageGalleryType.Name);
                if (!rowExists)
                {
                    imageGalleryType.DomainId = domain.Id;
                    bool inserted = await _imageGalleryTypeRepository.InsertImageGalleryTypeAsync(imageGalleryType);
                    return inserted;
                }
            }
            return false;
        }

        public async Task<bool> UpdateImageGalleryTypeAsync(DomainModels.ImageGalleryType imageGalleryType)
        {
            bool updated = await _imageGalleryTypeRepository.UpdateImageGalleryTypeAsync(imageGalleryType);
            return updated;
        }

        public async Task<bool> DeleteImageGalleryTypeAsync(int imageGalleryTypeId)
        {
            bool deleted = await _imageGalleryTypeRepository.DeleteImageGalleryTypeAsync(imageGalleryTypeId);
            return deleted;
        }
    }
}

