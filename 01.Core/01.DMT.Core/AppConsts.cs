using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMT
{
    public static class AppConsts
    {
        public static class Application
        {
            public static class TA
            {
                public static string ApplicationName = @"DMT TA Application";
                public static string Version = "1";
                public static string Minor = "0";
                public static string Build = "125";
                public static DateTime LastUpdate = new DateTime(2020, 06, 13, 10, 00, 00);
            }
            public static class TOD
            {
                public static string ApplicationName = @"DMT TOD Application";
                public static string Version = "1";
                public static string Minor = "0";
                public static string Build = "125";
                public static DateTime LastUpdate = new DateTime(2020, 06, 13, 10, 00, 00);
            }
            public static class PlazaConfig
            {
                public static string ApplicationName = @"DMT TOD-TA Plaza Config";
                public static string Version = "1";
                public static string Minor = "0";
                public static string Build = "125";
                public static DateTime LastUpdate = new DateTime(2020, 06, 13, 10, 00, 00);
            }
            public static class PlazaSumulator
            {
                public static string ApplicationName = @"DMT Plaza Simulator";
                public static string Version = "1";
                public static string Minor = "0";
                public static string Build = "125";
                public static DateTime LastUpdate = new DateTime(2020, 06, 13, 10, 00, 00);
            }
        }
        public static class WindowsService
        {
            public static class TA
            {
                public static string ServiceName = "TA Local Web Service";
                public static string DisplayName = "TA Local Web Service";
                public static string Description = "Toll Admin Local Web Service";
                public static string ExecutableFileName = @"DMT.TOD.Windows.Services.exe";
                public static string Version = "1";
                public static string Minor = "0";
                public static string Build = "125";
                public static DateTime LastUpdate = new DateTime(2020, 06, 13, 10, 00, 00);
            }
            public static class TOD
            {
                public static string ServiceName = "TOD Local Web Service";
                public static string DisplayName = "TOD Local Web Service";
                public static string Description = "Toll of Duty Local Web Service";
                public static string ExecutableFileName = @"DMT.TA.Windows.Services.exe";
                public static string Version = "1";
                public static string Minor = "0";
                public static string Build = "125";
                public static DateTime LastUpdate = new DateTime(2020, 06, 13, 10, 00, 00);
            }
        }
    }
}
