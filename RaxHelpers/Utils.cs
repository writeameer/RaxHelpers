using System;
using System.IO;
using System.Net;
using System.Xml;

namespace WriteAmeer.RaxHelpers
{
    public class Utils
    {
        public static XmlDocument GetResponseContentXml(HttpWebResponse response)
        {
            var httpContent = new StreamReader(response.GetResponseStream()).ReadToEnd();
            if (!String.IsNullOrEmpty(httpContent))
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(httpContent);
                return xmlDoc;
            }

            return null;
        }
    }
}