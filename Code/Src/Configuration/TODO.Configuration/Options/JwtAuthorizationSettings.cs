namespace TODO.Configuration.Options
{
    public class JwtAuthorizationSettings
    {
        public string Issuer { get; set; }

        public string Audience { get; set; }

        public string SecretKey { get; set; }

        public int ExpirationTimeInMinute { get; set; }
    }
}
