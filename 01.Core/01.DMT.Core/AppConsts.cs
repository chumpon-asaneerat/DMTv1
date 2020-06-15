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
                public static string ApplicationName = @"DMT Toll Admin Application";
                public static string Version = "1";
                public static string Minor = "0";
                public static string Build = "125";
                public static DateTime LastUpdate = new DateTime(2020, 06, 13, 10, 00, 00);
            }
            public static class TOD
            {
                public static string ApplicationName = @"DMT Toll of Duty Application";
                public static string Version = "1";
                public static string Minor = "0";
                public static string Build = "125";
                public static DateTime LastUpdate = new DateTime(2020, 06, 13, 10, 00, 00);
            }
            public static class Account
            {
                public static string ApplicationName = @"DMT Toll Account Application";
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
                public static string ApplicationName = @"DMT TOD-TA Plaza Simulator";
                public static string Version = "1";
                public static string Minor = "0";
                public static string Build = "125";
                public static DateTime LastUpdate = new DateTime(2020, 06, 13, 10, 00, 00);
            }
        }
        public static class WindowsService
        {
            public static class Plaza
            {
                public static string ServiceName = "DMT Plaza Windows Service";
                public static string DisplayName = "DMT Plaza Windows Service";
                public static string Description = "DMT Plaza Windows Service";
                public static string ExecutableFileName = @"DMT.Plaza.Windows.Services.exe";
                public static string Version = "1";
                public static string Minor = "0";
                public static string Build = "125";
                public static DateTime LastUpdate = new DateTime(2020, 06, 13, 10, 00, 00);

                public static class LocaWebServer
                {
                    public static int PortNumber = 9000;
                }
            }
        }
    }
}
