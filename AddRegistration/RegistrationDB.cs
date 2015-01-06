using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddRegistration
{
    public static class RegistrationDB
    {
        public static bool AddRegistration(Registration reg)
        {
            int rowsUpdated = 0;
            SqlConnection connection = TechSupportDB.GetConnection();
            
            string insertStatement =
                  "INSERT Registrations "
                + "(CustomerID, ProductCode, RegistrationDate)"
                + "VALUES (@CustomerID, @ProductCode, @RegistrationDate)";
            SqlCommand insertCommand = new SqlCommand(insertStatement, connection);
            insertCommand.Parameters.AddWithValue("@CustomerID", reg.CustomerID);
            insertCommand.Parameters.AddWithValue("@ProductCode", reg.ProductCode);
            insertCommand.Parameters.AddWithValue("@RegistrationDate", reg.RegistrationDate);

            try
            {
                connection.Open();
                rowsUpdated = insertCommand.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                connection.Close();  
            }

            if (rowsUpdated != 0)
                return true;
            else
                return false;
            
        }
    }
}
