using System;
using System.Data.Entity;
using Demo.DatabaseContext.Entries;

namespace Demo.DatabaseContext
{
    public interface IDatabaseContext : IDisposable
    {
        IDbSet<ModelEntry> ModelEntries { get; set; }
        IDbSet<PointEntry> PointEntries { get; set; }
        IDbSet<DataEntry> DataEntries { get; set; }
    }
}