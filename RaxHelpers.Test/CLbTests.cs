using System;
using System.Linq;
using NUnit.Framework;
using WriteAmeer.RaxHelpers;

namespace RaxHelpers.Test
{
    // These aren't "real" test but currently just samples of how to use the RaxHelper assembly

    [TestFixture]
    public class ClbTests
    {
        // Get Username and API Key info from  Environment Variables.
        // This is to ensure I don't check in my credentials by mistake :) !
        readonly string _userName = Environment.GetEnvironmentVariable("RackUser", EnvironmentVariableTarget.Machine);
        private readonly string _apiKey = Environment.GetEnvironmentVariable("RackApiKey", EnvironmentVariableTarget.Machine);

       
        // Cloud Load Balancer Client used accross tests
        private readonly ClbClient _clbClient;

        public ClbTests()
        {
            // Authenticate with Rackspace
            var authResponse =  new AuthClient(userName: _userName, apiKey: _apiKey, accountRegion: "us").Authenticate();

            // Extract Tokens from response
            var accountId = authResponse.Headers["X-Server-Management-Url"].Split('/').Last();
            var authToken = authResponse.Headers["X-Auth-Token"]; 

            // Create Cloud Load Balancer Client
            _clbClient = new ClbClient (
                clbRegion:"ord1",
                accountId: accountId,
                authToken: authToken
            );
        }


        [Test]
        public void ListLoadBalancers()
        {
            var response = _clbClient.Do("get", "loadbalancers");
            Console.WriteLine(Utils.GetResponseContentXml(response).OuterXml);
        }
        [Test]
        public void AddNodeToLoadBalancer()
        {
            const string addNodeXml = @"
                <nodes xmlns=""http://docs.openstack.org/loadbalancers/api/v1.0"">
                    <node address=""1.1.1.1"" port=""80"" condition=""ENABLED"" />
                </nodes>";

            var response = _clbClient.Post("loadbalancers/14048/nodes", addNodeXml);
            Console.WriteLine(Utils.GetResponseContentXml(response).OuterXml);
        }

        [Test]
        public void RemoveNodeFromLoadBalancer()
        {
            var response = _clbClient.Do("DELETE", "loadbalancers/14048/nodes/93387");
            Console.WriteLine(Utils.GetResponseContentXml(response).OuterXml);

        }

        [Test]
        public void ListNodesInLoadBalancer()
        {
            var response = _clbClient.Do("Get","loadbalancers/14048/nodes");
            Console.WriteLine(Utils.GetResponseContentXml(response).OuterXml);
        }

        [Test]
        public void CDnsTests()
        {
            /*
            var cdnsClient = new CdnsClient
                                 {
                                     AccountId = AccountId,
                                     AuthToken = AuthToken,
                                     Region = "ord"

                                 };


            var response = cdnsClient.Do("GET", "domains/2866163/records");
            Console.WriteLine(Utils.GetResponseContentXml(response).OuterXml);
             * */

        }
    }
}
