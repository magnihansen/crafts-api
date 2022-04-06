using System;
namespace CraftsApi.Auth
{
    public class UserIdentity : IUserIdentity
    {
        public UserIdentity(int userId, string username, string authenticationType, bool isAuthenticated)
        {
            UserId = userId;
            Username = username;
            AuthenticationType = authenticationType;
            IsAuthenticated = isAuthenticated;
        }
        public UserIdentity(int userId, string username, string fullName, string firstName, string lastName, string authenticationType, bool isAuthenticated)
        {
            UserId = userId;
            Username = username;
            FullName = fullName;
            FirstName = firstName;
            LastName = lastName;
            AuthenticationType = authenticationType;
            IsAuthenticated = isAuthenticated;
        }

        public int UserId { get; }

        public string Username { get; }

        public string FullName { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public string AuthenticationType { get; }

        public bool IsAuthenticated { get; }

        public string Name => Username;
    }
}
