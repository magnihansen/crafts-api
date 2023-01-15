using System;
namespace CraftsApi.Controllers.V1.Requests
{
    public class AddUserRequest
    {
        public AddUserRequest(string username, string firstname, string lastname,
            string address, string zip, string city, string country, string email,
            string phone, bool active, string createdBy)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException($"'{nameof(username)}' cannot be null or whitespace.", nameof(username));
            }

            if (string.IsNullOrWhiteSpace(firstname))
            {
                throw new ArgumentException($"'{nameof(firstname)}' cannot be null or whitespace.", nameof(firstname));
            }

            if (string.IsNullOrWhiteSpace(lastname))
            {
                throw new ArgumentException($"'{nameof(lastname)}' cannot be null or whitespace.", nameof(lastname));
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException($"'{nameof(email)}' cannot be null or whitespace.", nameof(email));
            }

            if (string.IsNullOrWhiteSpace(createdBy))
            {
                throw new ArgumentException($"'{nameof(createdBy)}' cannot be null or whitespace.", nameof(createdBy));
            }

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
            CreatedBy = createdBy;
        }

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

        public string CreatedBy { get; }
    }
}
