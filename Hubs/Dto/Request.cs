using System;
using MessagePack;

namespace signalsample.Hubs.Dto
{
    [MessagePackObject]
    public class Request
    {
        [Key("text")]
        public string Text { get; set; }
        [Key("clientTime")]
        public DateTime ClientTime { get; set; }
    }
}
