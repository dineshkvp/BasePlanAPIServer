using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BasePlan.API.Server.Hubs
{
    [HubName("BasePlanHub")]
     
    public class MessagingHub: Hub
    {
        private readonly static Dictionary<string, string> _connections = new Dictionary<string, string>();

        public void mapConnection(string userName)
        {
            // Clients.All.addMessage(userName, userName + "  Connection Id is: " + Context.ConnectionId);
            _connections.Add(userName, Context.ConnectionId);
        }
        //[HubAuthorize]
        public void Send(string userName, string who, string message)
        {
            if (string.IsNullOrEmpty(who))
            {
                Clients.All.addMessage(userName, message);
            }
            else
            {
                var targetConnectionIds = _connections.Where(p => p.Key == who).Select(k => k.Value);
                foreach (var connectionId in targetConnectionIds)
                {
                    Clients.Client(connectionId).addMessage(userName, message);
                }
            }
        }
        
        public override Task OnDisconnected()
        {
            if (_connections != null && _connections.Count > 0)
            {
                var user = _connections.Where(p => p.Value == Context.ConnectionId).Select(k => k.Key).FirstOrDefault();
                if (user != null)
                {
                    _connections.Remove(user);
                }
            }
                return base.OnDisconnected();            
        }

        /*
        public override Task OnConnected()
        {
            string name = Context.User.Identity.Name;

            _connections.Add(name, Context.ConnectionId);

            return base.OnConnected();
        }

        public override Task OnDisconnected()
        {
            string name = Context.User.Identity.Name;

            _connections.Remove(name, Context.ConnectionId);

            return base.OnDisconnected();
        }

        public override Task OnReconnected()
        {
            string name = Context.User.Identity.Name;

            if (!_connections.GetConnections(name).Contains(Context.ConnectionId))
            {
                _connections.Add(name, Context.ConnectionId);
            }

            return base.OnConnected();
        }
         * */
    }
}