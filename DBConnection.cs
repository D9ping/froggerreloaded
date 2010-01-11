using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Data.OleDb;

namespace Frogger
{
	public static class DBConnection
	{
		public static SqlDataReader GetData(string query)
		{
			string connectionString = "???";

            SqlConnection myConnection = null;
            SqlDataReader reader;
            try
            {
                myConnection = new SqlConnection(connectionString);
                myConnection.Open();
                SqlCommand command = new SqlCommand(query, myConnection);
                reader = command.ExecuteReader();
            }
            finally
            {
                myConnection.Close();
            }

			return reader;
		}

		public static void SetData(string query)
		{
			string connectionString = "???";

			SqlConnection myConnection = new SqlConnection(connectionString);
			myConnection.Open();

			SqlCommand command = new SqlCommand(query, myConnection);

			command.ExecuteNonQuery();

			myConnection.Close();
		}
	}
}
