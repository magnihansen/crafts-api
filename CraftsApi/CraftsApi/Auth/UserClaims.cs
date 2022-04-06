using System;
namespace CraftsApi.Auth
{
    /// <summary>
    /// A catalog of user claim keys.
    /// </summary>
    public class UserClaims : AdministrativeSystemClaims, IValidationClaims
    {
        /// <summary>

        /// <summary>
        /// Gets or sets a claim representing the subject/user id.
        /// </summary>
        public int Subject { get; set; }

        /// <summary>
        /// Gets or sets a claim representing the user id. An alias for subject.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets a claim representing the user name of the user.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets a claim representing the firstname of the user.
        /// </summary>
        public string Firstname { get; set; }

        /// <summary>
        /// Gets or sets a claim representing the lastname of the user.
        /// </summary>
        public string Lastname { get; set; }

        /// <summary>
        /// Gets or sets a claim representing the full name of the user.
        /// </summary>
        public string Fullname => $"{Firstname} {Lastname}".Trim();

        /// <summary>
        /// Gets or sets a claim representing the email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets a claim representing a Unix-timestamp from which a token is valid.
        /// </summary>
        public long NotBefore { get; set; }

        /// <summary>
        /// Gets or sets a claim representing when a token expires as a Unix-timestamp.
        /// </summary>
        public long ExpiresAt { get; set; }
    }
}
