/*
 * Author: Peter Cornell
 * Date: 25/11/14
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AddRegistration;

namespace WebApplication1
{
    public partial class AddRegistration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtDate.Text = DateTime.Today.ToShortDateString(); 
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Registration reg = new Registration();
            try
            {
                reg.CustomerID = cbxCustomer.SelectedValue.ToString();
            }
            catch (NullReferenceException)
            {
                PopUp.ShowPopUpMsg("Please select a valid customer", this);
            }
            try
            {
                reg.ProductCode = cbxProduct.SelectedValue.ToString();
            }
            catch (NullReferenceException)
            {
                PopUp.ShowPopUpMsg("Please select a valid product", this);
            }

            DateTime temp;
            if (DateTime.TryParse(txtDate.Text, out temp))
            {
                reg.RegistrationDate = temp.Date;
            }
            else
            {
                PopUp.ShowPopUpMsg("Please enter a valid date", this);
            }

            try
            {
                if (RegistrationDB.AddRegistration(reg))
                {
                    PopUp.ShowPopUpMsg("Registration added", this);
                    GridView1.DataBind();
                }
                else
                {
                    PopUp.ShowPopUpMsg("Registration not added", this);
                }
            }
            catch (Exception ex)
            {
                PopUp.ShowPopUpMsg("Error: \n\n" + ex.Message + "\n\nPlease try again.", this);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }
    }
}