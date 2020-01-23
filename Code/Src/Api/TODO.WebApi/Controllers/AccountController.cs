namespace TODO.Web.Controllers
{
    using Business.Account;
    using Configuration;
    using Cryptography;
    using TODO.WebApi.Controllers;
    using JwtManager;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Localization;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using TODO.Model.Account;

    [Route("api/[controller]")]
    public class AccountController : BaseApiController
    {
        #region Private Variables     
        private readonly ICipher _cipher;
        private readonly IAccountBL _accountBl;
        private readonly IAppConfiguration _appConfiguration;
        private readonly ITokenGenerator _tokenGenerator;
        #endregion

        #region Constructor
        public AccountController(
            IAccountBL accountBl,
            ICipher cipher,
            IAppConfiguration appConfiguration,
            ITokenGenerator tokenGenerator
            ) 
        {
            _accountBl = accountBl;
            _cipher = cipher;
            _appConfiguration = appConfiguration;
            _tokenGenerator = tokenGenerator;
        }
        #endregion
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            try
            {
                var response = await _accountBl.Login(loginViewModel.UserName, loginViewModel.Password);

                if (response != null && response.IsActive && !response.IsDeleted)
                {
                        response.Token = _tokenGenerator.GenerateJwtToken(
                                    _appConfiguration.JwtAuthorizationSettings.SecretKey,
                                    _appConfiguration.JwtAuthorizationSettings.Issuer,
                                    _appConfiguration.JwtAuthorizationSettings.Audience,
                                    new List<Claim>
                                    {
                                new Claim(ClaimTypes.Email, Convert.ToString(loginViewModel.UserName)),
                                new Claim("UserId", Convert.ToString(response.Id)),
                                    },
                                    _appConfiguration.JwtAuthorizationSettings.ExpirationTimeInMinute);

                        if (loginViewModel.RememberMe)
                        {
                            var refreshToken = _tokenGenerator.GenerateJwtToken(_appConfiguration.JwtRefreshTokenSettings.SecretKey,
                                    _appConfiguration.JwtRefreshTokenSettings.Issuer,
                                    _appConfiguration.JwtRefreshTokenSettings.Audience,
                                     new List<Claim>
                                    {
                                        new Claim(ClaimTypes.Email, Convert.ToString(loginViewModel.UserName)),
                                        new Claim("UserId", Convert.ToString(response.Id)),
                                    },
                                    _appConfiguration.JwtRefreshTokenSettings.ExpirationTimeInMinute);
                            if (await _accountBl.SaveRefreshToken(refreshToken, response.Id.ToString(), response.UserNo))
                            {
                                response.RefreshToken = refreshToken;
                            }
                        }
                    return Ok(response);
                }
                else
                {
                    HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    return StatusCode((int)HttpStatusCode.Forbidden);
                }
            }
            catch (Exception exception)
            {
                return InternalServerError(exception);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RefreshToken(RefreshTokenModel tokenModel)
        {
            try
            {
                var refreshTokenUserDetails = await _accountBl.GetRefreshToken(tokenModel.RefreshToken);

                if (refreshTokenUserDetails == null)
                {
                    return NotFound("Refresh token not found");
                }

                var token = _tokenGenerator.GenerateJwtToken(
                               _appConfiguration.JwtAuthorizationSettings.SecretKey,
                               _appConfiguration.JwtAuthorizationSettings.Issuer,
                               _appConfiguration.JwtAuthorizationSettings.Audience,
                               new List<Claim>
                               {
                                new Claim(ClaimTypes.Email, refreshTokenUserDetails.Email),
                                 new Claim("UserId", Convert.ToString(refreshTokenUserDetails.Id)),
                               },
                               _appConfiguration.JwtAuthorizationSettings.ExpirationTimeInMinute);
                var refreshToken = _tokenGenerator.GenerateJwtToken(_appConfiguration.JwtRefreshTokenSettings.SecretKey,
                              _appConfiguration.JwtRefreshTokenSettings.Issuer,
                              _appConfiguration.JwtRefreshTokenSettings.Audience,
                               new List<Claim>
                              {
                                        new Claim(ClaimTypes.Email, Convert.ToString( refreshTokenUserDetails.Email)),
                                        new Claim("UserId", Convert.ToString(refreshTokenUserDetails.Id)),
                              },
                              _appConfiguration.JwtRefreshTokenSettings.ExpirationTimeInMinute);
                if (await _accountBl.SaveRefreshToken(refreshToken, refreshTokenUserDetails.Id.ToString(), refreshTokenUserDetails.UserNo.ToString()))
                {
                    refreshTokenUserDetails.RefreshToken = refreshToken;
                }

                return Ok(new { Token = token, RefreshToken = refreshToken });
            }
            catch (Exception exception)
            {
                return InternalServerError(exception);
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> ValidateToken(string token)
        {
            try
            {
                var response = await _accountBl.ValidateToken(token);
                return Ok(response);
            }
            catch (Exception exception)
            {
                return InternalServerError(exception);
            }
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> ValidateForgotPasswordToken(string token)
        {
            try
            {
                var response = await _accountBl.ValidateForgotPasswordToken(token);
                return Ok(response);
            }
            catch (Exception exception)
            {
                return InternalServerError(exception);
            }
        }

        /// <summary>
        ///  Send Forgot Password Link
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> IsUsernameExist(string email)
        {
            try
            {
                return Ok(await _accountBl.IsEmailExist(email));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        /// <summary>
        ///  Send Forgot Password Link
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> SendForgotPasswordEmail(string email)
        {
            try
            {

                return Ok(await _accountBl.SendForgotPasswordEmail(email));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Reset Password
        /// </summary>
        /// <param name="forgotPasswordModel"></param>
        /// <returns></returns>

        [HttpPost("[action]")]
        public async Task<IActionResult> ResetPassword(ForgotPasswordModel forgotPasswordModel)
        {
            try
            {
                return Ok(await _accountBl.ResetPassword(forgotPasswordModel));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        /// <summary>
        /// Resend Confirmation mail
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet("[action]/{email}")]
        public async Task<IActionResult> ResendConfirmationEmail(string email)
        {
            try
            {
                var user = await _accountBl.GetUserDetailsByUsername(email);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// change password for user
        /// </summary>
        /// <param name="changePasswordModel"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel changePasswordModel)
        {
            try
            {
                return Ok(await _accountBl.ChangePassword(changePasswordModel));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
