using System;
using System.Data.SQLite;
using System.IO;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            //SqLiteDemo();
            SqLiteAndEntityFrameworkDemo();
        }

        private static void SqLiteDemo()
        {
            // Creating a database file
            SQLiteConnection.CreateFile("MyDatabase.sqlite");

            // Connecting to a database
            using (SQLiteConnection dbConnection = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;"))
            {
                dbConnection.Open();

                // Creating a table:
                string sql = "create table highscores (name varchar(20), score int)";
                SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
                command.ExecuteNonQuery();

                // Filling our table:
                sql = "insert into highscores (name, score) values ('Me', 3000)";
                command = new SQLiteCommand(sql, dbConnection);
                command.ExecuteNonQuery();

                sql = "insert into highscores (name, score) values ('Myself', 6000)";
                command = new SQLiteCommand(sql, dbConnection);
                command.ExecuteNonQuery();

                sql = "insert into highscores (name, score) values ('And I', 9001)";
                command = new SQLiteCommand(sql, dbConnection);
                command.ExecuteNonQuery();

                // Getting the high scores out of our database
                sql = "select * from highscores order by score desc";
                command = new SQLiteCommand(sql, dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                    Console.WriteLine("Name: " + reader["name"] + "\tScore: " + reader["score"]);
            }
        }

        private static void SqLiteAndEntityFrameworkDemo()
        {
            string location = System.Reflection.Assembly.GetEntryAssembly().Location;
            string dbFilePath = Path.Combine(Path.GetDirectoryName(location), "demo.db");

            var databaseContext = new DatabaseContext.DatabaseContext(dbFilePath);
        }
    }
}
