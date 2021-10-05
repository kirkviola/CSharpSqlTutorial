using Microsoft.Data.SqlClient; // Discovered from the new SqlConnection object
using System;
using System.Collections.Generic;
using EdDbLib;

namespace CSharpSqlTutorial
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create and open the connection
            var connection = new Connection("server=localhost\\sqlexpress;database=EdDb;trusted_connection=true;");
            connection.Open();

            var majorsCtrl = new MajorsController(connection);
            var major = majorsCtrl.GetByPk(1);          
            Console.WriteLine(major);
            major.Description = "General Business";
            var rowsAffected = majorsCtrl.Change(major);
            if(rowsAffected != 1)
                Console.WriteLine("Update failed!");

            //var newMajor = new Major()
            //{
            //    Id = 0,
            //    Code = "UWBW",
            //    Description = "Basket Weaving - Underwater",
            //    MinSAT = 1590
            //};
            //rowsAffected = majorsCtrl.Create(newMajor);
            //if (rowsAffected != 1)
            //    Console.WriteLine("create failed!");

            //var keyId = 9;
            //if (majorsCtrl.GetByPk(keyId) != null)
            //    rowsAffected = majorsCtrl.Remove(keyId);
            //else
            //    Console.WriteLine("Delete Failed");
            var MajorsAll = majorsCtrl.GetAll();
            foreach (var line in MajorsAll)
                Console.WriteLine(line);
            //Close the connections here

            connection.Close();
        }
        static void X() { 
            // 1. gets connection string 2. creates an instance of a sql connection 3. Opens connection to database.
            // 4. Checks that the connection is open.
            var connString = "server=localhost\\sqlexpress;database=EdDb;trusted_connection=true;";
            var sqlConn = new SqlConnection(connString);
            sqlConn.Open();
            if(sqlConn.State != System.Data.ConnectionState.Open)
            {
                Console.WriteLine("Connection did not open");
                return;
            }
            Console.WriteLine("Connection opened");

            // Sql commands happen here
            var sql = "Select * from Student " +
                " where gpa >= 2.5 and gpa <= 3.5 " +
                " order by SAT desc;"; // Statement to be passed into command constructor
            var cmd = new SqlCommand(sql, sqlConn); // Makes command object with the select statement and connection string
            var reader = cmd.ExecuteReader(); // Creates reader object
            var Students = new List<Student>();
            while (reader.Read())
            {
                var student = new Student();
                student.Id = Convert.ToInt32(reader["Id"]);
                student.Firstname = reader["Firstname"].ToString();
                student.Lastname = reader["Lastname"].ToString();
                student.StateCode = reader["StateCode"].ToString();
                //int? SAT = null;
                //if (!reader["SAT"].Equals(DBNull.Value))
                //    SAT = Convert.ToInt32(reader["SAT"]);
                student.SAT = reader["SAT"].Equals(DBNull.Value)
                    ? (int?)null // The (int?) tells C# specifically this is a nullable int, not just a null
                    : Convert.ToInt32(reader["SAT"]);
                student.GPA = Convert.ToDecimal(reader["GPA"]);
                student.MajorId = reader["MajorId"].Equals(DBNull.Value)
                    ? (int?)null
                    : Convert.ToInt32(reader["MajorId"]);
                Console.WriteLine(student);
                Students.Add(student);
            }
            reader.Close();
            sqlConn.Close();
       
        }
    }
}
