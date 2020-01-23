using MongoDB.Bson;
using MongoDB.Data.Generic;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TODO.Model.DTO;
using TODO.Model.Entities;

namespace TODO.Data.User
{
    public class UserRepository: IUserRepository
    {
        #region Private Variables
        private readonly IMongoDBRepository<Users> _userRepository;
        #endregion

        #region Constructor
        public UserRepository(IMongoDBRepository<Users> userRepository
        )
        {
            _userRepository = userRepository;
        }
        #endregion

        /// <summary>
        /// Add User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<Users> AddUser(Users user)
        {
            user.CreatedDate = DateTime.UtcNow;
            user.UpdatedDate = DateTime.UtcNow;
            await _userRepository.InsertAsync(user);
            return user;
        }

        /// <summary>
        /// Get Users Listing
        /// </summary>
        /// <returns></returns>
        public async Task<List<Users>> GetUsersListing()
        {
            return await _userRepository.Table.ToListAsync();
        }


        /// <summary>
        /// Update User
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        public async Task<bool> UpdateUser(Users users)
        {
            var filter = Builders<Users>.Filter.Where(x => x.Id == users.Id);
            var update = Builders<Users>.Update.Set(x => x.UserNo, users.UserNo)
                                               .Set(x => x.Mobile, users.Mobile)
                                               .Set(x => x.Name, users.Name)
                                               .Set(x => x.IsActive, users.IsActive)
                                               .Set(x => x.Language, users.Language)
                                               .Set(x => x.UpdatedDate, DateTime.UtcNow)
                                               .Set(x => x.BlobUrl, users.BlobUrl)
                                               .Set(x => x.AttachmentName, users.AttachmentName);

            await _userRepository.FindOneAndUpdateAsync(filter, update);
            return true;
        }

        /// <summary>
        /// Delete User
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteUser(string id)
        {
            var filter = Builders<Users>.Filter.Where(x => x.Id == ObjectId.Parse(id));
            var update = Builders<Users>.Update.Set(x => x.IsDeleted, true);
            await _userRepository.FindOneAndUpdateAsync(filter, update);
            return true;
        }

        public async Task<Users> GetUserDetailByEmailId(string emailId)
        {
            var filter = Builders<Users>.Filter.Where(x => x.Email.ToLower() == emailId.ToLower() && x.IsActive && !x.IsDeleted);
            return await _userRepository.FindSingleByFilterDefinitionAsync(filter);
        }
        public async Task<bool> ChangePassword(string userId, string oldPassword, string newPassword)
        {
            FilterDefinition<Users> filter;
                filter = Builders<Users>.Filter.Where(x => x.Id == ObjectId.Parse(userId) && x.Password == oldPassword);

            var user = await _userRepository.FindSingleByFilterDefinitionAsync(filter);
            if (user != null)
            {
                user.Password = newPassword;
                await _userRepository.UpdateAsync(user);
                return true;
            }
            return false;

        }

        public async Task<Users> GetUserDetailById(string id)
        {
            FilterDefinition<Users> filter;
            filter = Builders<Users>.Filter.Where(x => x.Id == ObjectId.Parse(id));
            var user = await _userRepository.FindSingleByFilterDefinitionAsync(filter);
            return user;
        }
    }
}
