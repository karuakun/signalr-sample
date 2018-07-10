using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace signalsample.Hubs.Dto
{
    public class Request
    {
        public string Text { get; set; }
        public DateTime ClientTime { get; set; }
    }
}
