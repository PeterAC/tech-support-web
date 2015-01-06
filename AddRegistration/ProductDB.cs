using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddRegistration
{
    public static class ProductDB
    {
        public static List<Product> GetProducts()
        {
            SqlConnection connection = TechSupportDB.GetConnection();
            connection.Open();
            string selectStatement = "SELECT Name, ProductCode FROM Products";
            SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
            SqlDataReader reader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);
            List<Product> products = new List<Product>();
            while (reader.Read())
            {
                Product p = new Product();
                p.Name = reader["Name"].ToString();
                p.ProductCode = reader["ProductCode"].ToString();
                products.Add(p);
            }
            reader.Close();

            return products.OrderBy(x => x.Name).ToList();
        }
    }
}
