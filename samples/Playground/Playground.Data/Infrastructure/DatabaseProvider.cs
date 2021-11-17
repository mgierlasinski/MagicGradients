using LiteDB;
using System;
using System.IO;

namespace Playground.Data.Infrastructure
{
    public class DatabaseProvider : IDatabaseProvider
    {
        public LiteDatabase CreateDatabase()
        {
            return new LiteDatabase($"Filename={GetDbPath()};Upgrade=true");
        }

        private string GetDbPath()
        {
            var dir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return Path.Combine(dir, "Gradients.db");
        }
    }
}
