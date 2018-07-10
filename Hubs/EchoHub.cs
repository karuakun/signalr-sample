using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace signalsample.Hubs
{
    using Dto;

    public class EchoHub: Hub
    {
        public override Task OnConnectedAsync()
        {
            Clients.All.SendAsync("Global", "connected", Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public async Task Send(Request message)
        {
            await Clients.All.SendAsync("Echo", new Message
            {
                ConnectionId = Context.ConnectionId,
                Text = message.Text,
                ServerTime = DateTime.Now
            });
        }

        public override Task OnDisconnectedAsync(System.Exception exception)
        {
            Clients.All.SendAsync("Global", "disconnected", Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}
