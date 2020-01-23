using System.Collections.Generic;
using System.Security.Claims;

namespace JwtManager
{
    public interface ITokenGenerator
    {
        /// <summary>Generates the JWT token.</summary>
        /// <param name="secretKey">The secret key.</param>
        /// <param name="issuer">The issuer.</param>
        /// <param name="audience">The audience.</param>
        /// <param name="claims">The claims.</param>
        /// <param name="expirationTimeInMinute">The expiration time in minute.</param>
        /// <returns>Token string</returns>
        string GenerateJwtToken(string secretKey, string issuer, string audience, IList<Claim> claims, int expirationTimeInMinute);
        
        /// <summary>
        /// Gets the Principal Claim
        /// </summary>
        /// <param name="token"></param>
        /// <param name="secretKey"></param>
        /// <param name="issuer"></param>
        /// <param name="audience"></param>
        /// <returns></returns>
        ClaimsPrincipal GetPrincipal(string token, string secretKey, string issuer, string audience);
    }
}
