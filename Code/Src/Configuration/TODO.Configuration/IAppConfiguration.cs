namespace TODO.Configuration
{
    using TODO.Configuration.Options;
    public interface IAppConfiguration
    {
        AuthorizationSettings AuthorizationSettings { get; }
        JwtAuthorizationSettings JwtAuthorizationSettings { get; }
        JwtConfirmEmailSettings JwtConfirmEmailSettings { get; }
        JwtForgotPasswordSettings JwtForgotPasswordSettings { get; }
        JwtReactivateUserSettings JwtReactivateUserSettings { get; }
        JwtRefreshTokenSettings JwtRefreshTokenSettings { get; }
    }
}
