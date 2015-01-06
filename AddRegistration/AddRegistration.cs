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

namespace AddRegistration
{
    public partial class frmAddRegistration : Form
    {
        public frmAddRegistration()
        {
            InitializeComponent();
        }

        private void customersBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.customersBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.techSupportDataSet);
        }

        private void frmAddRegistration_Load(object sender, EventArgs e)
        {
            try
            {
                LoadForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            Registration reg = new Registration();
            try
            {
                reg.CustomerID = cbxCustomer.SelectedValue.ToString();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Please select a valid customer", "Customer Error");
            }
            try
            {
                reg.ProductCode = cbxProduct.SelectedValue.ToString();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Please select a valid product", "Product Error");
            }

            DateTime temp;
            if (DateTime.TryParse(txtDate.Text, out temp))
            {
                reg.RegistrationDate = temp;
            }
            else
            {
                MessageBox.Show("Please enter a valid date", "Invalid Date");
            }

            if (RegistrationDB.AddRegistration(reg))
            {
                MessageBox.Show("Registration added", "Operation Completed");   
            }
            else
            {
                MessageBox.Show("Registration not added", "Operation Failed");
            }
            LoadForm();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            LoadForm();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadForm()
        {
            List<Customer> customers = CustomerDB.GetCustomers();
            cbxCustomer.DataSource = customers;
            cbxCustomer.DisplayMember = "Name";
            cbxCustomer.ValueMember = "CustomerID";
            List<Product> products = ProductDB.GetProducts();
            cbxProduct.DataSource = products;
            cbxProduct.DisplayMember = "Name";
            cbxProduct.ValueMember = "ProductCode";
            txtDate.Text = DateTime.Today.ToShortDateString();
        }

    }
}
