using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CraftsApi.DomainModels;
using CraftsApi.Repository;
using Microsoft.Extensions.Hosting;

namespace CraftsApi.Service
{
    public class ImageGalleryService : IImageGalleryService
    {
        private readonly IDomainRepository _domainRepository;
        private readonly IImageGalleryRepository _imageGalleryRepository;

        public ImageGalleryService(IImageGalleryRepository imageGalleryRepository, IDomainRepository domainRepository)
        {
            _imageGalleryRepository = imageGalleryRepository;
            _domainRepository = domainRepository;
        }

        public async Task<List<DomainModels.ImageGallery>> GetImageGalleriesAsync()
        {
            List<DomainModels.ImageGallery> imageGalleries = await _imageGalleryRepository.GetImageGalleriesAsync();
            return imageGalleries;
        }

        public async Task<DomainModels.ImageGallery> GetImageGalleryAsync(int imageGalleryId)
        {
            DomainModels.ImageGallery imageGallery = await _imageGalleryRepository.GetImageGalleryAsync(imageGalleryId);
            return imageGallery;
        }

        public async Task<bool> InsertImageGalleryAsync(string host, DomainModels.ImageGallery imageGallery)
        {
            DomainModels.Domain domain = await _domainRepository.GetDomainAsync(host);
            if (domain is not null)
            {
                imageGallery.DomainId = domain.Id;
                bool inserted = await _imageGalleryRepository.InsertImageGalleryAsync(imageGallery);
                return inserted;
            }
            return false;
        }

        public async Task<bool> UpdateImageGalleryAsync(DomainModels.ImageGallery imageGallery)
        {
            bool updated = await _imageGalleryRepository.UpdateImageGalleryAsync(imageGallery);
            return updated;
        }

        public async Task<bool> DeleteImageGalleryAsync(int imageGalleryId)
        {
            bool deleted = await _imageGalleryRepository.DeleteImageGalleryAsync(imageGalleryId);
            return deleted;
        }
    }
}

