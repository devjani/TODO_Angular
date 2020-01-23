using MongoDB.BaseEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace TODO.Model.DTO
{
    public class UsersDTO : MongoDBBaseEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string UserNo { get; set; }
        public string UserId { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
