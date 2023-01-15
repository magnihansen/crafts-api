using System;
using System.ComponentModel.DataAnnotations;

namespace CraftsApi.Controllers.V1.Requests
{
    public class AuthenticateRequest
    {
        public AuthenticateRequest(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException($"'{nameof(username)}' cannot be null or whitespace.", nameof(username));
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException($"'{nameof(password)}' cannot be null or whitespace.", nameof(password));
            }

            Username = username;
            Password = password;
        }

        [Required]
        public string Username { get; }

        [Required]
        public string Password { get; }
    }
}
