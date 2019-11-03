using System;
using System.Diagnostics;
using Zek.Windows.Forms;
using Zek.Core;

namespace Zek.Utils
{
    public class EventLogHelper
    {
        #region Private utility methods & constructors

        //Since this class provides only static methods, make the default constructor private to prevent 
        //instances from being created with "new EventLogHelper()".
        private EventLogHelper() { }

        static EventLogHelper()
        {
            _defaultSource = AssemblyHelper.AssemblyTitle;
        }


        public static void ExceptionLog(Exception exception)
        {
            try
            {

                EventLog.WriteEntry(DefaultSource, ExceptionHelper.GetExceptionString(exception), EventLogEntryType.Warning);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }

        #endregion

        #region Public methods
        #region WriteEntry
        /// <summary>
        /// Writes an entry in the event log.
        /// </summary>
        /// <param name="message">The string to write to the event log.</param>
        public static void WriteEntry(string message)
        {
            WriteEntry(message, EventLogEntryType.Information, DefaultSource, DefaultLogName, 0);
        }

        /// <summary>
        /// Writes an entry in the event log.
        /// </summary>
        /// <param name="message">The string to write to the event log.</param>
        /// <param name="eventType">One of the EventLogEntryType values.</param>
        public static void WriteEntry(string message, EventLogEntryType eventType)
        {
            WriteEntry(message, eventType, DefaultSource, DefaultLogName, 0);
        }

        /// <summary>
        /// Writes an entry in the event log.
        /// </summary>
        /// <param name="message">The string to write to the event log.</param>
        /// <param name="eventType">One of the EventLogEntryType values.</param>
        /// <param name="logsource">The source by which the application is registered on the specified computer.</param>
        public static void WriteEntry(string message, EventLogEntryType eventType, string logsource)
        {
            WriteEntry(message, eventType, logsource, DefaultLogName, 0);
        }

        /// <summary>
        /// Writes an entry in the event log.
        /// </summary>
        /// <param name="message">The string to write to the event log.</param>
        /// <param name="eventType">One of the EventLogEntryType values.</param>
        /// <param name="logsource">The source by which the application is registered on the specified computer.</param>
        /// <param name="logName">
        /// The name of the log the source's entries are written to. 
        /// Possible values include: Application, Security, System, or a custom event log. 
        /// If you do not specify a value, the logName defaults to Application. 
        /// </param>
        public static void WriteEntry(string message, EventLogEntryType eventType, string logsource, string logName)
        {
            WriteEntry(message, eventType, logsource, logName, 0);
        }

        /// <summary>
        /// Writes an entry in the event log.
        /// </summary>
        /// <param name="message">The string to write to the event log.</param>
        /// <param name="eventType">One of the EventLogEntryType values.</param>
        /// <param name="logsource">The source by which the application is registered on the specified computer.</param>
        /// <param name="logName">
        /// The name of the log the source's entries are written to. 
        /// Possible values include: Application, Security, System, or a custom event log. 
        /// If you do not specify a value, the logtype defaults to Application. 
        /// </param>
        /// <param name="eventID">The application-specific identifier for the event.</param>
        public static void WriteEntry(string message, EventLogEntryType eventType, string logsource, string logName, int eventID)
        {
            if (string.IsNullOrEmpty(logsource))
                logsource = DefaultSource;

            try
            {
                if (!EventLog.SourceExists(logsource))
                    EventLog.CreateEventSource(logsource, logName);

                EventLog.WriteEntry(logsource, message, eventType, eventID, 0);
            }
            catch (Exception e)
            {
                ExceptionLog(e);
            }
        }
        #endregion

        #region DeleteEventLog
        /// <summary>
        /// Removes the LogType.
        /// </summary>
        public static void DeleteEventLog()
        {
            DeleteEventLog(DefaultLogName);
        }

        /// <summary>
        /// Removes an event log from the local computer.
        /// </summary>
        /// <param name="logName">
        /// The name of the log the source's entries are written to. 
        /// Possible values include: Application, Security, System, or a custom event log. 
        /// If you do not specify a value, the logName defaults to Application. 
        /// </param>
        public static void DeleteEventLog(string logName)
        {
            try
            {
                EventLog.Delete(logName);
            }
            catch (Exception e)
            {
                ExceptionLog(e);
            }
        }
        #endregion

        #region RemoveEventSource
        /// <summary>
        /// Removes an event log from the local computer.
        /// </summary>
        public static void DeleteEventSource()
        {
            DeleteEventSource(DefaultSource);
        }

        /// <summary>
        /// Removes the event source registration from the event log of the local computer.
        /// </summary>
        /// <param name="logsource">The source by which the application is registered on the specified computer.</param>
        public static void DeleteEventSource(string logsource)
        {
            try
            {
                EventLog.DeleteEventSource(logsource);
            }
            catch (Exception e)
            {
                ExceptionLog(e);
            }
        }
        #endregion
        #endregion

        #region Public properties
        /// <summary>
        /// Default Log Type.
        /// </summary>
        public static string DefaultLogName => "Application";

        private static readonly string _defaultSource;
        /// <summary>
        /// Default Log Source.
        /// </summary>
        public static string DefaultSource => _defaultSource;

        #endregion
    }


}
