using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDB.BaseEntity
{
    /// <summary>
    /// Base class for entities
    /// </summary>
    public abstract partial class MongoDBBaseEntity
    {        
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
    }


}
