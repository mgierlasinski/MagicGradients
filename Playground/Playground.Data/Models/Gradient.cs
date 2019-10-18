using LiteDB;
using System;

namespace Playground.Data.Models
{
    public class Gradient
    {
        [BsonField("id")]
        public Guid Id { get; set; }

        [BsonField("stylesheet")]
        public string Stylesheet { get; set; }

        [BsonField("tags")]
        public string[] Tags { get; set; }
    }
}