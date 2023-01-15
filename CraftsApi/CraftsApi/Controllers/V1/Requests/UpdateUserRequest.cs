using System;
namespace CraftsApi.Controllers.V1.Requests
{
    public class UpdateUserRequest
    {
        public UpdateUserRequest(int parentId, string username, string firstname, string lastname,
            string address, string zip, string city, string country, string email, string phone,
            bool active, DateTime updatedDate, string updatedBy)
        {
            ParentId = parentId;
            Username = username;
            Firstname = firstname;
            Lastname = lastname;
            Address = address;
            Zip = zip;
            City = city;
            Country = country;
            Email = email;
            Phone = phone;
            Active = active;
            UpdatedDate = updatedDate;
            UpdatedBy = updatedBy;
        }

        public int ParentId { get; }

        public string Username { get; }

        public string Password { get; }

        public string Firstname { get; }

        public string Lastname { get; }

        public string Address { get; }

        public string Zip { get; }

        public string City { get; }

        public string Country { get; }

        public string Email { get; }

        public string Phone { get; }

        public bool Active { get; }

        public DateTime UpdatedDate { get; }

        public string UpdatedBy { get; }
    }
}
