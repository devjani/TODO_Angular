using TODO.Configuration;
using TODO.Model.Account;
using TODO.Model.Constants;
using JwtManager;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using TODO.Data.Account;
using Cryptography;
using AutoMapper;
using TODO.Model.DTO;
using TODO.Model.Entities;
using TODO.Data.User;
using TODO.Business.User;

namespace TODO.Business.Account
{
    public class AccountBL : IAccountBL
    {
        #region Private Variables

        private readonly IAppConfiguration _appConfiguration;
        private readonly IAccountRepository _accountRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly ICipher _cipher;
        private readonly IMapper _mapper;
        private readonly IUserBL _userBl;

        #endregion

        #region Constructor

        public AccountBL(
                IAppConfiguration appConfiguration,
                IAccountRepository accountRepository,
                ICipher cipher,
                IUserRepository userRepository,
                ITokenGenerator tokenGenerator,
                IMapper mapper,
                IUserBL userBl)
        {
            _appConfiguration = appConfiguration;
            _accountRepository = accountRepository;
            _cipher = cipher;
            _tokenGenerator = tokenGenerator;
            _mapper = mapper;
            _userRepository = userRepository;
            _userBl = userBl;
        }

        #endregion

        #region public Methods

        /// <summary>
        /// Authenticate/login user on the basis of given user name and password
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns>Sign in result</returns>
        public async Task<UsersDTO> Login(string userName, string password)
        {
            UsersDTO users = new UsersDTO();
            var userId = string.Empty;
            string encryptPassword = _cipher.Encrypt(password);
            var resilt = await _userRepository.GetUserDetailByEmailId(userName);
            var user = _mapper.Map<UsersDTO>(resilt);
            userId = user != null ? user.Id.ToString() : null;

            var result = string.IsNullOrEmpty(userId) ? false : await _accountRepository.Login(userId, encryptPassword);
            if (result && !string.IsNullOrEmpty(userId))
            {
                users = user;
            }
            return users;
        }

        /// <summary>
        /// Email Id Exists or not
        /// </summary>
        /// <param name="emailId"></param>
        /// <returns></returns>
        public async Task<bool> IsEmailExist(string emailId)
        {
            bool result = false;
            result = await _accountRepository.IsUserEmailExist(emailId);
            return result;
        }

        /// <summary>
        /// Validate reset password token
        /// </summary>
        /// <returns></returns>
        public async Task<bool> ValidateForgotPasswordToken(string token)
        {
            bool response = false;
            string email = GetUserNameFromToken(token, _appConfiguration.JwtForgotPasswordSettings.SecretKey,
                _appConfiguration.JwtForgotPasswordSettings.Issuer, _appConfiguration.JwtForgotPasswordSettings.Audience);
            if (!string.IsNullOrEmpty(email))
            {
                response = await IsEmailExist(email);
                return response;
            }
            return response;
        }

        /// <summary>
        /// Validate Token
        /// </summary>
        /// <returns></returns>
        public async Task<bool> ValidateToken(string token)
        {
            bool response = false;
            string email = GetUserNameFromToken(token, _appConfiguration.JwtAuthorizationSettings.SecretKey,
                _appConfiguration.JwtAuthorizationSettings.Issuer, _appConfiguration.JwtAuthorizationSettings.Audience);
            if (!string.IsNullOrEmpty(email))
            {
                var result = await IsEmailExist(email);
                if (result)
                {
                    response = true;
                }
            }
            return response;
        }

        /// <summary>
        /// Send Forgot Password Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<bool> SendForgotPasswordEmail(string email)
        {
            UsersDTO user = new UsersDTO();
            bool result = false;
            bool response = true;
            if (response)
            {
                user = await GetUserDetailsByUsername(email);
                if (user != null)
                {
                    IList<Claim> claims = GetPasswordClaims(_mapper.Map<Users>(user));
                    var token = _tokenGenerator.GenerateJwtToken(
                        _appConfiguration.JwtForgotPasswordSettings.SecretKey,
                        _appConfiguration.JwtForgotPasswordSettings.Issuer,
                        _appConfiguration.JwtForgotPasswordSettings.Audience,
                        claims,
                 _appConfiguration.JwtForgotPasswordSettings.ExpirationTimeInMinute
                    );

                   result = true;
                }
            }
            return result;
        }

