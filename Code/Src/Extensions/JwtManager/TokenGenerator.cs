using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtManager
{
    /// <summary>Its an class to generate tokens</summary>
    public class TokenGenerator:ITokenGenerator
    {
        /// <summary>Generates the JWT token.</summary>
        /// <param name="secretKey">The secret key.</param>
        /// <param name="issuer">The issuer.</param>
        /// <param name="audience">The audience.</param>
        /// <param name="claims">The claims.</param>
        /// <param name="expirationTimeInMinute">The expiration time in minute.</param>
        /// <returns>Token string</returns>
        public string GenerateJwtToken(string secretKey,string issuer,string audience, IList<Claim> claims,int expirationTimeInMinute)
        {
            var encryptedSecretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var signinCredentials = new SigningCredentials(encryptedSecretKey, SecurityAlgorithms.HmacSha256);

            var tokeOptions = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims:claims,
                expires: DateTime.Now.AddMinutes(expirationTimeInMinute),
                signingCredentials: signinCredentials
            );

           return new JwtSecurityTokenHandler().WriteToken(tokeOptions);
        }


        /// <summary>
        /// Gets the Principal Claim
        /// </summary>
        /// <param name="token"></param>
        /// <param name="secretKey"></param>
        /// <param name="issuer"></param>
        /// <param name="audience"></param>
        /// <returns></returns>
        public ClaimsPrincipal GetPrincipal(string token , string secretKey, string issuer, string audience)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
                if (jwtToken == null)
                    return null;
                TokenValidationParameters parameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
                SecurityToken securityToken;
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token,
                    parameters, out securityToken);
                return principal;
            }
            catch
            {
                return null;
            }
        }


    }
}
