using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace signalsample.Hubs.Dto
{
    public class Message
    {
        public string ConnectionId { get; set; }
        public string Text { get; set; }
        public DateTime ServerTime { get; set; }
    }

}
