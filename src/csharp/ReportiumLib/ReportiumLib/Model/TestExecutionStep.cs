using System.Runtime.Serialization;

namespace Reportium.model
{
    [DataContract]
    public class TestExecutionStep
    {
        [DataMember]
        public string Description { get; set; }
    }
}