using TODO.Configuration.Options;
using Microsoft.Extensions.Options;

namespace TODO.Configuration
{
    public class AppConfiguration : IAppConfiguration
    {
        public AppConfiguration(
            IOptions<AuthorizationSettings> authorizationSettings,
            IOptions<JwtAuthorizationSettings> jwtAuthorizationSettings,
            IOptions<JwtConfirmEmailSettings> jwtConfirmEmailSettings,
            IOptions<JwtForgotPasswordSettings> jwtForgotPasswordSettings,
            IOptions<JwtReactivateUserSettings> jwtReactiveUserSettings,
            IOptions<JwtRefreshTokenSettings> jwtRefreshTokenSettings
            )
        {
            AuthorizationSettings = authorizationSettings.Value;
            JwtAuthorizationSettings = jwtAuthorizationSettings.Value;
            JwtRefreshTokenSettings = jwtRefreshTokenSettings.Value;
            JwtConfirmEmailSettings = jwtConfirmEmailSettings.Value;
            JwtForgotPasswordSettings = jwtForgotPasswordSettings.Value;
            JwtReactivateUserSettings = jwtReactiveUserSettings.Value;
        }

        public AuthorizationSettings AuthorizationSettings { get; set; }

        public JwtAuthorizationSettings JwtAuthorizationSettings { get; set; }

        public JwtReactivateUserSettings JwtReactivateUserSettings { get; set; }

        public JwtConfirmEmailSettings JwtConfirmEmailSettings { get; set; }
        public JwtForgotPasswordSettings JwtForgotPasswordSettings { get; set; }

        public JwtRefreshTokenSettings JwtRefreshTokenSettings { get; set; }
    }
}
