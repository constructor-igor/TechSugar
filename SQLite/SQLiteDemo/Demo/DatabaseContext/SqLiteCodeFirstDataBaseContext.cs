using System.Data.Entity;
using System.Data.SQLite;
using System.IO;
using Demo.DatabaseContext.Entries;
using SQLite.CodeFirst;

namespace Demo.DatabaseContext
{
    public class SqLiteCodeFirstDemo
    {
        //https://github.com/msallin/SQLiteCodeFirst
        //http://stackoverflow.com/questions/22101150/sqlite-ef6-programmatically-set-connection-string-at-runtime
        public static void SQLiteCodeFirstDemo()
        {
            string location = System.Reflection.Assembly.GetEntryAssembly().Location;
            string dbFilePath = Path.Combine(Path.GetDirectoryName(location), "demoDb.db");
            //string dbFilePath = @"data source=.\db\demoDb\demoDb.sqlite;foreign keys=true";

            SqLiteCodeFirstDataBaseContext databaseContext = new SqLiteCodeFirstDataBaseContext(dbFilePath);
            databaseContext.GetValidationErrors();
        }

    }
    public class SqLiteCodeFirstDataBaseContext : DbContext
    {
        public SqLiteCodeFirstDataBaseContext(string dbFilePath)
            : base(CreateSqLiteConnection(dbFilePath), true)
        {
            Configuration.ProxyCreationEnabled = true;
            Configuration.LazyLoadingEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            ConfigureModelEntity(modelBuilder);

            var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<SqLiteCodeFirstDataBaseContext>(modelBuilder);
            Database.SetInitializer(sqliteConnectionInitializer);
        }

        private static void ConfigureModelEntity(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ModelEntry>();
        }

        private static SQLiteConnection CreateSqLiteConnection(string dbFilePath)
        {
            var connection = new SQLiteConnection
            {
                ConnectionString = new SQLiteConnectionStringBuilder
                {
                    DataSource = dbFilePath,
                    ForeignKeys = true,
                    DefaultTimeout = 2,                   
                }.ConnectionString
            };
            return connection;
        }
    }
}