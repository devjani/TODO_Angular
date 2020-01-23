using System.Collections.Generic;
using System.Threading.Tasks;
using TODO.Model.Entities;

namespace TODO.Data.User
{
    public interface IUserRepository
    {

        /// <summary>
        /// Add User
        /// </summary>
        /// <param name="userDetails"></param>
        /// <returns></returns>
        Task<Users> AddUser(Users user);

        /// <summary>
        /// Get User Detail By  Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Users> GetUserDetailById(string id);

        Task<bool> UpdateUser(Users users);
        /// <summary>
        /// Delete User
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteUser(string id);
        Task<List<Users>> GetUsersListing();
        Task<Users> GetUserDetailByEmailId(string emailId);
        Task<bool> ChangePassword(string userId, string oldPassword, string newPassword);
    }
}
