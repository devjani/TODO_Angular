using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TODO.Model.Common;
using TODO.Model.DTO;

namespace TODO.Business.User
{
    public interface IUserBL
    {
        /// <summary>
        /// Add User
        /// </summary>
        /// <param name="userDetails"></param>
        /// <returns></returns>
        Task<bool> AddUser(UsersDTO userDetails);

        /// <summary>
        /// Get User Detail By  Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<UsersDTO> GetUserDetailById(string id);



        /// <summary>
        /// Delete User
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteUser(string id);
    }
}
