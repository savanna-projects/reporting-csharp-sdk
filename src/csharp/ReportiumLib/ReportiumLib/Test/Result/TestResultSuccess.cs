using Reportium.Model.Util;

using System.Runtime.Serialization;

namespace Reportium.Test.Result
{
    [DataContract]
    internal class TestResultSuccess : ITestResult
    {
        [DataMember]
        public string Status { get; } = "PASSED";

        public bool IsSuccessful { get; }

        public string Message { get; }

        public string FailureReasonName { get; } = string.Empty;

        public override string ToString()
        {
            return new ToStringBuilder<TestResultSuccess>(this)
                .Append(x => x.Status)
                .ToString();
        }
    }
}