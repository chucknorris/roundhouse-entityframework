using System;

namespace __NAME__.Infrastructure.Services
{
    /// <summary>
    ///   This handles date/time information
    /// </summary>
    public interface IDateTimeService
    {
        /// <summary>
        ///   Gets the current date time.
        /// </summary>
        DateTime? GetCurrentDateTime();
    }
}