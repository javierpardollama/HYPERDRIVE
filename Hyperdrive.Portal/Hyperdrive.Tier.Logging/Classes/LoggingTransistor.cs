using System;

using Hyperdrive.Tier.Constants.Enums;
using Hyperdrive.Tier.Mappings.Classes;

using Microsoft.Extensions.Logging;

namespace Hyperdrive.Tier.Logging.Classes
{
    public static class LoggingTransistor
    {
        private const LogLevel DefaultLogLevel = LogLevel.None;

        private static void Emit(this ILogger @this,
                                 Enum appEventData,
                                 string logData) => @this.Log(
                GetApplicationEventLevel(appEventData),
                GetApplicationEventCode(appEventData),
                logData,
                DateTime.Now.ToShortDateString());

        public static void WriteGetItemNotFoundLog(this ILogger @this,
                                                   string logData)
        {
            @this.Emit(ApplicationEvents.GetItemNotFound, logData);

            WriteErrorDiagnostics(logData);
        }

        public static void WriteUpdateItemLog(this ILogger @this,
                                              string logData)
        {
            @this.Emit(ApplicationEvents.UpdateItem, logData);

            WriteInformationDiagnostics(logData);
        }

        public static void WriteDeleteItemLog(this ILogger @this,
                                              string logData)
        {
            @this.Emit(ApplicationEvents.DeleteItem, logData);

            WriteInformationDiagnostics(logData);
        }

        public static void WriteInsertItemLog(this ILogger @this,
                                              string logData)
        {
            @this.Emit(ApplicationEvents.InsertItem, logData);

            WriteInformationDiagnostics(logData);
        }

        public static void WriteGetItemFoundLog(this ILogger @this,
                                                string logData)
        {
            @this.Emit(ApplicationEvents.GetItemFound, logData);

            WriteErrorDiagnostics(logData);
        }

        public static void WriteUserAuthenticatedLog(this ILogger @this,
                                                     string logData)
        {
            @this.Emit(ApplicationEvents.UserAuthenticated, logData);

            WriteInformationDiagnostics(logData);
        }

        public static void WriteUserNotAuthenticatedLog(this ILogger @this,
                                                        string logData)
        {
            @this.Emit(ApplicationEvents.UserNotAuthenticated, logData);

            WriteErrorDiagnostics(logData);
        }

        public static void WritePasswordRestoredLog(this ILogger @this,
                                                        string logData)
        {
            @this.Emit(ApplicationEvents.PasswordRestored, logData);

            WriteInformationDiagnostics(logData);
        }

        public static void WriteEmailRestoredLog(this ILogger @this,
                                                       string logData)
        {
            @this.Emit(ApplicationEvents.EmailRestored, logData);

            WriteInformationDiagnostics(logData);
        }

        /// <summary>
        /// Writes Information Diagnostics
        /// </summary>
        /// <param name="logData">Injected <see cref="string"/></param>
        private static void WriteInformationDiagnostics(string @logData) => System.Diagnostics.Trace.TraceInformation(@logData);

        /// <summary>
        /// Writes Information Diagnostics
        /// </summary>
        /// <param name="logData">Injected <see cref="string"/></param>
        private static void WriteErrorDiagnostics(string @logData) => System.Diagnostics.Trace.TraceError(@logData);

        private static int GetApplicationEventCode(Enum appEventData) => (int)Convert.ChangeType(appEventData, appEventData.GetTypeCode());

        private static LogLevel GetApplicationEventLevel(Enum appEventData)
        {
            if (LoggingProfile.LogLevelMapings.ContainsKey(appEventData))
            {
                return LoggingProfile.LogLevelMapings[appEventData];
            }
            else
            {
                return DefaultLogLevel;
            }
        }
    }
}