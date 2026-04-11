using System;
using System.Configuration;
namespace coding_Tracker
{
    class Program
    {
        static string connectionString =  ConfigurationManager.AppSettings.Get("ConnectionString");// this is to connect to database
        static void Main(String [] args)
        {
            DatabaseManager databaseManager = new();// this is to create database if it doesn't exist
            GetUserInput getUserInput = new();// this is to get user input and process it
            databaseManager.CreateDatabase(connectionString);

            getUserInput.Mainmenu();
        }
    }
}