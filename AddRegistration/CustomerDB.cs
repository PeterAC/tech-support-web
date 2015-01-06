using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace AddRegistration
{
    public static class CustomerDB 
    {
        public static List<Customer> GetCustomers() 
        {
            SqlConnection connection = TechSupportDB.GetConnection();
            connection.Open();
            string selectStatement = "SELECT Name, CustomerID FROM Customers";
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            SqlDataReader reader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);
            List<Customer> customers = new List<Customer>();
            while (reader.Read())
            {
                Customer c = new Customer();
                c.Name = reader["Name"].ToString();
                c.CustomerID = reader["CustomerID"].ToString();
                customers.Add(c);
            }
            reader.Close();

            return customers.OrderBy(x => x.Name).ToList();
        }
    }
}
