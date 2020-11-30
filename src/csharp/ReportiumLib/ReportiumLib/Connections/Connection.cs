namespace Reportium.Connections
{
    /// <summary>
    /// Logical connection object
    /// </summary>
    public class Connection
    {
        /// <summary>
        /// Create a new connection to Reportium back-end
        /// </summary>
        /// <param name="apiKey"> apiKey to use for creating new SSO tokens </param>
        /// <param name="tenant"> Tenant which identifies where tests are reported to </param>
        public Connection(string apiKey, string tenant)
        {
            ApiKey = apiKey;
            Tenant = tenant;
        }

        // TODO: add documentation
        public string ApiKey { get; }

        // TODO: add documentation
        public string Tenant { get; }
    }
}
