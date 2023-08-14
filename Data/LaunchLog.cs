using System;
namespace HorusUITest.Data
{
    public struct LaunchLog
    {
        public Type InitialPageType;
        public bool WasOnInitializeHardwarePage;
        public int ConfirmedAppDialogs;
        public int AllowedPermissions;
        public int HearingAidConnectionAttempts;

        public static LaunchLog Default
        {
            get
            {
                LaunchLog launchLog = new LaunchLog();
                launchLog.InitialPageType = null;
                launchLog.WasOnInitializeHardwarePage = false;
                launchLog.ConfirmedAppDialogs = 0;
                launchLog.AllowedPermissions = 0;
                launchLog.HearingAidConnectionAttempts = 0;
                return launchLog;
            }
        }

        public void Add(int newAppDialogs, int newPermissions)
        {
            ConfirmedAppDialogs += newAppDialogs;
            AllowedPermissions += newPermissions;
        }
    }
}
