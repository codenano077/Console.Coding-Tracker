using System.Configuration;
using Microsoft.Data.Sqlite;

namespace coding_Tracker
{
    internal class CodingController// this is the controller class that will handle all the database operations for the coding sessions
    {
        string connectionString = ConfigurationManager.AppSettings.Get("ConnectionString");


        internal void Get()// this is to get all the records from the database and display them in a table format
        {
            List<Coding> tableData = new List<Coding>();
            using(var connection = new SqliteConnection(connectionString))
            {
                using(var tableCmd = connection.CreateCommand())
                {
                    connection.Open();
                    tableCmd.CommandText = "SELECT * FROM CodingSessions";
                    using(var reader = tableCmd.ExecuteReader())
                    {
                        if(reader.HasRows)
                        {
                            while(reader.Read())
                            {
                                tableData.Add(
                                    new Coding
                                    {
                                        Id =reader.GetInt32(0),
                                        Date = reader.GetString(1),
                                    Duration = reader.GetString(2)
                                });
                            }
                        }
                        else
                        {
                            Console.WriteLine("\n No records found");
                        }
                    }
                }
                Console.WriteLine("\n\n ");

            }
            TableVisualization.ShowTable(tableData);
        }
         internal void Post(Coding coding)// this is to add a new record to the database
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                using (var tableCmd = connection.CreateCommand())
                {
                    connection.Open();
                    tableCmd.CommandText = $"INSERT INTO CodingSessions (date, duration) VALUES ('{coding.Date}', '{coding.Duration}')";
                    tableCmd.ExecuteNonQuery();
                }
            }
        }

        internal Coding GetById(int id)// this is to get a record by id from the database
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                using(var tableCmd = connection.CreateCommand())
                {
                    connection.Open();
                    tableCmd.CommandText = $"SELECT * FROM CodingSessions WHERE Id = {id}";

                    using(var reader = tableCmd.ExecuteReader())
                    {
                        Coding coding = new ();
                        if(reader.HasRows)
                        {
                            reader.Read();
                            coding.Id = reader.GetInt32(0);
                            coding.Date = reader.GetString(1);
                            coding.Duration = reader.GetString(2);   
                        }

                        Console.WriteLine("\n\n ");

                        return coding;
                    }
                }
            }
        }

        internal void Delete(int id)// this is to delete a record by id from the database
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                using (var tableCmd = connection.CreateCommand())
                {
                    connection.Open();
                    tableCmd.CommandText = $"DELETE FROM CodingSessions WHERE Id = {id}";
                    tableCmd.ExecuteNonQuery();
                }
            }
        }
    
        internal void Update (Coding coding)// this is to update a record by id from the database,
        {
          using (var connection = new SqliteConnection(connectionString))
            {
                using (var tableCmd = connection.CreateCommand())
                {
                    connection.Open();
                    tableCmd.CommandText = 
                    $"UPDATE CodingSessions SET Date = '{coding.Date}', Duration = '{coding.Duration}' WHERE Id = {coding.Id}";
                    tableCmd.ExecuteNonQuery();
                }  
            }
            Console.WriteLine($"\n Record with id {coding.Id} updated successfully\n\n");
        } 
    }

}