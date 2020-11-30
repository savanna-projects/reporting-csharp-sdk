using System;
using System.Runtime.Serialization;

namespace Reportium.Exceptions
{
    /// <summary>
    /// Reporting Exception
    /// </summary>
    /// <remarks>Special reporting exception</remarks>
    [Serializable]
    public class ReportiumException : Exception
    {
        public ReportiumException()
        { }

        public ReportiumException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }

        public ReportiumException(string message)
            : base(message)
        { }

        public ReportiumException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
