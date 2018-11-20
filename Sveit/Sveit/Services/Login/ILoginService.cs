using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sveit.Services.Login
{
    public interface ILoginService
    {
        Task<Models.Player> CheckLogIn();

        Task<Models.Player> LogIn(string username, string password);

        Task<string> GetOAuthToken();

        bool LogOut();
    }
}
