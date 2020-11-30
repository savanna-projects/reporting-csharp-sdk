using Newtonsoft.Json;

using Reportium.Model;

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Reportium.model
{
    /// <summary>
    /// Denotes a test execution instance
    /// </summary>
    [DataContract]
    public class TestExecution
    {
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Owner { get; set; }

        [DataMember]
        public string ExternalId { get; set; }

        [DataMember]
        public string CreatedBy { get; set; }

        [DataMember]
        public long CreatedAt { get; set; }

        [DataMember]
        public string LastUpdatedBy { get; set; }

        [DataMember]
        public long LastUpdatedAt { get; set; }

        [DataMember]
        public long StartTime { get; set; }

        [DataMember]
        public long EndTime { get; set; }

        [DataMember]
        public long UxDuration { get; set; }

        [DataMember]
        public List<DevicePlatform> Platforms { get; set; }

        [DataMember]
        public Job Job { get; set; }

        [DataMember]
        public List<TestExecutionStep> Steps { get; set; }

        [DataMember]
        public List<string> Tags { get; set; }

        [DataMember]
        public Project Project { get; set; }

        [JsonIgnore]
        public TestExecutionStatus Status { get; set; }
    }
}