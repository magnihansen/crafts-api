using System;
namespace CraftsApi.Service.ViewModels
{
    public class ContactVM
    {
        public ContactVM(int id, string name, string email, string phone, string message, DateTime createdDate, string createdBy)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException($"'{nameof(email)}' cannot be null or whitespace.", nameof(email));
            }

            if (string.IsNullOrWhiteSpace(createdBy))
            {
                throw new ArgumentException($"'{nameof(createdBy)}' cannot be null or whitespace.", nameof(createdBy));
            }

            Id = id;
            Name = name;
            Email = email;
            Phone = phone;
            Message = message;
            CreatedDate = createdDate;
            CreatedBy = createdBy;
        }

        public int Id { get; }

        public string Name { get; }

        public string Email { get; }

        public string Phone { get; }

        public string Message { get; }

        public DateTime CreatedDate { get; }

        public string CreatedBy { get; }
    }
}
