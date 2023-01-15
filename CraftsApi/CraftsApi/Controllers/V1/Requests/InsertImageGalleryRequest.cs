using System;
namespace CraftsApi.Controllers.V1.Requests
{
	public class InsertImageGalleryRequest
	{
        public InsertImageGalleryRequest(string name, string description, int imageGalleryTypeId, int imageGalleryTemplateId, string createdBy)
        {
            Name = name;
            Description = description;
            ImageGalleryTypeId = imageGalleryTypeId;
            ImageGalleryTemplateId = imageGalleryTemplateId;
            CreatedBy = createdBy;
        }

        public string Name { get; }
		public string Description { get; }
		public int ImageGalleryTypeId { get; }
		public int ImageGalleryTemplateId { get; }
		public string CreatedBy { get;  }
	}
}

