/*
 * Author: Peter Cornell
 * Date: 25/11/14
 */
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class AddIncident : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtDate.Text = DateTime.Today.ToShortDateString();
        }

        protected void btnGetCustomer_Click(object sender, EventArgs e)
        {
            DetailsView1.DataBind();
            if (DetailsView1.Rows.Count == 0)
            {
                PopUp.ShowPopUpMsg("Customer ID not found.", this);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (InsertNewRow())
            {
                PopUp.ShowPopUpMsg("New entry successfully added.", this);
                DetailsView1.DataBind();
                txtGetCustomer.Text = "";
                txtTitle.Text = "";
                txtDescription.Text = "";
            }
            else
            {
                PopUp.ShowPopUpMsg("Error. Entry not added.", this);
            }

        }
        /// <summary>
        /// Creates a connection to the database and inserts a new line using information from the form.
        /// </summary>
        /// <returns>bool</returns>
        public bool InsertNewRow()
        {
            string customerID = txtGetCustomer.Text;
            string productCode = cbxProduct.SelectedValue.ToString();
            string name = DetailsView1.Rows[1].Cells[1].Text;
            DateTime dateOpened = DateTime.Today.Date;
            string title = txtTitle.Text;
            string description = txtDescription.Text;

            int rowsUpdated = 0;
            string connectionString = WebConfigurationManager.ConnectionStrings["TechSupportConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);

            string insertStatement =
                  "INSERT Incidents "
                + "(CustomerID, ProductCode, DateOpened, Title, Description)"
                + "VALUES (@CustomerID, @ProductCode, @DateOpened, @Title, @Description)";
            SqlCommand insertCommand = new SqlCommand(insertStatement, connection);
            insertCommand.Parameters.AddWithValue("@CustomerID", customerID);
            insertCommand.Parameters.AddWithValue("@ProductCode", productCode);
            insertCommand.Parameters.AddWithValue("@DateOpened", dateOpened);
            insertCommand.Parameters.AddWithValue("@Title", title);
            insertCommand.Parameters.AddWithValue("@Description", description);
            try
            {
                connection.Open();
                rowsUpdated = insertCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                PopUp.ShowPopUpMsg(ex.Message, this);
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