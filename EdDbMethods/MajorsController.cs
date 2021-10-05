using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdDbLib
{
    public class MajorsController
    {
        private SqlConnection sqlConn { get; set; }
        public MajorsController(Connection sqlConnection) // Constructor places the connection object as a prop of the controller
        {
            this.sqlConn = sqlConnection.sqlConnection;
        }
        public int Remove(int key)
        {
            var sql = "Delete from Major " +
                    $" where Id = {key}";
            var cmd = new SqlCommand(sql, sqlConn);
            var rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected;
        }
        public int Change(Major major)
        {

            //var sql = where
            //    ? " UPDATE Major SET " +
            //        $" {major.MinSAT} = {change}  " +
            //        $" {whereClause}"
            //    : " UPDATE Major SET " +
            //        $" {major.MinSAT} = {change}";

            var sql = "UPDATE Major SET " +
                    $" Code = '{major.Code}', " +
                    $" Description = '{major.Description}', " +
                    $" MinSAT = {major.MinSAT} " +
                    $" Where Id = {major.Id};";
            var cmd = new SqlCommand(sql, sqlConn);
            var rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected;
        }

        public int Create(Major major)
        {
            var sql = " INSERT Major (Code, Description, MinSAT) " +
                       $"VALUES ('{major.Code}', '{major.Description}', {major.MinSAT}) ";
            var cmd = new SqlCommand(sql, sqlConn);
            var rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected;

        }
        public List<Major> GetAll() // Non static for now - will make changes requiring instanced.
        {
            var sql = "Select * from Major ";
            var cmd = new SqlCommand(sql, this.sqlConn);
            var reader = cmd.ExecuteReader(); // This opens the reader.
            var Majors = new List<Major>();
            while (reader.Read())
            {
                var major = new Major()
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Code = reader["Code"].ToString(),
                    Description = reader["Description"].ToString(),
                    MinSAT = Convert.ToInt32(reader["MinSAT"])
                };
                Majors.Add(major);
            }
            reader.Close(); // DO NOT FORGET THESE
            return Majors;
        }

        public Major? GetByPk(int Id)
        {
            var sql = $"Select * from Major where Id = {Id};";
            var cmd = new SqlCommand(sql, sqlConn);
            var reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                reader.Close();
                return null;
            }
            reader.Read(); // Can't forget about this one.
            var major = new Major()
            {
                Id = Convert.ToInt32(reader["Id"]),
                Code = reader["Code"].ToString(),
                Description = reader["Description"].ToString(),
                MinSAT = Convert.ToInt32(reader["minSAT"])
            };
            reader.Close();
            return major;
        }
        public Major? GetByCode(string Code)
        {
            var sql = "Select * From Major " +
                    $" where Code = {Code} ";
            var cmd = new SqlCommand(sql, sqlConn);
            var reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                reader.Close();
                return null;
            }
            reader.Read();
            var major = new Major()
            {
                Id = Convert.ToInt32(reader["Id"]),
                Code = reader["Code"].ToString(),
                Description = reader["Description"].ToString(),
                MinSAT = Convert.ToInt32(reader["minSAT"])
            };
            reader.Close();
            return major;
        }
        //public int CustomChange(List<Major> majorList)
        //{
        //    var sql = "";
            
        //    foreach(var major in majorList)
        //    {
        //        var sql = "Update Major Set " +
        //                $" Code = '{major.Code}', " +
        //                $" Description = '{major.Description}' " +
        //                $" MinSAT = {major.MinSAT} " +
        //                $" Where Id = {major.Id}";
        //        var cmd = new SqlCommand(sql, sqlConn);
               
        //    }
        //    var rowsAffected = cmd.
        //}
    }        

}
