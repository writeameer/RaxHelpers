using System;
using System.Linq;
using NUnit.Framework;
using WriteAmeer.RaxHelpers;

namespace RaxHelpers.Test
{
    // These aren't "real" test but currently just samples of how to use the RaxHelper assembly

    [TestFixture]
    class CloudServerTests
    {
        // Get Username and API Key info from  Environment Variables.
        // This is to ensure I don't check in my credentials by mistake :) !
        readonly string _userName = Environment.GetEnvironmentVariable("RackUser", EnvironmentVariableTarget.Machine);
        private readonly string _apiKey = Environment.GetEnvironmentVariable("RackApiKey", EnvironmentVariableTarget.Machine);

        private readonly CloudServerClient _csClient;


        public CloudServerTests()
        {
            // Authenticate with Rackspace
            var authResponse = new AuthClient(userName: _userName, apiKey: _apiKey, accountRegion: "us").Authenticate();

            // Extract Tokens from response
            var accountId = authResponse.Headers["X-Server-Management-Url"].Split('/').Last();
            var authToken = authResponse.Headers["X-Auth-Token"];
            var serverManagementUrl = authResponse.Headers["X-Server-Management-Url"];

            // Create Cloud Server Client
            _csClient = new CloudServerClient(serverManagementUrl: serverManagementUrl, authToken: authToken);

        }

        [Test]
        public void ListServers()
        {
            var response = _csClient.Do("GET", "/servers");
            Console.WriteLine(Utils.GetResponseContentXml(response).OuterXml);
        }
    }
}
