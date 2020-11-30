
using System.Runtime.Serialization;

namespace Reportium.model
{
    /// <summary>
    /// Browser info class
    /// </summary>
    /// <remarks>Used in the execution context without using MCM</remarks>
    [DataContract]
    public class BrowserInfo
    {
        // TODO: add documentation
        public BrowserInfo(BrowserType browserType, string browserVersion)
        {
            BrowserType = browserType.ToString();
            BrowserVersion = browserVersion;
        }

        /// <summary>
        /// Gets or sets the browser type assigned to the browser info
        /// </summary>
        [DataMember]
        public string BrowserType { get; set; }

        /// <summary>
        /// Gets or sets the browser version assigned to the browser info
        /// </summary>
        [DataMember]
        public string BrowserVersion { get; set; }
    }
}
