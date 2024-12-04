using System;
using System.Windows.Forms;
using InventoryMS.Controllers;
using InventoryMS.Models;
using System.Windows.Forms;
using System.Xml.Linq;

namespace InventoryMS.Views
{
    public partial class ProductForm : Form
    {
        private ProductController _productController;

        public ProductForm()
        {
            InitializeComponent();
            _productController = new ProductController();
            InitializeListView();
            LoadProducts();
        }

        private void InitializeListView()
        {
            // Add columns to the ListView
            listViewProducts.Columns.Add("Product ID", 100, HorizontalAlignment.Left);
            listViewProducts.Columns.Add("Product Name", 200, HorizontalAlignment.Left);
            listViewProducts.Columns.Add("Description", 250, HorizontalAlignment.Left);
            listViewProducts.Columns.Add("Quantity", 100, HorizontalAlignment.Left);
            listViewProducts.Columns.Add("Unit Price", 100, HorizontalAlignment.Right);

            listViewProducts.View = View.Details; // Set view to details
            listViewProducts.FullRowSelect = true; // Allow selecting the entire row
        }

        private void LoadProducts()
        {
            // Clear the ListView before reloading the products
            listViewProducts.Items.Clear();

            // Get the products from the controller
            var products = _productController.GetAllProducts();

            // Add each product to the ListView
            foreach (var product in products)
            {
                ListViewItem item = new ListViewItem(product.Id.ToString());
                item.SubItems.Add(product.Name);
                item.SubItems.Add(product.Description);
                item.SubItems.Add(product.Quantity.ToString());
                item.SubItems.Add(product.UnitPrice.ToString("C"));

                listViewProducts.Items.Add(item);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void ProductForm_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            var product = new Product
            {
                Name = txtName.Text,
                Quantity = int.Parse(txtQuantity.Text),
                Description = txtDescription.Text,
                UnitPrice = double.Parse(txtUnitPrice.Text)
            };

            // Add the product to the database via the controller
            _productController.AddProduct(product);

            // Display success message
            MessageBox.Show("Product Added Successfully!");

            // Refresh the ListView
            LoadProducts();
        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            // Get the Product ID from the Delete ID textbox
            int productId = int.Parse(txtDeleteId.Text);

            // Delete the product from the database via the controller
            _productController.DeleteProduct(productId);

            // Display success message
            MessageBox.Show("Product Deleted Successfully!");

            // Refresh the ListView
            LoadProducts();
        }
    }
}
