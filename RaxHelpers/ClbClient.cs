using System;
using System.Collections.Generic;
using System.Net;

namespace WriteAmeer.RaxHelpers
{
    public class ClbClient : RaxClient
    {
        readonly List<string> _validRegions = new List<string> {"dfw","lon","ord" } ;
        public string ClbName { get; set; }
        public string ServiceEndpoint { get; set; }


        public ClbClient(string clbRegion, string accountId, string authToken)
        {
            // Validate Clb Region
            if (!_validRegions.Contains(clbRegion))   throw new Exception("Property value must be " + _validRegions);
 
            AuthToken = authToken;
            AccountId = accountId;
            
            // Set Service Endpoint based on CLB Region
            ServiceEndpoint = "https://" + clbRegion + ".loadbalancers.api.rackspacecloud.com/v1.0/" + AccountId + "/";
        }

        public override HttpWebResponse Do(string method, string url) { return base.Do(method, ServiceEndpoint + url ); }
        public override HttpWebResponse Post(string uri,string postData) { return base.Post(ServiceEndpoint + uri,postData);}
        public override HttpWebResponse Put(string url, string postData) { return base.Put(ServiceEndpoint + url, postData); }
    }
}