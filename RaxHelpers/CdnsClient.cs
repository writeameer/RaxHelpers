using System;
using System.Net;

namespace WriteAmeer.RaxHelpers
{
    public class CdnsClient : RaxClient
    {
        private string _region;
        public string Region
        {
            get { return _region; }
            set {
                if (value != "us" && value != "uk") throw new Exception("Property value must be 'us' or 'uk'");
                _region = value;
            }
        }

        public string ServiceEndpoint
        {
            get { return Region.Equals("us")    
                             ? "https://dns.api.rackspacecloud.com/v1.0/" + AccountId + "/" 
                             : "https://lon.dns.api.rackspacecloud.com/v1.0/" + AccountId + "/"; 
            }
        }

        public override HttpWebResponse Do (string method, string url) { return base.Do(method, ServiceEndpoint + url); }
        public override HttpWebResponse Post(string url, string postData) { return base.Post(ServiceEndpoint + url, postData); }
        public override HttpWebResponse Put(string url, string postData) { return base.Put(ServiceEndpoint + url, postData); }
    }
}