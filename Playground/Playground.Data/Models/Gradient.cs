using LiteDB;
using System;

namespace Playground.Data.Models
{
    public class Gradient
    {
        public Guid Id { get; set; }

        [BsonField("stylesheet")]
        public string Stylesheet { get; set; }

        [BsonField("tags")]
        public string[] Tags { get; set; }

        [BsonField("preview")]
        public bool IsPreview { get; set; }
    }
}