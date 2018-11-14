using Sveit.AppServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace Sveit.API
{
    /// <summary>
    /// Inicia a API.
    /// </summary>
    public class WebApiApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Permite configurações ao iniciar a API.
        /// </summary>
        protected void Application_Start()
        {
            var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            json.SerializerSettings.ContractResolver = new ShouldSerializeContractResolver();

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
