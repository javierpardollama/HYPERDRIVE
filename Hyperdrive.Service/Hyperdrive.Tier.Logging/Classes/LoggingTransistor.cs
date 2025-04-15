using System;

using Hyperdrive.Tier.Constants.Enums;
using Hyperdrive.Tier.Mappings.Classes;

using Microsoft.Extensions.Logging;

namespace Hyperdrive.Tier.Logging.Classes
{
    /// <summary>
    /// Represents a <see cref="LoggingTransistor"/> class
    /// </summary>
    public static class LoggingTransistor
    {
        /// <summary>
        /// Emits
        /// </summary>
        /// <param name="this">Injected <see cref="ILogger"/></param>
        /// <param name="appEventData">Injected <see cref="Enum"/></param>
        /// <param name="logData">Injected <see cref="string"/></param>
        private static void Emit(this ILogger @this,
                                 Enum appEventData,
                                 string logData) => @this.Log(
                LoggingProfile.Map(@appEventData),
                GetApplicationEventCode(appEventData),
                logData,
                DateTime.UtcNow.ToShortDateString());

        /// <summary>
        /// Emits Not Found
        /// </summary>
        /// <param name="this">Injected <see cref="ILogger"/></param>
        /// <param name="logData">Injected <see cref="string"/></param>
        public static void WriteGetItemNotFoundLog(this ILogger @this,
                                                   string logData)
        {
            @this.Emit(ApplicationEvents.GetItemNotFound, logData);

            WriteErrorDiagnostics(logData);
        }

        /// <summary>
        /// Emits Update
        /// </summary>
        /// <param name="this">Injected <see cref="ILogger"/></param>
        /// <param name="logData">Injected <see cref="string"/></param>
        public static void WriteUpdateItemLog(this ILogger @this,
                                              string @logData)
        {
            @this.Emit(ApplicationEvents.UpdateItem, @logData);

            WriteInformationDiagnostics(@logData);
        }

        /// <summary>
        /// Emits Delete
        /// </summary>
        /// <param name="this">Injected <see cref="ILogger"/></param>
        /// <param name="logData">Injected <see cref="string"/></param>
        public static void WriteDeleteItemLog(this ILogger @this,
                                              string @logData)
        {
            @this.Emit(ApplicationEvents.DeleteItem, @logData);

            WriteInformationDiagnostics(@logData);
        }

        /// <summary>
        /// Emits Insert
        /// </summary>
        /// <param name="this">Injected <see cref="ILogger"/></param>
        /// <param name="logData">Injected <see cref="string"/></param>
        public static void WriteInsertItemLog(this ILogger @this,
                                              string @logData)
        {
            @this.Emit(ApplicationEvents.InsertItem, @logData);

            WriteInformationDiagnostics(@logData);
        }

        /// <summary>
        /// Emits Found
        /// </summary>
        /// <param name="this">Injected <see cref="ILogger"/></param>
        /// <param name="logData">Injected <see cref="string"/></param>
        public static void WriteGetItemFoundLog(this ILogger @this,
                                                string @logData)
        {
            @this.Emit(ApplicationEvents.GetItemFound, @logData);

            WriteErrorDiagnostics(@logData);
        }

        /// <summary>
        /// Emits Authenticated
        /// </summary>
        /// <param name="this">Injected <see cref="ILogger"/></param>
        /// <param name="logData">Injected <see cref="string"/></param>
        public static void WriteUserAuthenticatedLog(this ILogger @this,
                                                     string @logData)
        {
            @this.Emit(ApplicationEvents.UserAuthenticated, @logData);

            WriteInformationDiagnostics(@logData);
        }

        /// Emits Unauthenticated
        /// </summary>
        /// <param name="this">Injected <see cref="ILogger"/></param>
        /// <param name="logData">Injected <see cref="string"/></param>
        public static void WriteUserUnauthenticatedLog(this ILogger @this,
                                                     string @logData)
        {
            @this.Emit(ApplicationEvents.UserUnauthenticated, @logData);

            WriteInformationDiagnostics(@logData);
        }

        /// <summary>
        /// Emits Not Authenticated
        /// </summary>
        /// <param name="this">Injected <see cref="ILogger"/></param>
        /// <param name="logData">Injected <see cref="string"/></param>
        public static void WriteUserNotAuthenticatedLog(this ILogger @this,
                                                        string @logData)
        {
            @this.Emit(ApplicationEvents.UserNotAuthenticated, @logData);

            WriteErrorDiagnostics(@logData);
        }

        /// <summary>
        /// Emits Password Restored
        /// </summary>
        /// <param name="this">Injected <see cref="ILogger"/></param>
        /// <param name="logData">Injected <see cref="string"/></param>
        public static void WritePasswordRestoredLog(this ILogger @this,
                                                        string @logData)
        {
            @this.Emit(ApplicationEvents.PasswordRestored, @logData);

            WriteInformationDiagnostics(@logData);
        }

        /// <summary>
        /// Emits Email Restored
        /// </summary>
        /// <param name="this">Injected <see cref="ILogger"/></param>
        /// <param name="logData">Injected <see cref="string"/></param>
        public static void WriteEmailRestoredLog(this ILogger @this,
                                                       string @logData)
        {
            @this.Emit(ApplicationEvents.EmailRestored, @logData);

            WriteInformationDiagnostics(@logData);
        }

        /// <summary>
        /// Emits Phone Number Restored
        /// </summary>
        /// <param name="this">Injected <see cref="ILogger"/></param>
        /// <param name="logData">Injected <see cref="string"/></param>
        public static void WritePhoneNumberRestoredLog(this ILogger @this,
                                                       string @logData)
        {
            @this.Emit(ApplicationEvents.PhoneNumberRestored, @logData);

            WriteInformationDiagnostics(@logData);
        }

        /// <summary>
        /// Emits Refresh Token Revoked
        /// </summary>
        /// <param name="this">Injected <see cref="ILogger"/></param>
        /// <param name="logData">Injected <see cref="string"/></param>
        public static void WriteRefreshTokenRevokedLog(this ILogger @this,
                                                       string @logData)
        {
            @this.Emit(ApplicationEvents.RefreshTokenRevoked, @logData);

            WriteInformationDiagnostics(@logData);
        }

        /// <summary>
        /// Emits Tokens Refreshed
        /// </summary>
        /// <param name="this">Injected <see cref="ILogger"/></param>
        /// <param name="logData">Injected <see cref="string"/></param>
        public static void WriteTokensRefreshedLog(this ILogger @this,
                                                       string @logData)
        {
            @this.Emit(ApplicationEvents.TokensRefreshed, @logData);

            WriteInformationDiagnostics(@logData);
        }

        /// <summary>
        /// Writes Information Diagnostics
        /// </summary>
        /// <param name="logData">Injected <see cref="string"/></param>
        private static void WriteInformationDiagnostics(string @logData) => System.Diagnostics.Trace.TraceInformation(@logData);

        /// <summary>
        /// Writes Error Diagnostics
        /// </summary>
        /// <param name="logData">Injected <see cref="string"/></param>
        private static void WriteErrorDiagnostics(string @logData) => System.Diagnostics.Trace.TraceError(@logData);

        /// <summary>
        /// Gets Application Event Code
        /// </summary>
        /// <param name="appEventData">Injected <see cref="Enum"/></param>
        /// <returns>Instance of <see cref="int"/></returns>
        private static int GetApplicationEventCode(Enum appEventData) => (int)Convert.ChangeType(appEventData, appEventData.GetTypeCode());
    }
}