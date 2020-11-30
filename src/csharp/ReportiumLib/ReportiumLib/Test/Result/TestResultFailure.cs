﻿﻿using System;
using System.Runtime.Serialization;

using Reportium.Model.Util;

namespace Reportium.Test.Result
{
    /// <summary>
    /// Test result failure
    /// </summary>
    [DataContract]
    internal class TestResultFailure : ITestResult
    {
        // constants
		public static readonly int MessageMaxLength = 4096;
		public static readonly string TrimmedTextSuffix = "...";

        public TestResultFailure(string reason, Exception ex, string failureReasonName)
        {
            this.FailureReasonName = failureReasonName;

            if (ex != null)
            {
                Message = reason + ". Stack Trace:" + ex.StackTrace;
            }
            else
            {
                Message = reason;
            }

            if (Message != null && Message.Length > MessageMaxLength)
			{
                Message = Message.Substring(0, MessageMaxLength - TrimmedTextSuffix.Length) + TrimmedTextSuffix;
			}
        }

        [DataMember]
        public string Status { get; } = "FAILED";

        public string Message { get; }

        public string FailureReasonName { get; }

        public bool IsSuccessful { get; }

        public override string ToString()
        {
            return new ToStringBuilder<TestResultFailure>(this)
                .Append(x => x.Message)
                .Append(x => x.Status)
                .Append(x => x.FailureReasonName)
                .ToString();
        }
    }
}