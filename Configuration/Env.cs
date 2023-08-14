using System;

namespace HorusUITest.Configuration
{
    public static class Env
    {
        public static TimeSpan INIT_TIMEOUT = TimeSpan.FromSeconds(180);
        public static TimeSpan IMPLICIT_TIMEOUT = TimeSpan.FromSeconds(0);
        public static TimeSpan DEFAULT_EXPLICIT_TIMEOUT = TimeSpan.FromSeconds(35);
        public static TimeSpan OPTIONAL_DIALOG_TIMEOUT= TimeSpan.FromSeconds(5);
        public static TimeSpan OPTIONAL_PERMISSION_TIMEOUT= TimeSpan.FromSeconds(5);
        public static TimeSpan DIALOG_GONE_MAXDURATION = TimeSpan.FromSeconds(1);
        public static TimeSpan DEFAULT_LOADING_INDICATOR_TIMEOUT= TimeSpan.FromSeconds(45);
        public const int DEFAULT_MAX_SWIPES_TO_SCROLL = 10;

        public static bool ReportInconclusiveAsFailed = true;   //TFS 2015 is unable to show messages associated with inconclusive test results. As a workaround, inconclusive result can be reported as failed. This variable can be overriden by the CLI parameter with the same name.
    }
}
