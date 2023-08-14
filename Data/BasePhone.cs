namespace HorusUITest.Data
{
    public abstract class BasePhone
    {
        public string DeviceName { get; protected set; }
        public string Udid { get; protected set; }
        public string PlatformVersion { get; protected set; }

        public override string ToString()
        {
            return $"{DeviceName} {Udid}";
        }
    }
}
