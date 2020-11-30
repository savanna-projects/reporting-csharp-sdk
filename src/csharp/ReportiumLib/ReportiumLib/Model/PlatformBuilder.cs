using Reportium.model;

namespace Reportium.Models
{
    /// <summary>
    /// Build a Platform object using a given configuration
    /// </summary>
    public class PlatformBuilder
    {
        internal string deviceType;
        internal string deviceId;
        internal string os;
        internal string osVersion;
        internal string screenResolution;
        internal string location;
        internal MobileInfo mobileInfo;
        internal BrowserInfo browserInfo;

        public PlatformBuilder WithDeviceType(DeviceType deviceType)
        {
            this.deviceType = deviceType.ToString();
            return this;
        }

        public PlatformBuilder WithDeviceId(string deviceId)
        {
            this.deviceId = deviceId;
            return this;
        }

        public PlatformBuilder WithOs(DeviceOperatingSystem os)
        {
            this.os = os.ToString();
            return this;
        }

        public PlatformBuilder WithOsVersion(string osVersion)
        {
            this.osVersion = osVersion;
            return this;
        }

        public PlatformBuilder WithScreenResolution(string screenResolution)
        {
            this.screenResolution = screenResolution;
            return this;
        }

        public PlatformBuilder WithLocation(string location)
        {
            this.location = location;
            return this;
        }

        public PlatformBuilder WithDeviceInfo(MobileInfo mobileInfo)
        {
            this.mobileInfo = mobileInfo;
            return this;
        }

        public PlatformBuilder WithBrowserInfo(BrowserInfo browserInfo)
        {
            this.browserInfo = browserInfo;
            return this;
        }

        public DevicePlatform Build()
        {
            return new DevicePlatform(this);
        }
    }
}
