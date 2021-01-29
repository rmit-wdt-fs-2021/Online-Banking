using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace AdminApp
{
    /// <summary>
    /// Code referenced from Matthew Bolger's Tut/Lab 09
    /// </summary>
    public class Helper
    {
        private const string ApiBaseUri = "http://localhost:5000";

        public static HttpClient InitializeClient()
        {
            var client = new HttpClient { BaseAddress = new Uri(ApiBaseUri) };
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }
    }
}
