using System;
using System.Collections.Generic;
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
        private static Dictionary<string, SessionContext> CurrentContexts { get; set; } =
            new Dictionary<string, SessionContext>();

        public override Task OnConnectedAsync()
        {
            var session = GetSessionContext();
            Clients.All.SendAsync("Global", "connected", GetSessionContext());

            lock (CurrentContexts)
            {
                if (!CurrentContexts.ContainsKey(session.ConnectionId))
                {
                    CurrentContexts.Add(session.ConnectionId, session);
                    Clients.All.SendAsync("ConnectedUserChange", CurrentContexts.Select(c => c.Value).ToList());
                }
            }

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
            var session = GetSessionContext();
            Clients.All.SendAsync("Global", "disconnected", session);

            lock (CurrentContexts)
            {
                if (CurrentContexts.ContainsKey(session.ConnectionId))
                {
                    CurrentContexts.Remove(session.ConnectionId);
                    Clients.All.SendAsync("ConnectedUserChange", CurrentContexts.Select(c => c.Value).ToList());
                }
            }

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
