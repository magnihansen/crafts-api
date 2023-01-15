using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CraftsApi.Controllers.V1.Requests;
using CraftsApi.Helpers;
using CraftsApi.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CraftsApi.Controllers.V1
{
    [Version(1)]
    public class ImageGalleryController : BaseController
    {
        private readonly IImageGalleryService _imageGalleryService;

        public ImageGalleryController(IImageGalleryService imageGalleryService)
        {
            _imageGalleryService = imageGalleryService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<DomainModels.ImageGallery>))]
        public async Task<List<DomainModels.ImageGallery>> GetImageGalleriesAsync()
        {
            List<DomainModels.ImageGallery> imageGalleries = await _imageGalleryService.GetImageGalleriesAsync();
            return imageGalleries;
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DomainModels.ImageGallery))]
        public async Task<DomainModels.ImageGallery> GetImageGalleryAsync(int imageGalleryId)
        {
            DomainModels.ImageGallery imageGallery = await _imageGalleryService.GetImageGalleryAsync(imageGalleryId);
            return imageGallery;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<bool> InsertImageGalleryAsync(InsertImageGalleryRequest insertImageGalleryRequest)
        {
            var imageGallery = new DomainModels.ImageGallery()
            {
                Name = insertImageGalleryRequest.Name,
                Description = insertImageGalleryRequest.Description,
                ImageGalleryTypeId = insertImageGalleryRequest.ImageGalleryTypeId,
                ImageGalleryTemplateId = insertImageGalleryRequest.ImageGalleryTemplateId,
                CreatedBy = insertImageGalleryRequest.CreatedBy
            };
            bool inserted = await _imageGalleryService.InsertImageGalleryAsync(Request.GetAddressHost(), imageGallery);
            return inserted;
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<bool> UpdateImageGalleryAsync(DomainModels.ImageGallery imageGallery)
        {
            bool updated = await _imageGalleryService.UpdateImageGalleryAsync(imageGallery);
            return updated;
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<bool> DeleteImageGalleryAsync(int imageGalleryId)
        {
            bool deleted = await _imageGalleryService.DeleteImageGalleryAsync(imageGalleryId);
            return deleted;
        }
    }
}

