using MongoDB.BaseEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace TODO.Model.DTO
{
    public class TodoDTO: MongoDBBaseEntity
    {
        public string TodoId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
