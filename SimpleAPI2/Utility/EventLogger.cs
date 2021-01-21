using SimpleAPI2.Utility.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleAPI2.Utility
{
    public static class EventLogger
    {
        private static string ApplicationName = System.Configuration.ConfigurationManager.AppSettings[AppConstants.ApplicationName].ToString();
        public static void WriteEventViewerLog(string function, string log, int eventid)
        {
            using (System.Diagnostics.EventLog eventLog = new System.Diagnostics.EventLog(AppConstants.LogSource))
            {
                eventLog.Source = AppConstants.LogSource;
                eventLog.WriteEntry(ApplicationName + "-" + function + " : " + log, System.Diagnostics.EventLogEntryType.Information, eventid);
            }
        }
    }
}