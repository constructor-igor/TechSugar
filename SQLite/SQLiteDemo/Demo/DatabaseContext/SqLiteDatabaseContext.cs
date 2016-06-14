using System.Data.Entity;
using System.Data.SQLite;

namespace Demo.DatabaseContext
{
    public abstract class SqLiteDatabaseContext : DbContext
    {
        private string m_dbFilePath;
        protected SqLiteDatabaseContext(string dbFilePath)
            : base(CreateSqLiteConnection(dbFilePath), true)
        {
            m_dbFilePath = dbFilePath;
            Configuration.ProxyCreationEnabled = true;
            Configuration.LazyLoadingEnabled = true;

            Database.Initialize(true);
        }

        private static SQLiteConnection CreateSqLiteConnection(string dbFilePath)
        {
            var connection = new SQLiteConnection
            {
                ConnectionString = new SQLiteConnectionStringBuilder
                {
                    DataSource = dbFilePath,
                    ForeignKeys = true,
                    DefaultTimeout = 2
                }.ConnectionString
            };
            return connection;
        }
    }
}