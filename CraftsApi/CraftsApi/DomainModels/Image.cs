using System;
namespace CraftsApi.DomainModels
{
    public class Image : BaseDateColumns
    {
        public int Id { get; set; }
        public string Base64String { get; set; }
        public int ImageGalleryId { get; set; }
    }
}

