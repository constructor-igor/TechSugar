using System;
using System.Linq;
using Cassandra;

namespace CassandraDB
{
    class Program
    {
        static void Main(string[] args)
        {
            // Connect to the demo keyspace on our cluster running at 127.0.0.1
            Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
            ISession session = cluster.Connect("demo");

            // Insert Bob
            session.Execute("insert into users (lastname, age, city, email, firstname) values ('Jones', 35, 'Austin', 'bob@example.com', 'Bob')");
            session.Execute("insert into users (lastname, age, city, email, firstname) values ('Jackson', 35, 'Austin', 'bob@example.com', 'Michael')");

            // Read Bob's information back and print to the console
            Row result = session.Execute("select * from users where lastname='Jones'").First();
            Console.WriteLine("{0} {1}", result["firstname"], result["age"]);

            // Update Bob's age and then read it back and print to the console
            session.Execute("update users set age = 36 where lastname = 'Jones'");
            result = session.Execute("select * from users where lastname='Jones'").First();
            Console.WriteLine("{0} {1}", result["firstname"], result["age"]);

            // Delete Bob, then try to read all users and print them to the console
            session.Execute("delete from users where lastname = 'Jones'");

            RowSet rows = session.Execute("select * from users");
            foreach (Row row in rows)
                Console.WriteLine("{0} {1}", row["firstname"], row["age"]);

            // Wait for enter key before exiting
            Console.ReadLine();

//            //Create a cluster instance using 3 cassandra nodes.
//            var cluster = Cluster.Builder()
//              .AddContactPoints("127.0.0.1")
//              .Build();
//            //Create connections to the nodes using a keyspace
//            var session = cluster.Connect("sample_keyspace");
//            //Execute a query on a connection synchronously
//            var rs = session.Execute("SELECT * FROM sample_table");
//            //Iterate through the RowSet
//            foreach (var row in rs)
//            {
//                var value = row.GetValue<int>("sample_int_column");
//                //do something with the value
//            }
        }
    }
}
