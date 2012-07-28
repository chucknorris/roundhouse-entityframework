using System;

namespace __NAME__.Infrastructure.Services
{
    /// <summary>
    /// Uses information from the system
    /// </summary>
    class SystemDateTimeService : IDateTimeService
    {
        /// <summary>
        /// Gets the system date time
        /// </summary>
        /// <returns>The current system date and time</returns>
        public DateTime? GetCurrentDateTime()
        {
            return DateTime.Now;
        }
    }
}