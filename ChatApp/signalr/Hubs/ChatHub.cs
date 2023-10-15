using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ChatApp.signalr.Hubs
{
    public class ChatHub : Hub
    {
        // Keeps a track of active client connections by storing connection ids
        // Can be used to store names of active users by using
        // Context.User.Identity.Name provided authentication is used
        // Current example uses no authentication
        public static HashSet<string> CurrentConnections = new HashSet<string>();

        // Used to store the count of active users
        private static int _userCount = 0;
        public override Task OnConnected()
        {
            _userCount++;
            Clients.All.online(_userCount);
            //CurrentConnections.Add(Context.ConnectionId);
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            _userCount--;
            Clients.All.online(_userCount);
            //CurrentConnections.Remove(Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            _userCount++;
            Clients.All.online(_userCount);
            //CurrentConnections.Add(Context.ConnectionId);
            return base.OnReconnected();
        }

        public void Send(string name, string message)
        {
            Clients.All.addNewMessageToPage(name, message);
        }
    }
}