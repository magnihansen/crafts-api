using System;
namespace CraftsApi.Controllers.V1.Requests
{
    public class AddPageTypeRequest
    {
        public AddPageTypeRequest(string name, bool active, string createdBy)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace", nameof(name));
            }

            if (string.IsNullOrWhiteSpace(createdBy))
            {
                throw new ArgumentException($"'{nameof(createdBy)}' cannot be null or whitespace", nameof(createdBy));
            }

            Name = name;
            Active = active;
            CreatedBy = createdBy;
        }

        public string Name { get; }

        public bool Active { get; }

        public string CreatedBy { get; }
    }
}
