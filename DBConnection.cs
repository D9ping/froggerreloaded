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
			string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\highscores.accdb;Persist Security Info=False;";

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
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\highscores.accdb;Persist Security Info=False;";

			SqlConnection myConnection = new SqlConnection(connectionString);
			myConnection.Open();

			SqlCommand command = new SqlCommand(query, myConnection);

			command.ExecuteNonQuery();

			myConnection.Close();
		}

        public static void VoegHighscoreToe(DateTime tijddatum, string naam, int speeltijd, int level)
        {
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\highscores.accdb;Persist Security Info=False;";

            SqlConnection myConnection = new SqlConnection(connectionString);
            myConnection.Open();

            //INSERT INTO table_name
            //VALUES (value1, value2, value3,...)
            string query = "INSERT INTO HIGHSCORES VALUES ('" + tijddatum.ToString() + "', '" + naam + "', '" + speeltijd.ToString() + "', '" + level.ToString() + "')";

            SqlCommand command = new SqlCommand(query, myConnection);

            command.ExecuteNonQuery();

            myConnection.Close();
        }
	}
}
