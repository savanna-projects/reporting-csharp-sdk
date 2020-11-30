using Reportium.Models;

using System.Runtime.Serialization;

namespace Reportium.model
{
    /// <summary>
    /// Platform class
    /// </summary>
    /// <remarks>Platform to be specified in the report</remarks>
    [DataContract]
    public class DevicePlatform
    {
        public DevicePlatform(PlatformBuilder platformBuilder)
        {
            DeviceType = platformBuilder.deviceType;
            DeviceId = platformBuilder.deviceId;
            OS = platformBuilder.os;
            OsVersion = platformBuilder.osVersion;
            ScreenResolution = platformBuilder.screenResolution;
            Location = platformBuilder.location;
            MobileInfo = platformBuilder.mobileInfo;
            BrowserInfo = platformBuilder.browserInfo;
        }

        [DataMember]
        public string DeviceId { get; set; }

        [DataMember]
        public string DeviceType { get; set; }

        [DataMember]
        public string OS { get; set; }

        [DataMember]
        public string OsVersion { get; set; }

        [DataMember]
        public string ScreenResolution { get; set; }

        [DataMember]
        public string Location { get; set; }

        [DataMember]
        public MobileInfo MobileInfo { get; set; }

        [DataMember]
        public BrowserInfo BrowserInfo { get; set; }

        public override bool Equals(object obj)
        {
            if (this == obj)
            {
                return true;
            }
            if (obj == null || this.GetType() != obj.GetType())
            {
                return false;
            }

            var platform = (DevicePlatform)obj;

            if (DeviceType != platform.DeviceType)
            {
                return false;
            }
            if (!OS.Equals(platform.OS))
            {
                return false;
            }
            if (OsVersion != null ? !OsVersion.Equals(platform.OsVersion) : platform.OsVersion != null)
            {
                return false;
            }
            if (ScreenResolution != null ? !ScreenResolution.Equals(platform.ScreenResolution) : platform.ScreenResolution != null)
            {
                return false;
            }
            if (Location != null ? !Location.Equals(platform.Location) : platform.Location != null)
            {
                return false;
            }
            if (MobileInfo != null ? !MobileInfo.Equals(platform.MobileInfo) : platform.MobileInfo != null)
            {
                return false;
            }
            return BrowserInfo != null ? BrowserInfo.Equals(platform.BrowserInfo) : platform.BrowserInfo == null;
        }

        public override int GetHashCode()
        {
            // setup
            int result = DeviceType.Equals(model.DeviceType.Desktop) ? DeviceType.GetHashCode() : 0;
            result = (31 * result) + OS.GetHashCode();
            result = (31 * result) + ((OsVersion?.GetHashCode()) ?? 0);
            result = (31 * result) + ((ScreenResolution?.GetHashCode()) ?? 0);
            result = (31 * result) + ((Location?.GetHashCode()) ?? 0);
            result = (31 * result) + ((MobileInfo?.GetHashCode()) ?? 0);
            result = (31 * result) + ((BrowserInfo?.GetHashCode()) ?? 0);

            // get
            return result;
        }
    }
}