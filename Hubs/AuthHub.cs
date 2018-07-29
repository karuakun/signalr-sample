using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using signalsample.Hubs.Dto;
using Microsoft.AspNetCore.Authorization;

namespace signalsample.Hubs
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class AuthHub:Hub
    {
        public override Task OnConnectedAsync()
        {
            Clients.All.SendAsync("Global", "connected", GetSessionContext());
            return base.OnConnectedAsync();
        }

        public async Task Send(Request message)
        {
            await Clients.All.SendAsync("Echo",
                GetSessionContext(),
                new Message
                {
                    ConnectionId = Context.ConnectionId,
                    Text = message.Text,
                    ServerTime = DateTime.Now
                });
        }

        public override Task OnDisconnectedAsync(System.Exception exception)
        {
            Clients.All.SendAsync("Global", "disconnected", GetSessionContext());
            return base.OnDisconnectedAsync(exception);
        }
        
        public SessionContext GetSessionContext()
        {
            return new SessionContext()
            {
                ConnectionId = Context.ConnectionId,
                Email = Context.User.Claims.FirstOrDefault(c => c.Type == "email")?.Value,
                NickName = Context.User.Claims.FirstOrDefault(c => c.Type == "nickname")?.Value,
                Picture = Context.User.Claims.FirstOrDefault(c => c.Type == "picture")?.Value
            };
        }
    }
}
