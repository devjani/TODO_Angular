using TODO.Configuration.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TODO.Configuration
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection ConfigureConfigurationOptions(this IServiceCollection services,IConfiguration configuration)
        {           
            services.Configure<AuthorizationSettings>(option => configuration.GetSection("Authorization").Bind(option));
            services.Configure<JwtAuthorizationSettings>(option => configuration.GetSection("JwtAuthorizationSettings").Bind(option));
            services.Configure<JwtRefreshTokenSettings>(option => configuration.GetSection("JwtRefreshTokenSettings").Bind(option));
            services.Configure<JwtReactivateUserSettings>(option => configuration.GetSection("JwtReactivateUserSettings").Bind(option));
            services.Configure<JwtConfirmEmailSettings>(option => configuration.GetSection("JwtConfirmEmailSettings").Bind(option));
            services.Configure<JwtForgotPasswordSettings>(option => configuration.GetSection("JwtForgotPasswordSettings").Bind(option));
            return services;
        }
    }
}
