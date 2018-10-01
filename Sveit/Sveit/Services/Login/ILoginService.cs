using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sveit.Services.Login
{
    public interface ILoginService
    {
        Task<Models.Player> LogIn(string email, string password);

        Task<bool> LogOut();
    }
}
