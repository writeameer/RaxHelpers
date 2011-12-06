using System;
using System.Net;

namespace WriteAmeer.RaxHelpers
{
    public class AuthClient
    {
        private readonly string _userName;
        private readonly string _apiKey;
        private readonly string _authUrl = "https://auth.api.rackspacecloud.com/v1.0";


        public AuthClient(string userName, string apiKey, string accountRegion)
        {
            // validate account region
            if (accountRegion != "us" && accountRegion != "uk") throw new Exception("Propety value must be 'us' or 'uk'");
   
            _userName = userName;
            _apiKey = apiKey;

            // Set authentication URL based on region
            if (accountRegion.Equals("uk")) _authUrl = "https://lon.auth.api.rackspacecloud.com/v1.0";

        }

        public WebResponse Authenticate()
        {
            // Make an authentication request
            var apiRequest = WebRequest.Create(_authUrl);
            apiRequest.Headers.Add("X-Auth-User", _userName);
            apiRequest.Headers.Add("X-Auth-Key", _apiKey);

            // Return Auth Reponse
            var response = apiRequest.GetResponse();
            if (response == null) throw new WebException("Rackspace Authentication response was null!");
            return response;
        }
    }
}