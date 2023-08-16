using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Nop.Web.Framework.Mvc.Routing;
using Nop.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finitx.Plugin.Misc.LogEventsDataToServer.Infrastructure
{
    public class RouteProvider : BaseRouteProvider, IRouteProvider
    {
        public int Priority => 5;

        public void RegisterRoutes(IEndpointRouteBuilder endpointRouteBuilder)
        {
            var lang = GetLanguageRoutePattern();

            endpointRouteBuilder.MapControllerRoute(name: "finitxCustomer",pattern: "Customer/{action=Index}/{id?}",           
            new { controller = "FinitxCustomer" });

            //change route of  register Customer
            endpointRouteBuilder.MapControllerRoute(name: "registerCustomer",pattern: "register/{id?}",           
            new { controller = "FinitxCustomer",action= "register" });            
          
            //change route of password recovery confirmation
            endpointRouteBuilder.MapControllerRoute(name: "PasswordRecoveryConfirm",
                pattern: $"{lang}/passwordrecovery/confirm",
                new { controller = "FinitxCustomer", action = "PasswordRecoveryConfirm" });
            //change route of Account Activation
            endpointRouteBuilder.MapControllerRoute(name: "AccountActivation",
               pattern: $"{lang}/customer/activation",
               defaults: new { controller = "FinitxCustomer", action = "AccountActivation" });

        }
    }
}
