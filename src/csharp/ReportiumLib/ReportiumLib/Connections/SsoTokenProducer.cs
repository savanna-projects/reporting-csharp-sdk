using Newtonsoft.Json;

using Reportium.Client;

using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Reportium.Connections
{
    /// <summary>
    /// Creator of single sign on token
    /// </summary>
    internal class SsoTokenProducer
    {
        // members
        private readonly Uri apiKeyServer;
        private readonly string apiKey;
        private readonly string tenantId;
        private readonly static HttpClient httpClient = new HttpClient();

        /// <summary>
        /// Creates SsoTokenProducer object
        /// </summary>
        /// <param name="apiKeyServer">URI of the api key server</param>
        /// <param name="apiKey">Used to create a new SSO token</param>
        /// <param name="tenantId">Tenant id which identifies where tests are reported to</param>
        public SsoTokenProducer(Uri apiKeyServer, string apiKey, string tenantId)
        {
            this.apiKeyServer = apiKeyServer;
            this.apiKey = apiKey;
            this.tenantId = tenantId;
        }

        /// <summary>
        /// Produce a new token using the ApikeyServer
        /// </summary>
        /// <returns>String token</returns>
        public string GetToken()
        {
            // setup
            var resolvedUri = new Uri(apiKeyServer, string.Format(Constants.Url.Path, tenantId));
            var formParams = new Dictionary<string, string>
            {
                ["grant_type"] = "refresh_token",
                ["client_id"] = "reportium",
                ["refresh_token"] = apiKey,
            };
            var form = new FormUrlEncodedContent(formParams); //by default supporting UTF-8

            // post
            var response = httpClient.PostAsync(resolvedUri, form)
                .GetAwaiter()
                .GetResult()
                .Content
                .ReadAsStringAsync()
                .GetAwaiter()
                .GetResult();

            // deserializeObject
            var token = JsonConvert.DeserializeObject<IDictionary<string, object>>(response);

            // get
            return $"{token["access_token"]}";
        }
    }
}