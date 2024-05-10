using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SystemSetting
/// </summary>
public class SystemSetting
{
    public SystemSetting()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static string EventSource
    {
        get
        {
            try
            {
                return SystemUtility.Configuration.GetAppSetting(InsuranceLibraryConfiguration, "EventSource");
            }
            catch
            {
                return "CLIBMicrositeWS";
            }
        }
    }
    public static string EventLogDirectory
    {
        get
        {
            try
            {
                return SystemUtility.Configuration.GetAppSetting(InsuranceLibraryConfiguration, "EventLogDirectory");
            }
            catch
            {
                return @"C:\EventLogs\CLIBMicrositeWS";
            }
        }
    }
    public static System.Configuration.Configuration InsuranceLibraryConfiguration
    {
        get { return SystemUtility.Configuration.GetLocalConfiguration(System.Reflection.Assembly.GetExecutingAssembly().Location); }
    }
}