using MongoDB.BaseEntity;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TODO.Model.Entities
{
    public class Todos : MongoDBBaseEntity
    {
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("description")]
        public string Description { get; set; }
        [BsonElement("createdDate")]
        public DateTime CreatedDate { get; set; }

        [BsonElement("updatedDate")]
        public DateTime UpdatedDate { get; set; }

    }
}
