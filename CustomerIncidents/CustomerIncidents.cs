using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomerIncidents
{
    public partial class frmCustomerIncidents : Form
    {
        public frmCustomerIncidents()
        {
            InitializeComponent();
        }

        private void customersBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.customersBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.techSupportDataSet);

        }

        private void frmCustomerIncidents_Load(object sender, EventArgs e)
        {
            try
            {
                this.incidentsTableAdapter.Fill(this.techSupportDataSet.Incidents, Convert.ToInt32(customerIDToolStripTextBox.Text));
                this.customersTableAdapter.Fill(this.techSupportDataSet.Customers);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database Error: " + ex.Message, ex.GetType().ToString());
            }
            
        }

        private void fillByCustomerIDToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                int custID = Convert.ToInt32(customerIDToolStripTextBox.Text);
                this.customersTableAdapter.FillByCustomerID(this.techSupportDataSet.Customers, custID);
                
                if (customersTableAdapter.GetData().Any(row => row.CustomerID == custID))
                {
                    this.incidentsTableAdapter.Fill(this.techSupportDataSet.Incidents, custID);
                }
                else
                {
                    MessageBox.Show("Customer not found", "ID error");
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter a valid Customer ID", "Format Exception");
            }

        }

    }
}
