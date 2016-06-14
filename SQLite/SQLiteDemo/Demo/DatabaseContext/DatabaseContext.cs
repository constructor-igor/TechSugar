using System.Data.Entity;
using Demo.DatabaseContext.Entries;

namespace Demo.DatabaseContext
{
    public class DatabaseContext : SqLiteDatabaseContext, IDatabaseContext
    {
        public DatabaseContext(string dbFilePath) : base(dbFilePath)
        {
        }

        public IDbSet<ModelEntry> ModelEntries { get; set; }
        public IDbSet<PointEntry> PointEntries { get; set; }
        public IDbSet<DataEntry> DataEntries { get; set; }
    }
}