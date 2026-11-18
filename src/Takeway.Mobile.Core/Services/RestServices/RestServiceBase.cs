using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Takeway.Mobile.Core.Services.RestServices
{
    public class RestServiceBase
    {
      
        protected HttpResponseMessage GetService(string url, string token)
        {
            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            request.Headers.Add("IndaSoftAuthToken", token);

            var response = httpClient.SendAsync(request);
            return response.Result;
        }

        protected HttpResponseMessage PostService(string url, string token, HttpContent content)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var request = new HttpRequestMessage(HttpMethod.Post, url);

            request.Headers.Add("IndaSoftAuthToken", token);

            request.Content = content;

            var response = httpClient.SendAsync(request);
            return response.Result;
        }

        protected HttpResponseMessage PutService(string url, string token, HttpContent content)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var request = new HttpRequestMessage(HttpMethod.Put, url);

            request.Headers.Add("IndaSoftAuthToken", token);

            request.Content = content;

            var response = httpClient.SendAsync(request);
            return response.Result;
        }

        protected HttpResponseMessage DeleteService(string url, string token)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var request = new HttpRequestMessage(HttpMethod.Delete, url);

            request.Headers.Add("IndaSoftAuthToken", token);

            var response = httpClient.SendAsync(request);
            return response.Result;
        }


        public void Execute(Action action)
        {
            try
            {
               action();
            }
            catch (Exception ex)
            {
               
            }
        }

        public TResponse Execute<TResponse>(Func<TResponse> action)
        {
            try
            {
                return action();
            }
            catch (Exception ex)
            {
                return default(TResponse);
            }
        }



    }
}