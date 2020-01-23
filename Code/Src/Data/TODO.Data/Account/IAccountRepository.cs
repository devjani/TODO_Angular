using TODO.Model.Account;
using System.Threading.Tasks;
using TODO.Model.Entities;

namespace TODO.Data.Account
{
    public interface IAccountRepository
    {
        /// <summary>
        /// Email Id Exists or not
        /// </summary>
        /// <param name="emailId"></param>
        /// <returns></returns>
        Task<bool> IsUserEmailExist(string emailId);


        /// <summary>
        /// Authenticate User
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="customerNo"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<bool> Login(string userId, string password);


        Task<bool> SaveRefreshToken(string token, string userId);

        Task<Users> GetRefreshToken(string token);
    }
}
