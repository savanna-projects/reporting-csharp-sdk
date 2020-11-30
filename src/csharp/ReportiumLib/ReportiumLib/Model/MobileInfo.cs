using System.Runtime.Serialization;

namespace Reportium.model
{
    [DataContract]
    public class MobileInfo
    {
        [DataMember]
        public string Imei { get; set; }

        [DataMember]
        public string Imsi { get; set; }

        [DataMember]
        public string Manufacturer { get; set; }

        [DataMember]
        public string Model { get; set; }

        [DataMember]
        public string PhoneNumber { get; set; }

        [DataMember]
        public string Distributor { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Firmware { get; set; } // SW Version

        [DataMember]
        public string Operator { get; set; }

        [DataMember]
        public string OperatorCountry { get; set; }

        [DataMember]
        public string Email { get; set; }
    }
}