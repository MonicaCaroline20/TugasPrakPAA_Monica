﻿using Npgsql;
using System.Data;

namespace PrakModul2.Helpers
{
    public class sqlDBHelpers
    {
        private NpgsqlConnection connection;
        private string __constr;
        public sqlDBHelpers(string pCOnstr)
        {
            __constr = pCOnstr;
            connection = new NpgsqlConnection();
            connection.ConnectionString = __constr;
        }
        public NpgsqlCommand getNpgsqlCommand(string query)
        {
            connection.Open();
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            return cmd;
        }
        public void closeConnection() 
        { 
            connection.Close();
        }
    }
}