        /// <summary>
        /// Reset Password
        /// </summary>
        /// <param name="forgotPasswordModel"></param>
        /// <returns></returns>
        public async Task<bool> ResetPassword(ForgotPasswordModel forgotPasswordModel)
        {
            bool result = false;
            string email = GetUserNameFromToken(forgotPasswordModel.Token, _appConfiguration.JwtForgotPasswordSettings.SecretKey,
                _appConfiguration.JwtForgotPasswordSettings.Issuer, _appConfiguration.JwtForgotPasswordSettings.Audience);
            if (!string.IsNullOrEmpty(email))
            {
                var user = await _userRepository.GetUserDetailByEmailId(email);
                var passwords = user.Password;
                user.Password = _cipher.Encrypt(forgotPasswordModel.Password);
                result = await _userRepository.UpdateUser(user);
            }
            return result;

        }

        public async Task<bool> SaveRefreshToken(string token, string userId, string userNo)
        {
            var user = await _userRepository.GetUserDetailById(userId);
            if (user != null)
            {
                await _accountRepository.SaveRefreshToken(token, user.Id.ToString());
            }
            return true;
        }

        public async Task<UsersDTO> GetRefreshToken(string token)
        {
            UsersDTO user = new UsersDTO();
            var users = await _accountRepository.GetRefreshToken(token);
            if (users != null && users.Id != null)
            {
                user = _mapper.Map<UsersDTO>(await _userBl.GetUserDetailById(users.Id.ToString()));
            }
            return user;

        }

        #endregion

        #region private

        /// <summary>
        /// To get Email from Token by User Claim.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="secretKey"></param>
        /// <param name="issuer"></param>
        /// <param name="Audience"></param>
        /// <returns></returns>
        private string GetUserNameFromToken(string token, string secretKey, string issuer, string Audience)
        {
            string email = string.Empty;
            var principal = _tokenGenerator.GetPrincipal(token,
               secretKey, issuer, Audience);

            if (principal != null)
            {
                ClaimsIdentity identity = (ClaimsIdentity)principal.Identity;
                Claim usernameClaim = identity?.FindFirst(ClaimTypes.Email);
                if (usernameClaim != null) email = usernameClaim.Value;
            }
            else
            {
                return null;
            }
            return email;
        }

        private IList<Claim> GetEmailConfirmationClaims(Users user)
        {
            var claims = new List<Claim>(new[]
            {
                // Issuer
                new Claim(JwtRegisteredClaimNames.Iss, _appConfiguration.JwtConfirmEmailSettings.Issuer),

                // UserName
                new Claim(JwtRegisteredClaimNames.Sub, user.Name),

                // Email is unique
                new Claim(JwtRegisteredClaimNames.Email, user.Email),

                // Unique Id for all Jwt tokes
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            });
            return claims;
        }

        private IList<Claim> GetReactivatedUserClaims(Users user)
        {
            var claims = new List<Claim>(new[]
            {
                // Issuer
                new Claim(JwtRegisteredClaimNames.Iss, _appConfiguration.JwtReactivateUserSettings.Issuer),

                // UserName
                new Claim(JwtRegisteredClaimNames.Sub, user.Name),

                // Email is unique
                new Claim(JwtRegisteredClaimNames.Email, user.Email),

                // Unique Id for all Jwt tokes
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            });
            return claims;
        }

        /// <summary>
        /// Get Password Claims
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private IList<Claim> GetPasswordClaims(Users user)
        {
            var claims = new List<Claim>(new[]
            {
                // Issuer
                new Claim(JwtRegisteredClaimNames.Iss, _appConfiguration.JwtForgotPasswordSettings.Issuer),

                // UserName
                new Claim(JwtRegisteredClaimNames.Sub, user.Name),

                // Email is unique
                new Claim(JwtRegisteredClaimNames.Email, user.Email),

                // Unique Id for all Jwt tokes
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            });
            return claims;
        }

        public Task<bool> UploadImage(UserImage image)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteImage(UserImageDetails image)
        {
            throw new NotImplementedException();
        }

        public async Task<UsersDTO> GetUserDetailsByUsername(string username)
        {
            var users = _mapper.Map<UsersDTO>(await _userRepository.GetUserDetailByEmailId(username));
            return users;
        }

        public async Task<bool> ChangePassword(ChangePasswordModel changePasswordModel)
        {
            return await _userRepository.ChangePassword(Convert.ToString(changePasswordModel.UserId), _cipher.Encrypt(changePasswordModel.OldPassword), _cipher.Encrypt(changePasswordModel.NewPassword));
        }
        #endregion


    }
}
