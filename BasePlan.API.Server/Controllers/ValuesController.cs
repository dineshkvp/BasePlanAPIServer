using BasePlan.API.Server.Hubs;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace BasePlan.API.Server.Controllers
{
    public class ValuesController : ApiController
    {
        //// GET api/values
        //[System.Web.Http.Authorize]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        //// GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [System.Web.Http.Authorize]
        public void Post([FromBody]string value)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<MessagingHub>();
            var userName = HttpContext.Current.User.Identity.Name;
            
            context.Clients.All.addMessage(userName, value);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}