using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace signalsample.Hubs.Dto
{
    public class SessionContext
    {
        public string ConnectionId { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public string Picture { get; set; }

        public override string ToString()
        {
            return $"{NickName}({Email}) - ConnectionId";
        }
    }
}
