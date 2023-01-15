using System;
namespace CraftsApi.Service.ViewModels
{
    public class PageTypeVM
    {
        public PageTypeVM(int id, string name, bool active, DateTime createdDate, string createdBy,
            DateTime updatedDate, string updatedBy)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace", nameof(name));
            }

            if (string.IsNullOrWhiteSpace(createdBy))
            {
                throw new ArgumentException($"'{nameof(createdBy)}' cannot be null or whitespace", nameof(createdBy));
            }

            Id = id;
            Name = name;
            Active = active;
            CreatedDate = createdDate;
            CreatedBy = createdBy;
            UpdatedDate = updatedDate;
            UpdatedBy = updatedBy;
        }

        public int Id { get; }

        public string Name { get; }

        public bool Active { get; }

        public DateTime CreatedDate { get; }

        public string CreatedBy { get; }

        public DateTime UpdatedDate { get; }

        public string UpdatedBy { get; }
    }
}
