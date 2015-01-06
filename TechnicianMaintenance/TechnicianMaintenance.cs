﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TechnicianMaintenance
{
    public partial class frmTechnicianMaintenance : Form
    {
        public frmTechnicianMaintenance()
        {
            InitializeComponent();
        }

        private void techniciansBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            try
            {
                this.techniciansBindingSource.EndEdit();
                this.tableAdapterManager.UpdateAll(this.techSupportDataSet);
            }
            catch (DBConcurrencyException)
            {
                MessageBox.Show("Some row were not updated.", "Concurrency Error");
                this.techniciansTableAdapter.Fill(this.techSupportDataSet.Technicians);
            }
            catch (DataException ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
                techniciansBindingSource.CancelEdit();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database Error: " + ex.Message, ex.GetType().ToString());
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                this.techniciansTableAdapter.Fill(this.techSupportDataSet.Technicians);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database Error: " + ex.Message, ex.GetType().ToString());
            }

        }
    }
}
