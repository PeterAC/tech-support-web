/*
 * Author: Peter Cornell
 * Date: 25/11/14
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class CustomerIncidents : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGetCustomer_Click(object sender, EventArgs e)
        {
            
            DetailsView1.DataBind();
            if (DetailsView1.Rows.Count == 0)
            {
                PopUp.ShowPopUpMsg("Customer ID not found.", this);
            }
        }
    }
}