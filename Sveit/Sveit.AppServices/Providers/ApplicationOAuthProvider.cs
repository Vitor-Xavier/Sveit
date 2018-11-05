using Microsoft.Owin.Security.OAuth;
using Sveit.API.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Sveit.AppServices.Providers
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly DatabaseContext _context;

        /// <summary>
        /// Inicializa conexao de dados;
        /// </summary>
        public ApplicationOAuthProvider()
        {
            _context = new DatabaseContext();
        }

        /// <summary>
        /// Valida autenticação do cliente.
        /// </summary>
        /// <param name="c">Contexto da autenticação.</param>
        /// <returns></returns>
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext c)
        {
            c.Validated();

            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Da permissões ao dono da credencial caso sua autenticação seja aprovada.
        /// </summary>
        /// <param name="c">Credenciais da requisição.</param>
        /// <returns></returns>
        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext c)
        {

            if (ValidateUser(c.UserName, c.Password))
            {
                Claim claim1 = new Claim(ClaimTypes.Name, c.UserName);
                Claim[] claims = new Claim[] { claim1 };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(
                    claims, OAuthDefaults.AuthenticationType);
                c.Validated(claimsIdentity);
            }

            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Verifica as credenciais informadas.
        /// </summary>
        /// <param name="user">Nome de usuário do solicitante.</param>
        /// <param name="pass">Senha do usuário do solicitante.</param>
        /// <returns>Validade das credenciais</returns>
        public bool ValidateUser(string user, string pass)
        {
            var player = (from p in _context.Players
                          where p.Email == user &&
                          p.Password == pass
                          select p).FirstOrDefault();
            if (player == null)
                return false;
            else
                return true;
        }
    }
}