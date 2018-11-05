using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Sveit.Services.Requests
{
    public class RequestService : IRequestService
    {
        private static HttpClient _instance;

        private static HttpClient HttpClient => _instance ?? (_instance = new HttpClient());

        public async Task<TResult> GetAsync<TResult>(string uri, string token = "")
        {
            SetupHttpClient(token);

            HttpResponseMessage response = await HttpClient.GetAsync(uri).ConfigureAwait(false);
            await HandleResponse(response);

            string content = await response.Content.ReadAsStringAsync();
            return await Task.Run(() => JsonConvert.DeserializeObject<TResult>(content)).ConfigureAwait(false);
        }

        public async Task<TResult> PostAsync<TRequest, TResult>(string uri, TRequest data, string token = "")
        {
            SetupHttpClient(token);
            string serialized = await Task.Run(() => JsonConvert.SerializeObject(data)).ConfigureAwait(false);

            HttpResponseMessage response =
                await HttpClient.PostAsync(uri, new StringContent(serialized, Encoding.UTF8, "application/json"))
                .ConfigureAwait(false);
            await HandleResponse(response);

            string content = await response.Content.ReadAsStringAsync();
            return await Task.Run(() => JsonConvert.DeserializeObject<TResult>(content)).ConfigureAwait(false);
        }

        public async Task<TResult> PutAsync<TRequest, TResult>(string uri, TRequest data, string token)
        {
            SetupHttpClient(token);
            string serialized = await Task.Run(() => JsonConvert.SerializeObject(data)).ConfigureAwait(false);

            HttpResponseMessage response =
                await HttpClient.PutAsync(uri, new StringContent(serialized, Encoding.UTF8, "application/json"))
                .ConfigureAwait(false);
            await HandleResponse(response);

            string content = await response.Content.ReadAsStringAsync();
            return await Task.Run(() => JsonConvert.DeserializeObject<TResult>(content));
        }

        public async Task<bool> DeleteAsync(string uri, string token)
        {
            SetupHttpClient(token);

            HttpResponseMessage response = await HttpClient.DeleteAsync(uri).ConfigureAwait(false);
            await HandleResponse(response);

            return Convert.ToBoolean(await response.Content.ReadAsStringAsync());
        }

        private async Task HandleResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.Forbidden || response.StatusCode == HttpStatusCode.Unauthorized)
                    throw new Exception(content);

                throw new HttpRequestException(content);
            }
        }

        private void SetupHttpClient(string token = "")
        {
            HttpClient.DefaultRequestHeaders.Clear();
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (!string.IsNullOrWhiteSpace(token))
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<string> PostRawAsync(string uri, string data)
        {
            HttpClient.DefaultRequestHeaders.Clear();
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));

            HttpResponseMessage response =
                await HttpClient.PostAsync(uri, new StringContent(data, Encoding.UTF8, "text/plain"))
                .ConfigureAwait(false);
            await HandleResponse(response);

            return await response.Content.ReadAsStringAsync();
        }
    }
}
