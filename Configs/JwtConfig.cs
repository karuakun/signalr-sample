namespace signalsample.Configs
{
    public class JwtConfig
    {
        public const string ConfigName = "Jwt";
        /// <summary>
        /// 認証を受けているIdPのアドレス Auth0-Applications-Domain
        /// </summary>
        public string Authority { get; set; }
        /// <summary>
        /// WebApiのアドレス Auth0-APIs-Identifier
        /// </summary>
        public string Audience { get; set; }
        /// <summary>
        /// 認証を受けているサイト（SPAのURL）
        /// </summary>
        public string ValidIssuer { get; set; }

        /// <summary>
        /// ClientId Auth0-Applications-Client ID
        /// </summary>
        public string ValidAudience { get; set; }

        /// <summary>
        /// ClientSecret Auth0-Applications-Client Secret
        /// </summary>
        public string IssuerSigningKey { get; set; }
    }
}