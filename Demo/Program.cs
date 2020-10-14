using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Demo.DataObjects;
using MySql.Data.MySqlClient;
using ZimLabs.Database.MySql;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            // Step 1: Normal constructor
            Console.WriteLine("Normal constructor");
            var connector = new Connector("localhost", "testdatabase", "testuser", "testpassword");
            LoadAndPrintPersons(connector);
            

            // Step 2: Normal constructor with secure password
            Console.WriteLine("Normal constructor with secure password");
            connector = new Connector("localhost", "testdatabase", "testuser", "testpassword".ToSecureString());
            LoadAndPrintPersons(connector);


            // Step 3: With complete connection string
            Console.WriteLine("Constructor wich connection string");
            var conString = new MySqlConnectionStringBuilder
            {
                Server = "localhost",
                Database = "testdatabase",
                UserID = "testuser",
                Password = "testpassword"
            }.ConnectionString.ToSecureString();
            connector = new Connector(conString);
            LoadAndPrintPersons(connector);


            // Step 4: With the custom settings
            Console.WriteLine("Constructor with custom settings");
            var settings =
                new DatabaseSettings("localhost", "testdatabase", "testuser", "testpassword".ToSecureString());
            connector = new Connector(settings);
            LoadAndPrintPersons(connector);

            // Step 5: Print the connection info
            var conStringInfo =
                connector.ConnectionStringInfo(ConnectionInfoType.Server | ConnectionInfoType.Database);
            Console.WriteLine($"Connection: {conStringInfo}");

            Console.WriteLine("Done");
            Console.ReadLine();
        }

        private static void LoadAndPrintPersons(Connector connector)
        {
            var result = LoadPersons(connector);
            PrintPersons(result);

            connector.Dispose();
        }

        private static List<Person> LoadPersons(Connector connector)
        {
            const string personQuery = "SELECT id, firstname, lastname, departmentId FROM person";

            var persons = connector.Connection.Query<Person>(personQuery).ToList();

            const string departmentQuery = "SELECT id, name FROM department";
            var departments = connector.Connection.Query<Department>(departmentQuery).ToList();

            foreach (var person in persons)
            {
                person.Department = departments.FirstOrDefault(f => f.Id == person.DepartmentId);
            }

            return persons;
        }

        private static void PrintPersons(List<Person> persons)
        {
            foreach (var person in persons)
            {
                Console.WriteLine(
                    $"Person: {person.Lastname}, {person.Firstname}; Department: {person.Department.Name}");
            }

            Console.WriteLine();
        }
    }
}
