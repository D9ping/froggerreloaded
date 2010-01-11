using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace Frogger
{
	public static class DBConnection
	{
        /*
		public static OleDbDataReader GetData(string query)
		{
			//string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\highscores.accdb;Persist Security Info=False;";

            OleDbConnection myConnection = null;
            OleDbDataReader reader;
            try
            {
                
                myConnection = new OleDbConnection(connectionString);
                myConnection.Open();
                OleDbCommand command = new OleDbCommand(query, myConnection);
                DataTable table = new DataTable();
                DataRow dr;
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    dr = table.NewRow();
                    for (int i = 0; i <
                }
                foreach (DataRow row in reader)
                
            }
            
            try
            {
                OracleDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Dr = table.NewRow();
                    for (int i = 0; i < kolomaantal; i++)
                    {
                        if (table.Columns.Count != kolomaantal)
                            table.Columns.Add(Convert.ToString(i), typeof(string));
                        Dr[i] = reader.GetValue(i);
                    }
                    table.Rows.Add(Dr);
                }
            }
            
            finally
            {
                myConnection.Close();
            }

			return reader;
		}
*/

		public static void SetData(string query)
		{
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\highscores.mdb;Persist Security Info=False;";

            OleDbConnection myConnection = new OleDbConnection(connectionString);
            myConnection.Open();

			OleDbCommand command = new OleDbCommand(query, myConnection);

			command.ExecuteNonQuery();

			myConnection.Close();
		}

        public static DataTable ExecuteQuery(string query, int kolomaantal)
        {
            OleDbConnection conn = GetConnection();

            DataTable table = new DataTable();
            DataRow Dr;
            string queryString = query;

            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            OleDbCommand cmd = new OleDbCommand(queryString);
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            try
            {
                OleDbDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Dr = table.NewRow();
                    for (int i = 0; i < kolomaantal; i++)
                    {
                        if (table.Columns.Count != kolomaantal)
                            table.Columns.Add(Convert.ToString(i), typeof(string));
                        Dr[i] = reader.GetValue(i);
                    }
                    table.Rows.Add(Dr);
                }
            }


            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                cmd.Dispose();
                conn.Close();
                conn.Dispose();
            }
            return table;
        }

        public static OleDbConnection GetConnection()
        {
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\highscores.mdb;Persist Security Info=False;";            //HOST=192.168.1.22

            try
            {
                OleDbConnection connection = new OleDbConnection(connectionString);
                return connection;
            }
            catch (Exception)
            {
                return null;
            }
        }
	}
}