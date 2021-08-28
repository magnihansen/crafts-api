using System;
namespace CraftsApi.DomainModels
{
    public class FileUploadResponse
    {
        public FileUploadResponse(
            bool status,
            string originalName,
            string generatedName,
            string msg,
            string imageUrl)
        {
            if (string.IsNullOrWhiteSpace(imageUrl))
            {
                throw new ArgumentException($"'{nameof(imageUrl)}' cannot be null or whitespace.", nameof(imageUrl));
            }

            Status = status;
            OriginalName = originalName;
            GeneratedName = generatedName;
            Msg = msg;
            ImageUrl = imageUrl;
        }

        public bool Status { get; }

        public string OriginalName { get; }

        public string GeneratedName { get; }

        public string Msg { get; }

        public string ImageUrl { get; }
    }
}
