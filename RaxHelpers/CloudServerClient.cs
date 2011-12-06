using System.Net;

namespace WriteAmeer.RaxHelpers
{
    public class CloudServerClient : RaxClient
    {
        public CloudServerClient(string serverManagementUrl, string authToken)
        {
            ServerManagementUrl = serverManagementUrl;
            AuthToken = authToken;
        }
        public string ServerManagementUrl { get; set; }

        public override HttpWebResponse Do(string method, string url) { return base.Do(method, ServerManagementUrl + url); }
        public override HttpWebResponse Post(string uri, string postData) { return base.Post(ServerManagementUrl + uri, postData); }
        public override HttpWebResponse Put(string url, string postData) { return base.Put(ServerManagementUrl + url, postData); }
    }
}
