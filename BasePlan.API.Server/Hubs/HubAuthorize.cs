using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;



namespace BasePlan.API.Server.Hubs
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class HubAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool UserAuthorized(System.Security.Principal.IPrincipal user)
        {
            
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            var principal = (ClaimsPrincipal)user;

            if (principal != null)
            {
                Claim authenticated = principal.FindFirst(ClaimTypes.Authentication);
                if (authenticated != null)
                {
                    return authenticated.Value == "true" ? true : false;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}