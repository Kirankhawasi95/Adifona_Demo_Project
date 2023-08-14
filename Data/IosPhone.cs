namespace HorusUITest.Data
{
    public class IosPhone : BasePhone
    {
        public IosPhone(string deviceName, string udid, string platformVersion)
        {
            DeviceName = deviceName.Length > 0 ? deviceName : "iOS Device";
            Udid = udid;
            PlatformVersion = platformVersion.Length > 0 ? platformVersion : "iOS";
        }

        public IosPhone(string udid)
        {
            DeviceName = "iOS Device";
            Udid = udid;
            PlatformVersion = "iOS";
        }
    }
}
