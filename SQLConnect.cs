using Microsoft.SqlServer.Server;
using sushi_darom.cs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sushi_darom
{
    public class SQLConnect
    {
        public string connectionString;
        public SqlConnection myConnection;
        public string Server;
        public string Ini_catalog;

        public SQLConnect(string server = "localhost", string ini_catalog = "sushiDB") // указание пути сервера и название базы данных
        {
            Server = server;
            Ini_catalog = ini_catalog;
            connectionString = @"Server=" + server + ";" + "Initial Catalog=" + ini_catalog + ";" + "Integrated Security=True";
            myConnection = new SqlConnection(connectionString);
        }

        public SqlConnection GetConnection()
        {
            return myConnection;
        }

        public void OpenConnection()
        {
            if (myConnection.State == System.Data.ConnectionState.Closed)
            {
                myConnection.Open();
            }
        }

        public void CloseConnection()
        {
            if (myConnection.State == System.Data.ConnectionState.Open)
            {
                myConnection.Close();
            }
        }
    }
}
