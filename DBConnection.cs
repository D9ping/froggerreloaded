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
        /// <summary>
        /// Executes query.
        /// </summary>
        /// <param name="query">SQL query</param>
		public static void SetData(string query)
		{
            OleDbConnection myConnection = GetConnection();
            try
            {
                myConnection.Open();
            }
            catch (Exception exc)
            {
                System.Windows.Forms.MessageBox.Show(exc.Message);
            }
            
            try
            {
                OleDbCommand command = new OleDbCommand(query, myConnection);
                command.ExecuteNonQuery();
            }

            catch (Exception exc)
            {
                System.Windows.Forms.MessageBox.Show(exc.Message);
            }

            finally
            {
                myConnection.Close();
            }
		}

        /// <summary>
        /// Execute query and returns a datables
        /// </summary>
        /// <param name="query">SQL query</param>
        /// <param name="kolomaantal">number coloms datatable</param>
        /// <returns>the DataTable</returns>
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
            catch (Exception exc)
            {
                System.Windows.Forms.MessageBox.Show(exc.Message);
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
            catch (Exception exc)
            {
                System.Windows.Forms.MessageBox.Show(exc.Message);
            }
            finally
            {
                conn.Close();
                cmd.Dispose();
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