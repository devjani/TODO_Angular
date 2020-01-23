using Dapper;
using Data.Generic;
using TODO.Model.Account;
using System;
using System.Data;
using System.Threading.Tasks;
using static TODO.Model.Constants.Constants;
using MongoDB.Data.Generic;
using MongoDB.Driver;
using TODO.Model.Entities;

namespace TODO.Data.Account
{
    public class AccountRepository : IAccountRepository
    {

        #region Private Variables
        private readonly IMongoDBRepository<Users> _userRepository;
        #endregion

        #region Constructor
        public AccountRepository(IMongoDBRepository<Users> userRepository)
        {
            _userRepository = userRepository;
        }
        #endregion

        #region Public Methods


        /// <summary>
        /// Email Id Exists or not
        /// </summary>
        /// <param name="emailId"></param>
        /// <returns></returns>
        public async Task<bool> IsUserEmailExist(string emailId)
        {
            var isEmailExist = await _userRepository.AnyAsync(x => !x.IsDeleted && !string.IsNullOrEmpty(emailId) && x.Email.ToLower() == emailId.ToLower());
            return isEmailExist;
        }

        /// <summary>
        /// Authenticate User
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="customerNo"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<bool> Login(string userId, string password)
        {
            return true;
            //FilterDefinition<Passwords> filter;

            //if (!string.IsNullOrEmpty(userId))
            //{
            //    filter = Builders<Passwords>.Filter.Where(x => x.Password == password && !x.IsDeleted &&
            //     (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(x.UserId) && x.UserId.ToLower() == userId.ToLower()));
            //}
            //else
            //{
            //    filter = Builders<Passwords>.Filter.Where(x => x.Password == password && !x.IsDeleted &&
            //         (!string.IsNullOrEmpty(customerNo) && !string.IsNullOrEmpty(x.CustomerNo) && x.CustomerNo.ToLower() == customerNo.ToLower()));
            //}

            //var passwords = await _passwordRepository.FindSingleByFilterDefinitionAsync(filter);

            //return passwords != null ? true : false;
        }

        public async Task<bool> SaveRefreshToken(string token, string passwordId)
        {
            //var filter = Builders<Passwords>.Filter.Where(x => x.Id == ObjectId.Parse(passwordId));
            //var update = Builders<Passwords>.Update.Set(x => x.RefreshToken, token);
            //await _passwordRepository.FindOneAndUpdateAsync(filter, update);
            return true;
        }

        public async Task<Users> GetRefreshToken(string token)
        {
            //var filter = Builders<Passwords>.Filter.Where(x => x.RefreshToken == token);
            //return await _passwordRepository.FindSingleByFilterDefinitionAsync(filter);
            return null;
        }

        public Task<bool> Login(string userId, string customerNo, string password)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}

