using System;
using System.Collections.Generic;

namespace DMT
{
    public static class RouteConsts
    {
        public const string Url = @"api";

        public static class Job
        {
            public const string Url = RouteConsts.Url + @"/Job";

            public static class BeginJob
            {
                public const string Name = "BeginJob";
                public const string Url = Job.Url + @"/" + Name;
            }

            public static class EndJob
            {
                public const string Name = "EndJob";
                public const string Url = Job.Url + @"/" + Name;
            }
        }
    }
}
