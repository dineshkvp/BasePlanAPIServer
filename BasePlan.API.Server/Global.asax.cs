using Microsoft.AspNet.SignalR;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace BasePlan.API.Server
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            /*
            GlobalConfiguration.Configuration
               .MessageHandlers.Add(new BasicAuthMessageHandler()
               {
                   PrincipalProvider = new DummyPrincipalProvider()
               });
             * */
            var config = new HubConfiguration
            {
                EnableCrossDomain = true
            };

            // Register the default hubs route: ~/signalr
            RouteTable.Routes.MapHubs(config);

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
        }

        // claims transformation
        protected void Application_PostAuthenticateRequest()
        {
            if (ClaimsPrincipal.Current.Identity.IsAuthenticated)
            {
                //var principal = new ClaimsTransformer().Authenticate(string.Empty, ClaimsPrincipal.Current);

                //HttpContext.Current.User = principal;
                //Thread.CurrentPrincipal = principal;
            }
        }
    }
}