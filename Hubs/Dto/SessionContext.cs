using MessagePack;

namespace signalsample.Hubs.Dto
{
    [MessagePackObject]
    public class SessionContext
    {
        [Key("connectionId")]
        public string ConnectionId { get; set; }
        [Key("nickName")]
        public string NickName { get; set; }
        [Key("email")]
        public string Email { get; set; }
        [Key("picture")]
        public string Picture { get; set; }

        public override string ToString()
        {
            return $"{NickName}({Email}) - ConnectionId";
        }
    }
}
