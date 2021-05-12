using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace NuxtSignalRApi.Hubs
{
    public class VisitorHub : Hub
    {
        public override Task OnConnectedAsync ()
        {
            return base.OnConnectedAsync ();
        }

        public override Task OnDisconnectedAsync (Exception exception)
        {
            return base.OnDisconnectedAsync (exception);
        }

        public async Task SendVisitorCount ()
        {
            await Clients.All.SendAsync ("ReceiveVisitorCount");
        }
    }
}