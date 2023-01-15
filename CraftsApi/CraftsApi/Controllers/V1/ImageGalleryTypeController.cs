using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CraftsApi.Helpers;
using CraftsApi.Service;
using CraftsApi.Service.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CraftsApi.Controllers.V1
{
    [Version(1)]
    public class ImageGalleryTypeController : BaseController
    {
        private readonly IImageGalleryTypeService _imageGalleryTypeService;

        public ImageGalleryTypeController(IImageGalleryTypeService imageGalleryTypeService)
        {
            _imageGalleryTypeService = imageGalleryTypeService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<DomainModels.ImageGalleryType>))]
        public async Task<List<DomainModels.ImageGalleryType>> GetImageGalleryTypesAsync()
        {
            List<DomainModels.ImageGalleryType> imageTypes = await _imageGalleryTypeService.GetImageGalleryTypesAsync(Request.GetAddressHost());
            return imageTypes;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DomainModels.ImageGalleryType))]
        public async Task<DomainModels.ImageGalleryType> GetImageGalleryTypeAsync(int imageGalleryTypeId)
        {
            DomainModels.ImageGalleryType imageType = await _imageGalleryTypeService.GetImageGalleryTypeAsync(imageGalleryTypeId);
            return imageType;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<bool> InsertImageGalleryTypeAsync(DomainModels.ImageGalleryType imageGalleryType)
        {
            bool inserted = await _imageGalleryTypeService.InsertImageGalleryTypeAsync(Request.GetAddressHost(), imageGalleryType);
            return inserted;
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<bool> UpdateImageGalleryTypeAsync(DomainModels.ImageGalleryType imageGalleryType)
        {
            bool updated = await _imageGalleryTypeService.UpdateImageGalleryTypeAsync(imageGalleryType);
            return updated;
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<bool> DeleteImageGalleryTypeAsync(int imageGalleryTypeId)
        {
            bool deleted = await _imageGalleryTypeService.DeleteImageGalleryTypeAsync(imageGalleryTypeId);
            return deleted;
        }
    }
}

