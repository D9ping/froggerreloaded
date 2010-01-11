using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Data.OleDb;

namespace Frogger
{
	public static class DBConnection
	{

		public static OleDbDataReader GetData(string query)
		{
			string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\highscores.accdb;Persist Security Info=False;";

            OleDbConnection myConnection = null;
            OleDbDataReader reader;
            try
            {
                myConnection = new OleDbConnection(connectionString);
                myConnection.Open();
                OleDbCommand command = new OleDbCommand(query, myConnection);
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
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\highscores.mdb;Persist Security Info=False;";

            OleDbConnection myConnection = new OleDbConnection(connectionString);
            myConnection.Open();

			OleDbCommand command = new OleDbCommand(query, myConnection);

			command.ExecuteNonQuery();

			myConnection.Close();
		}
	}
}