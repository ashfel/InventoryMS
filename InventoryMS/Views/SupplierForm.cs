using System;
using System.Collections.Generic;
using System.Windows.Forms;
using InventoryMS.Controllers;
using InventoryMS.Models;

namespace InventoryMS.Views
{
    public partial class SupplierForm : Form
    {
        private SupplierController _controller;

        public SupplierForm()
        {
            InitializeComponent();
            _controller = new SupplierController();
            InitializeListView();
            LoadSuppliers();
        }

        private void InitializeListView()
        {
            // Set up the ListView
            listViewSuppliers.View = View.Details;
            listViewSuppliers.FullRowSelect = true;
            listViewSuppliers.GridLines = true;

            // Add columns
            listViewSuppliers.Columns.Add("Supplier ID", 100, HorizontalAlignment.Left);
            listViewSuppliers.Columns.Add("Supplier Name", 150, HorizontalAlignment.Left);
            listViewSuppliers.Columns.Add("Email", 150, HorizontalAlignment.Left);
            listViewSuppliers.Columns.Add("Address", 200, HorizontalAlignment.Left);
            listViewSuppliers.Columns.Add("Contact", 100, HorizontalAlignment.Left);
        }

        private void LoadSuppliers()
        {
            listViewSuppliers.Items.Clear();
            var suppliers = _controller.GetAllSuppliers();

            foreach (var supplier in suppliers)
            {
                var item = new ListViewItem(supplier.Id.ToString());
                item.SubItems.Add(supplier.Name);
                item.SubItems.Add(supplier.Email);
                item.SubItems.Add(supplier.Address);
                item.SubItems.Add(supplier.Contact);
                listViewSuppliers.Items.Add(item);
            }
        }

        private void ClearForm()
        {
            txtName.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";
            txtContact.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var supplier = new Supplier
            {
                Name = txtName.Text,
                Email = txtEmail.Text,
                Address = txtAddress.Text,
                Contact = txtContact.Text
            };

            _controller.AddSupplier(supplier);
            MessageBox.Show("Supplier Added Successfully!");
            ClearForm();
            LoadSuppliers();
        }

        private void btnD_Click(object sender, EventArgs e)
        {
            int supplierId;
            if (int.TryParse(txtDeleteId.Text, out supplierId))
            {
                _controller.DeleteSupplier(supplierId);
                MessageBox.Show("Supplier deleted successfully!");
                LoadSuppliers();
            }
            else
            {
                MessageBox.Show("Please enter a valid ID.");
            }
        }

        private void btnU_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtUpdateId.Text, out int supplierId))
            {
                UpdateSupplierForm updateForm = new UpdateSupplierForm(supplierId);
                updateForm.ShowDialog();
                LoadSuppliers();
            }
            else
            {
                MessageBox.Show("Please enter a valid Supplier ID!");
            }
        }
    }
}
