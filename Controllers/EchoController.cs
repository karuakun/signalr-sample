using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using signalsample.Hubs;
using signalsample.Hubs.Dto;

namespace signalsample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EchoController : ControllerBase
    {
        private readonly IHubContext<EchoHub> _hubContext;

        public EchoController(IHubContext<EchoHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> Send(string message)
        {
            await _hubContext.Clients.All.SendAsync("Echo", new Message
            {
                ConnectionId = "api",
                Text = message,
                ServerTime = DateTime.Now
            });
            return Ok();
        }
    }
}