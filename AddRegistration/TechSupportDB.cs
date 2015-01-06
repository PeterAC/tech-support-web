using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddRegistration
{
    public static class TechSupportDB
    {
        public static SqlConnection GetConnection() 
        {
            string connectionString = AddRegistration.Properties.Settings.Default.TechSupportConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }
    }
}
