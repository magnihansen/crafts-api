using System;
using System.Collections.Generic;

namespace CraftsApi.Auth
{
    /// <summary>
    /// An exception thrown when a precondition assertion fails.
    /// </summary>
    public class PreconditionException : InvalidOperationException
    {
        public PreconditionException(string message = null)
            : base(message) { }
    }

    /// <summary>
    /// An exception thrown when a postcondition assertion fails.
    /// </summary>
    public class PostconditionException : InvalidOperationException
    {
        public PostconditionException(string message = null)
            : base(message) { }
    }

    /// <summary>
    /// A design-by-contract inspired Check class, used to quickly add runtime assertions to the code.
    /// Throws exceptions if assetions fail.
    /// </summary>
    public static class Check
    {
        /// <summary>
        /// Asserts that a required precondition is true.
        /// </summary>
        /// <exception cref="PreconditionException"></exception>
        public static void Requires(bool assertion, string message = null)
        {
            if (!assertion)
            {
                throw new PreconditionException(message ?? "Precondition contract failed.");
            }
        }

        /// <summary>
        /// Asserts whether a postcondition is true.
        /// </summary>
        /// <exception cref="PostconditionException"></exception>
        public static void Ensures(bool assertion, string message = null)
        {
            if (!assertion)
            {
                throw new PostconditionException(message ?? "Postcondition contract failed.");
            }
        }

        /// <summary>
        /// Assert whether an argument is null.
        /// Returns the argument if the assertion passes, otherwise it throws an exception.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public static T NotNull<T>(T value, string parameterName)
        {
            if (!ReferenceEquals(value, null))
            {
                return value;
            }

            throw new ArgumentNullException(parameterName);
        }

        /// <summary>
        /// Assert whether a property of an argument is null. 
        /// Returns the argument if the assertion passes, otherwise it throws an exception.
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        public static T NotNull<T>(T value, string parameterName, string propertyName)
        {
            if (!ReferenceEquals(value, null))
            {
                return value;
            }

            throw new ArgumentException($"The property '{propertyName}' of the argument '{parameterName}' cannot be null.", parameterName);
        }

        /// <summary>
        /// Assert whether a string is null or empty. 
        /// Returns the string argument if the assertion passes, otherwise it throws an exception.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static string NotEmpty(string value, string parameterName)
        {
            if (value is null)
            {
                throw new ArgumentNullException(parameterName);
            }
            else if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException($"The string argument '{parameterName}' cannot be empty.", parameterName);
            }

            return value;
        }

        /// <summary>
        /// Assert whether a string is null or empty. 
        /// Returns the string argument if the assertion passes, otherwise it throws an exception.
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        public static string NotEmpty(string value, string parameterName, string propertyName)
        {
            if (value is null)
            {
                throw new ArgumentException($"The property '{propertyName}' of the argument '{parameterName}' cannot be null.", parameterName);
            }
            else if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException($"The property '{propertyName}' of the argument '{parameterName}' cannot be empty.", parameterName);
            }

            return value;
        }

        /// <summary>
        /// Assert whether a collection is null or empty. 
        /// Returns the collection argument if the assertion passes, otherwise it throws an exception.
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public static ICollection<T> NotEmpty<T>(ICollection<T> value, string parameterName, string propertyName = null)
        {
            if (!(propertyName is null))
            {
                NotNull(value, parameterName, propertyName);
            }
            else
            {
                NotNull(value, parameterName);
            }

            if (value.Count != 0)
            {
                return value;
            }

            throw new ArgumentException($"The collection {(propertyName != null ? $"property '{propertyName}' of the " : string.Empty)}argument '{parameterName}' must contain at least one element.", parameterName);
        }
    }
}
