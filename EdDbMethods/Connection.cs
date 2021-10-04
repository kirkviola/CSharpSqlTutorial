using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdDbLib
{
    public class Connection
    {
        public SqlConnection sqlConnection { get; set; }
        public string ConnectionString { get; set; }

        public Connection(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public void Open()
        {
            sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();
            if(sqlConnection.State != System.Data.ConnectionState.Open)
            {
                throw new Exception("Connection failed to open");
            }
        }
        
        public void Close()
        {
            if (sqlConnection == null || sqlConnection.State == System.Data.ConnectionState.Open)
                sqlConnection.Close();
        } 
    }
}
