using Newtonsoft.Json;
using Polly;
using RevenueCompanion.Presentation.Services.Interface;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace RevenueCompanion.Presentation.Services
{
    public class HttpClientService : IHttpClientService
    {

        public async Task<T> GetAsync<T>(string uri, string authToken, string param)

        {
            try
            {
                HttpClient httpClient = CreateHttpClient(authToken, param);
                string jsonResult = string.Empty;

                var responseMessage = await httpClient.GetAsync(uri);

                if (responseMessage.IsSuccessStatusCode)
                {
                    jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var json = JsonConvert.DeserializeObject<T>(jsonResult);
                    return json;
                }

                else if (responseMessage.StatusCode == HttpStatusCode.Forbidden ||
                 responseMessage.StatusCode == HttpStatusCode.Unauthorized ||
                 responseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var json = JsonConvert.DeserializeObject<T>(jsonResult);
                    return json;

                }
                else
                {
                    return default(T);
                }


            }
            catch (Exception e)
            {
                Debug.WriteLine($"{ e.GetType().Name + " : " + e.Message}");
                return default(T);
            }
        }


        public async Task<T> PostAsync<T>(string uri, T data, string authToken = "")
        {
            try
            {
                HttpClient httpClient = CreateHttpClient(authToken);

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string jsonResult = string.Empty;

                var responseMessage = await Policy
                    .Handle<WebException>(ex =>
                    {
                        Debug.WriteLine($"{ex.GetType().Name + " : " + ex.Message}");
                        return true;
                    })
                    .WaitAndRetryAsync
                    (
                        5,
                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    )
                    .ExecuteAsync(async () => await httpClient.PostAsync(uri, content));

                if (responseMessage.IsSuccessStatusCode)
                {
                    jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var json = JsonConvert.DeserializeObject<T>(jsonResult);
                    return json;
                }

                if (responseMessage.StatusCode == HttpStatusCode.Forbidden ||
                    responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return default(T);
                }

                return default(T);

            }
            catch (Exception e)
            {
                Debug.WriteLine($"{ e.GetType().Name + " : " + e.Message}");
                return default(T);
            }
        }

        public async Task<TR> PostAsync<T, TR>(string uri, T data, string authToken = "")
        {
            try
            {


                HttpClient httpClient = CreateHttpClient(authToken);

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string jsonResult = string.Empty;

                var responseMessage = await Policy
                    .Handle<WebException>(ex =>
                    {
                        Debug.WriteLine($"{ex.GetType().Name + " : " + ex.Message}");
                        return true;
                    })
                    .WaitAndRetryAsync
                    (
                        5,
                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    )
                    .ExecuteAsync(async () => await httpClient.PostAsync(uri, content));
                jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var json = JsonConvert.DeserializeObject<TR>(jsonResult);
                    return json;
                }

                if (responseMessage.StatusCode == HttpStatusCode.Forbidden ||
                    responseMessage.StatusCode == HttpStatusCode.BadRequest ||
                    responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    var json = JsonConvert.DeserializeObject<TR>(jsonResult);
                    return json;
                }

                var jsonResp = JsonConvert.DeserializeObject<TR>(jsonResult);
                return jsonResp;

            }
            catch (Exception e)
            {
                Debug.WriteLine($"{ e.GetType().Name + " : " + e.Message}");
                return default;
            }
        }

        public async Task<T> PutAsync<T>(string uri, T data, string authToken = "")
        {
            try
            {
                HttpClient httpClient = CreateHttpClient(authToken);

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string jsonResult = string.Empty;

                var responseMessage = await Policy
                    .Handle<WebException>(ex =>
                    {
                        Debug.WriteLine($"{ex.GetType().Name + " : " + ex.Message}");
                        return true;
                    })
                    .WaitAndRetryAsync
                    (
                        5,
                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    )
                    .ExecuteAsync(async () => await httpClient.PutAsync(uri, content));

                if (responseMessage.IsSuccessStatusCode)
                {
                    jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var json = JsonConvert.DeserializeObject<T>(jsonResult);
                    return json;
                }

                if (responseMessage.StatusCode == HttpStatusCode.Forbidden ||
                    responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return default(T);
                }

                return default(T);

            }
            catch (Exception e)
            {
                Debug.WriteLine($"{ e.GetType().Name + " : " + e.Message}");
                return default(T);
            }
        }
        public async Task<TR> PutAsync<T, TR>(string uri, T data, string authToken = "")
        {
            try
            {
                HttpClient httpClient = CreateHttpClient(authToken);

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string jsonResult = string.Empty;

                var responseMessage = await Policy
                    .Handle<WebException>(ex =>
                    {
                        Debug.WriteLine($"{ex.GetType().Name + " : " + ex.Message}");
                        return true;
                    })
                    .WaitAndRetryAsync
                    (
                        5,
                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    )
                    .ExecuteAsync(async () => await httpClient.PutAsync(uri, content));
                jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var json = JsonConvert.DeserializeObject<TR>(jsonResult);
                    return json;
                }
                if (responseMessage.StatusCode == HttpStatusCode.Forbidden ||
                                   responseMessage.StatusCode == HttpStatusCode.BadRequest ||
                                   responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    var json = JsonConvert.DeserializeObject<TR>(jsonResult);
                    return json;
                }

                var jsonResp = JsonConvert.DeserializeObject<TR>(jsonResult);
                return jsonResp;

            }
            catch (Exception e)
            {
                Debug.WriteLine($"{ e.GetType().Name + " : " + e.Message}");
                return default;
            }
        }

        public async Task DeleteAsync(string uri, string authToken = "")
        {
            HttpClient httpClient = CreateHttpClient(authToken);
            await httpClient.DeleteAsync(uri);
        }

        private HttpClient CreateHttpClient(string authToken = "", string param = "")
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            var httpClient = new HttpClient(clientHandler);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (!string.IsNullOrEmpty(authToken))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
            }
            if (!string.IsNullOrEmpty(param))
            {
                httpClient.DefaultRequestHeaders.Add("browserUrl", param);
            }

            return httpClient;
        }


    }
}
