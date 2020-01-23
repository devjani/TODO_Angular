using MongoDB.BaseEntity;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TODO.Model.Entities
{
    public class Users : MongoDBBaseEntity
    {
        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }

        [BsonElement("mobile")]
        public string Mobile { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("isActive")]
        public bool IsActive { get; set; }

        [BsonElement("createdDate")]
        public DateTime CreatedDate { get; set; }

        [BsonElement("updatedDate")]
        public DateTime UpdatedDate { get; set; }

        [BsonElement("isDeleted")]
        public bool IsDeleted { get; set; }

        [BsonElement("userNo")]
        public string UserNo { get; set; }

        [BsonElement("refreshToken")]
        public string RefreshToken { get; set; }

        [BsonElement("language")]
        public string Language { get; set; }

        [BsonElement("attachmentName")]
        public string AttachmentName { get; set; }

        [BsonElement("blobUrl")]
        public string BlobUrl { get; set; }

    }
}
