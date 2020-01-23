using System;
using System.Collections.Generic;
using System.Text;

namespace TODO.Configuration.Options
{
    public class JwtReactivateUserSettings
    {
        public string Issuer { get; set; }

        public string Audience { get; set; }

        public string SecretKey { get; set; }

        public int ExpirationTimeInMinute { get; set; }
    }
}
