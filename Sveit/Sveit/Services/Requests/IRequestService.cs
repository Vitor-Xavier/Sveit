using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sveit.Services.Requests
{
    public interface IRequestService
    {
        Task<TResult> GetAsync<TResult>(string uri, string token = "");

        Task<TResult> PostAsync<TRequest, TResult>(string uri, TRequest data, string token = "");

        Task<TResult> PutAsync<TRequest, TResult>(string uri, TRequest data, string token);

        Task<bool> DeleteAsync(string uri, string token);
    }
}
