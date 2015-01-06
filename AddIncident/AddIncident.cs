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

namespace AddIncident
{
    public partial class frmAddIncident : Form
    {
        public frmAddIncident()
        {
            InitializeComponent();
        }

        private void customersBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.customersBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.techSupportDataSet);
        }

        private void frmAddIncident_Load(object sender, EventArgs e)
        {
            try
            {
                this.productsTableAdapter.Fill(this.techSupportDataSet.Products);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database Error: " + ex.Message, ex.GetType().ToString());
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void fillByCustIDToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                int custID = Convert.ToInt32(customerIDToolStripTextBox.Text);

                if (customersTableAdapter.GetData().Any(row => row.CustomerID == custID))
                {
                    this.customersTableAdapter.FillByCustID(this.techSupportDataSet.Customers, custID);
                    this.incidentsTableAdapter.FillByCustID(this.techSupportDataSet.Incidents, custID);
                    this.productsTableAdapter.FillProductsFromRegistrations(this.techSupportDataSet.Products, custID);
                    
                    txtDate.Text = DateTime.Today.ToShortDateString();

                    btnAdd.Enabled = true;
                    btnCancel.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Customer ID not found.", "ID Error");
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter a valid Customer ID", "Invalid ID");
                ClearFields();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database Error: " + ex.Message, ex.GetType().ToString());
            }
            catch (DBConcurrencyException)
            {
                MessageBox.Show("Some row were not updated.", "Concurrency Error");
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int custID = Convert.ToInt16(txtCustomerID.Text);
            string product = productsTableAdapter.GetProductCodeByName(cbxProduct.Text).ToString();
            DateTime date = DateTime.Parse(txtDate.Text);
            string title = txtTitle.Text;
            string description = txtDescription.Text;
            string customer = txtName.Text;

            if (title != "" && description != "")
            {
                this.incidentsTableAdapter.Insert(custID, product, date, title, description);
                string output = String.Format("Customer: {0} \n\n Product: {1}", customer, product);
                MessageBox.Show(output, "Incident Added");

                ClearFields();
                customersBindingSource.RemoveCurrent();
                incidentsBindingSource.RemoveCurrent();
            }
            else
            {
                MessageBox.Show("Please ensure all fields have been filled out.", "Empty Fields");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            customerIDToolStripTextBox.Clear();
            txtCustomerID.Clear();
            txtName.Clear();
            txtDate.Clear();
            txtTitle.Clear();
            txtDescription.Clear();
            btnAdd.Enabled = false;
            btnCancel.Enabled = false;
        }

        private void fillProductsFromRegistrationsToolStripButton_Click(object sender, EventArgs e)
        {

        }

    }
}
