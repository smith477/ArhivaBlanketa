using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArhivaBlanketa.Models
{
    public class Subject
    {
        public Subject()
        {
            Sheets = new List<string>();
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("Name")]
        public string SubjectName { get; set; }
        public string Major { get; set; }
        public int Year { get; set; }
        public List<string> Sheets { get; set; }
    }
}
