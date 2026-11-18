using System;
using System.Net;

namespace Takeaway.Admin.Wpf.Utility
{
    public class ConnectionHelper
    {
        public static bool CheckConnection(String url)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = 15*000;
                request.Credentials = CredentialCache.DefaultNetworkCredentials;
                var response = (HttpWebResponse)request.GetResponse();

                return response.StatusCode == HttpStatusCode.OK;
            }
            catch
            {
                return false;
            }
        } 
    }
}