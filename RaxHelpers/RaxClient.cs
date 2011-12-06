using System.Net;
using System.Text;

namespace WriteAmeer.RaxHelpers
{
    public class RaxClient
    {
        public string AuthToken { get; set; }
        public string AccountId { get; set; }


        public virtual HttpWebResponse Do (string method, string url)
        {
            var apiRequest = CreateWebRequest(url, AuthToken);
            apiRequest.Method = method;
            return (HttpWebResponse)GetWebResponse(apiRequest);
        }

        public virtual HttpWebResponse Post(string url, string postData)
        {
            var apiRequest = CreateWebRequest(url, AuthToken);
            apiRequest.Method = "POST";

            // Add POST data
            var byteArray = Encoding.UTF8.GetBytes(postData);
            apiRequest.ContentLength = byteArray.Length;
            var dataStream = apiRequest.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            return (HttpWebResponse) GetWebResponse(apiRequest);
        }

        public virtual HttpWebResponse Put(string url, string postData)
        {
            var apiRequest = CreateWebRequest(url, AuthToken);
            apiRequest.Method = "Put";

            // Add POST data
            var byteArray = Encoding.UTF8.GetBytes(postData);
            apiRequest.ContentLength = byteArray.Length;
            var dataStream = apiRequest.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            return (HttpWebResponse)GetWebResponse(apiRequest);
        }

        public HttpWebRequest CreateWebRequest(string uri, string authtoken)
        {
            var apiRequest = (HttpWebRequest)WebRequest.Create(uri);
            apiRequest.Accept = "application/xml";
            apiRequest.ContentType = "application/xml";
            apiRequest.Headers.Add("X-Auth-Token", authtoken);

            return apiRequest;
        }

        public static WebResponse GetWebResponse(HttpWebRequest webRequest)
        {
            // Make Request 
            try
            {
                return webRequest.GetResponse();
            }
            catch (WebException webException)
            {
                return webException.Response;
            }
        }
    }
}