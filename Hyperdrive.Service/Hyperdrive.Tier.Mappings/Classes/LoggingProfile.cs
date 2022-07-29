using Hyperdrive.Tier.Constants.Enums;

using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;

namespace Hyperdrive.Tier.Mappings.Classes
{
    /// <summary>
    /// Represents a <see cref="LoggingProfile"/> class
    /// </summary>
    public static class LoggingProfile
    {
        /// <summary>
        /// Instance of <see cref="Dictionary{Enum, LogLevel}"/>
        /// </summary>
        public static readonly Dictionary<Enum, LogLevel> LogLevelMapings = new()
        {
    { ApplicationEvents.InsertItem, LogLevel.Information },
    { ApplicationEvents.UpdateItem, LogLevel.Information },
    { ApplicationEvents.DeleteItem, LogLevel.Information },
    { ApplicationEvents.GetItemNotFound, LogLevel.Error },
    { ApplicationEvents.GetItemFound, LogLevel.Error },
    { ApplicationEvents.UserAuthenticated, LogLevel.Information },
    { ApplicationEvents.UserNotAuthenticated, LogLevel.Error },
    { ApplicationEvents.PasswordRestored, LogLevel.Information },
    { ApplicationEvents.EmailRestored, LogLevel.Information },
    };
    }
}