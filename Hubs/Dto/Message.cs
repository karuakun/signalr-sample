using System;
using MessagePack;

namespace signalsample.Hubs.Dto
{
    [MessagePackObject]
    public class Message
    {
        [Key("connectionId")]
        public string ConnectionId { get; set; }
        [Key("text")]
        public string Text { get; set; }
        [Key("serverTime")]
        public DateTime ServerTime { get; set; }
    }

}
