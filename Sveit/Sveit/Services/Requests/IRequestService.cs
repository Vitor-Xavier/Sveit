using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Sveit.Services.Requests
{
    public interface IRequestService
    {
        Task<TResult> GetAsync<TResult>(string uri, string token = "");

        Task<TResult> PostAsync<TRequest, TResult>(string uri, TRequest data, string token = "");

        Task<string> PostRawAsync(string uri, string data);

        Task<string> PostMimeAsync(string uri, ByteArrayContent[] byteArrays, string token = "");

        Task<TResult> PutAsync<TRequest, TResult>(string uri, TRequest data, string token);

        Task<bool> DeleteAsync(string uri, string token);
    }
}
