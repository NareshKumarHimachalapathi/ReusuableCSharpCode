using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Utilities.Service
{
    public static class ServiceHelper
    {
        public static async Task<string> CallPostServiceWithRetry(string jsonData, string subscriptionKey, string uri, int retries, int timeToSleep)
        {
            string jsonResponse = string.Empty;
            while (true)
            {
                try
                {
                    var client = new HttpClient();
                    // Request headers
                    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
                    HttpResponseMessage response;
                    // Request body
                    byte[] byteData = Encoding.UTF8.GetBytes(jsonData);

                    using (var content = new ByteArrayContent(byteData))
                    {
                        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                        response = await client.PostAsync(uri, content);
                        if (response.IsSuccessStatusCode)
                        {
                            jsonResponse = await response.Content.ReadAsStringAsync();
                        }
                    }
                    break;
                }
                catch (Exception ex)
                {
                    if (--retries == 0)
                    {
                        throw ex;
                    }
                    else
                    {
                        Thread.Sleep(timeToSleep);
                    };
                }

            }
            return jsonResponse;
        }

        public static async Task<string> CallPutServiceWithRetry(string jsonData, string subscriptionKey, string uri, int retries, int timeToSleep)
        {
            string jsonResponse = string.Empty;
            while (true)
            {
                try
                {
                    var client = new HttpClient();
                    // Request headers
                    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
                    HttpResponseMessage response;
                    // Request body
                    byte[] byteData = Encoding.UTF8.GetBytes(jsonData);

                    using (var content = new ByteArrayContent(byteData))
                    {
                        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                        response = await client.PutAsync(uri, content);
                        jsonResponse = await response.Content.ReadAsStringAsync();
                    }
                    break;
                }
                catch (Exception ex)
                {
                    if (--retries == 0)
                    {
                        throw ex;
                    }
                    else
                    {
                        Thread.Sleep(timeToSleep);
                    };
                }

            }
            return jsonResponse;
        }

        public static async Task<string> CallGetServiceWithRetry(string uri, string subscriptionKey,int retries, int timeToSleep)
        {
            string jsonResponse = string.Empty;

            while (true)
            {
                try
                {
                    var client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
                    var response = await client.GetAsync(uri);
                    jsonResponse = await response.Content.ReadAsStringAsync();
                    break;
                }
                catch (Exception ex)
                {
                    if (--retries == 0)
                    {
                        throw ex;
                    }
                    else
                    {
                        Thread.Sleep(timeToSleep);
                    };
                }
            }

            return jsonResponse;
        }


        public static async Task<string> CallDeletServiceWithRetry(string uri, string subscriptionKey, int retries, int timeToSleep)
        {
            string jsonResponse = string.Empty;

            while (true)
            {
                try
                {
                    var client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
                    var response = await client.DeleteAsync(uri);
                    jsonResponse = await response.Content.ReadAsStringAsync();
                    break;
                }
                catch (Exception ex)
                {
                    if (--retries == 0)
                    {
                        throw ex;
                    }
                    else
                    {
                        Thread.Sleep(timeToSleep);
                    };
                }
            }

            return jsonResponse;
        }
    }
}
