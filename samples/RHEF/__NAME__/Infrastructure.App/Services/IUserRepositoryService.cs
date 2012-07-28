using System;
using __NAME__.Domain;
using __NAME__.Infrastructure.Services;

namespace __NAME__.Infrastructure.App.Services
{
    /// <summary>
    ///   Service that houses queries against users
    /// </summary>
    public interface IUserRepositoryService : IRepositoryService<SampleUser>
    {
        /// <summary>
        ///   Gets the user by email address.
        /// </summary>
        /// <param name = "email">The email.</param>
        /// <returns>An instance of <see cref = "SampleUser" /> that meets the key; otherwise null</returns>
        SampleUser GetUserByEmailAddress(string email);

        /// <summary>
        /// Updates the last login date.
        /// </summary>
        /// <param name="appUser">The app user.</param>
        void UpdateLastLogin(SampleUser appUser);

        /// <summary>
        /// Gets the user by the user key.
        /// </summary>
        /// <param name="userKey">The user key.</param>
        /// <returns>An instance of <see cref = "SampleUser" /> that meets the key; otherwise null</returns>
        SampleUser GetUserByKey(Guid userKey);
    }
}