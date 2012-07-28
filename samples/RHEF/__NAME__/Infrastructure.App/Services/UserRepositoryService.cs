using System;
using System.Linq;
using EnsureThat;
using __NAME__.Domain;
using __NAME__.Infrastructure.Persistence;
using __NAME__.Infrastructure.Services;

namespace __NAME__.Infrastructure.App.Services
{
    /// <summary>
    /// Houses the queries against users in the database
    /// </summary>
    public class UserRepositoryService : BaseRepositoryService<SampleUser>, IUserRepositoryService
    {
        private readonly IDateTimeService _dateTimeService;

        /// <summary>
        ///   Initializes a new instance of the <see cref="UserRepositoryService" /> class.
        /// </summary>
        /// <param name="repository"> The repository. </param>
        /// <param name="dateTimeService"> The date time service </param>
        public UserRepositoryService(IRepository repository, IDateTimeService dateTimeService)
            : base(repository)
        {
            _dateTimeService = dateTimeService;
        }

        public SampleUser GetUserByEmailAddress(string email)
        {
            Ensure.That(() => email).IsNotNullOrWhiteSpace();

            this.Log().Debug("Looking for '{0}' (email address) in the database.", email);

            return Repository.GetAll<SampleUser>()
                .Where(u => u.Email == email)
                .SingleOrDefault();
        }

        /// <summary>
        /// Updates the last login date.
        /// </summary>
        /// <param name="appUser">The app user.</param>
        public void UpdateLastLogin(SampleUser appUser)
        {
            appUser.LastLoginDate = _dateTimeService.GetCurrentDateTime();
            Repository.CommitChanges();
        }

        public SampleUser GetUserByKey(Guid userKey)
        {
            Ensure.That(() => userKey).IsNotEmpty();

            this.Log().Debug("Looking for user by guid '{0}' in the database.", userKey.ToString());

            return Repository
                .GetAll<SampleUser>()
                .SingleOrDefault(u => u.UserKey == userKey);
        }
    }
}