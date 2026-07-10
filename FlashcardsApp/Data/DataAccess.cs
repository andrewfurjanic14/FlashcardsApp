using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashcardsApp.Data
{
    internal static class DataAccess
    {
        public static SqlConnection GetConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }
    }
}
