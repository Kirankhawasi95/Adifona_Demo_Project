namespace HorusUITest.Data
{
    using HorusUITest.Configuration;

    public class AndroidPhone : BasePhone
    {
        public AndroidPhone(string deviceName, string udid, string platformVersion)
        {
            DeviceName = deviceName.Length > 0 ? deviceName : "Android Device";
            Udid = udid;
            PlatformVersion = platformVersion.Length > 0 ? platformVersion : "Android";
        }

        public AndroidPhone(string udid)
        {
            DeviceName = "Android Device";
            Udid = udid;
            PlatformVersion = "Android";
        }
    }
}
