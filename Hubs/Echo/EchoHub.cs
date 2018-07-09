using Microsoft.AspNetCore.SignalR;

namespace signalsample.Hubs.Echo
{
    public class EchoHub: Hub
    {
        public void Send(string room, string message)
        {
            Clients.All.SendAsync(room, message);
        }
    }

}
