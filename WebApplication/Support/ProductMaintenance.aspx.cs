/*
 * Author: Peter Cornell
 * Date: 25/11/14
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Text;

namespace WebApplication1
{
    public partial class ProductMaintenance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnInsertNew_Click(object sender, EventArgs e)
        {
            this.Label1.Visible = true;
            this.txtProduct.Visible = true;
            this.Label2.Visible = true;
            this.txtName.Visible = true;
            this.Label3.Visible = true;
            this.txtVersion.Visible = true;
            this.Label4.Visible = true;
            this.txtDate.Visible = true;
            this.btnSubmit.Visible = true;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (InsertNewRow())
            {
                PopUp.ShowPopUpMsg("New entry successfully added.", this);
            }
            else
            {
                PopUp.ShowPopUpMsg("Error. Entry not added.", this);
            }
            GridView1.DataBind();

            this.Label1.Visible = false;
            this.txtProduct.Visible = false;
            this.Label2.Visible = false;
            this.txtName.Visible = false;
            this.Label3.Visible = false;
            this.txtVersion.Visible = false;
            this.Label4.Visible = false;
            this.txtDate.Visible = false;
            this.btnSubmit.Visible = false;
        }

        public bool InsertNewRow()
        {
            int rowsUpdated = 0;
            string connectionString = WebConfigurationManager.ConnectionStrings["TechSupportConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);

            string insertStatement =
                  "INSERT Products "
                + "(ProductCode, Name, Version, ReleaseDate)"
                + "VALUES (@ProductCode, @Name, @Version, @ReleaseDate)";
            SqlCommand insertCommand = new SqlCommand(insertStatement, connection);
            insertCommand.Parameters.AddWithValue("@ProductCode", txtProduct.Text);
            insertCommand.Parameters.AddWithValue("@Name", txtName.Text);
            insertCommand.Parameters.AddWithValue("@Version", txtVersion.Text);
            insertCommand.Parameters.AddWithValue("@ReleaseDate", txtDate.Text);
            txtProduct.Text = "";
            txtName.Text = "";
            txtVersion.Text = "";
            txtDate.Text = "";
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