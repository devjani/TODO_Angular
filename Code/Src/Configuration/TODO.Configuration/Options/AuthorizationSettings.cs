namespace TODO.Configuration.Options
{
    public class AuthorizationSettings
    {
        public int ExpirationTime { get; set; }

        public int AccessFailedCount { get; set; }

        public int LockoutHours { get; set; }

        public int SessionIdleTimeout { get; set; }

        public int DefaultLockoutTime { get; set; }
    }
}
