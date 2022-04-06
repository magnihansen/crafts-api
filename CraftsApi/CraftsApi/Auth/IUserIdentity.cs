using System;
using System.Security.Principal;

namespace CraftsApi.Auth
{
    public interface IUserIdentity : IIdentity
    {
        int UserId { get; }

        string Username { get; }

        string FullName { get; }

        string FirstName { get; }

        string LastName { get; }
    }
}
