using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace JwtManager
{
  public static class IServiceCollectionExtension
    {
        /// <summary>Configures the JWT token.</summary>
        /// <param name="services">The services.</param>
        /// <param name="issuer">The issuer.</param>
        /// <param name="audience">The audience.</param>
        /// <param name="secretKey">The secret key.</param>
        /// <returns></returns>
        public static IServiceCollection ConfigureJwt(this IServiceCollection services,string issuer,string audience,string secretKey)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });
            services.AddTransient<ITokenGenerator, TokenGenerator>();
            return services;
        }        
    }
}
