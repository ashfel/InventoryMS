using System;
using System.Windows.Forms;
using System.Xml.Linq;
using InventoryMS.Controllers;
using InventoryMS.Models;

namespace InventoryMS.Views
{
    public partial class UpdateSupplierForm : Form
    {
        private int _supplierId;
        private SupplierController _controller;

        public UpdateSupplierForm(int supplierId)
        {
            InitializeComponent();
            _supplierId = supplierId;
            _controller = new SupplierController();
            LoadSupplierData();
        }

        private void LoadSupplierData()
        {
            var supplier = _controller.GetSupplierById(_supplierId);
            if (supplier != null)
            {
                txtName.Text = supplier.Name;
                txtEmail.Text = supplier.Email;
                txtAddress.Text = supplier.Address;
                txtContact.Text = supplier.Contact;
            }
            else
            {
                MessageBox.Show("Supplier not found!");
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var supplier = new Supplier
            {
                Id = _supplierId,
                Name = txtName.Text,
                Email = txtEmail.Text,
                Address = txtAddress.Text,
                Contact = txtContact.Text
            };

            _controller.UpdateSupplier(supplier);
            MessageBox.Show("Supplier Updated Successfully!");
            this.Close();
        }
    }
}
