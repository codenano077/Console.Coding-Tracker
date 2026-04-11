using System;
using Microsoft.Data.Sqlite;
namespace coding_Tracker
{
    internal class DatabaseManager
    {
        internal void CreateDatabase(string connectionString)// this is to create the database and the table if it doesn't exist
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var cmd = connection.CreateCommand();
                cmd.CommandText =
                @"
                    CREATE TABLE IF NOT EXISTS CodingSessions (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Date TEXT ,
                        Duration TEXT 
                    );
                ";
                cmd.ExecuteNonQuery();

                connection.Close();
            }
        }
    }
}