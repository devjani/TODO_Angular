using TODO.Model.Account;
using System.Threading.Tasks;
using TODO.Model.DTO;
using TODO.Model.Entities;

namespace TODO.Business.Account
{
    public interface IAccountBL
    {
        /// <summary>
        /// Authenticate/login user on the basis of given user name and password
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns>Sign in result</returns>
        Task<UsersDTO> Login(string userName, string password);



        /// <summary>
        /// Email Id Exists or not
        /// </summary>
        /// <param name="emailId"></param>
        /// <returns></returns>
        Task<bool> IsEmailExist(string emailId);


        /// <summary>
        /// Validate Token
        /// </summary>
        /// <returns></returns>
        Task<bool> ValidateToken(string token);

        /// <summary>
        /// Send Forgot Password Email 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<bool> SendForgotPasswordEmail(string email);

        /// <summary>
        /// Reset Password
        /// </summary>
        /// <param name="forgotPasswordModel"></param>
        /// <returns></returns>
        Task<bool> ResetPassword(ForgotPasswordModel forgotPasswordModel);

        Task<bool> SaveRefreshToken(string token, string userId, string userNo);

        /// <summary>
        /// get refresh token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<UsersDTO> GetRefreshToken(string token);


        /// <summary>
        /// get user detail by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<UsersDTO> GetUserDetailsByUsername(string username);
        /// <summary>
        /// Validate forgot password token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<bool> ValidateForgotPasswordToken(string token);
        Task<bool> ChangePassword(ChangePasswordModel changePasswordModel);
    }
}
