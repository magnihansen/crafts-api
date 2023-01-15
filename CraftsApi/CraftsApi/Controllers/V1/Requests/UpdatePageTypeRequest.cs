using System;
namespace CraftsApi.Controllers.V1.Requests
{
    public class UpdatePageTypeRequest
    {
        public UpdatePageTypeRequest(int id, string name, bool active, DateTime updatedDate, string updatedBy)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace", nameof(name));
            }

            if (string.IsNullOrWhiteSpace(updatedBy))
            {
                throw new ArgumentException($"'{nameof(updatedBy)}' cannot be null or whitespace", nameof(updatedBy));
            }

            Id = id;
            Name = name;
            Active = active;
            UpdatedDate = updatedDate;
            UpdatedBy = updatedBy;
        }

        public int Id { get; }

        public string Name { get; }

        public bool Active { get; }

        public DateTime UpdatedDate { get; }

        public string UpdatedBy { get; }
    }
}
