using AutoMapper;
using Cryptography;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODO.Model.Common;
using TODO.Data.User;
using TODO.Model.DTO;
using TODO.Model.Entities;

namespace TODO.Business.User
{
    public class UserBL:IUserBL
    {
        #region Private Variables
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ICipher _cipher;
        #endregion


        #region Constructor
        public UserBL(IUserRepository userRepository, IMapper mapper, ICipher cipher)                  {
            _userRepository = userRepository;
            _mapper = mapper;
            _cipher = cipher;
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Add User
        /// </summary>
        /// <param name="userDetails"></param>
        /// <returns></returns>
        public async Task<bool> AddUser(UsersDTO userDetails)
        {
            if (userDetails.UserId == null)
            {
                userDetails.Password = _cipher.Encrypt(userDetails.Password);
                var response = await _userRepository.AddUser(_mapper.Map<UsersDTO, Users>(userDetails));
                return true;
            }
            else
            {
                userDetails.Id = ObjectId.Parse(userDetails.UserId);
                await _userRepository.UpdateUser(_mapper.Map<UsersDTO, Users>(userDetails));
                return true;
            }

        }
        /// <summary>
        /// Get UserDetail By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UsersDTO> GetUserDetailById(string id)
        {
            var userListing = await _userRepository.GetUsersListing();
            var users = _mapper.Map<UsersDTO>(userListing.Where(x => x.Id == ObjectId.Parse(id)).FirstOrDefault());
            users.Password = null;
            return users;
        }


        /// <summary>
        /// Delete User
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteUser(string id)
        {
            await _userRepository.DeleteUser(id);
            return true;
        }
        #endregion
    }
}
