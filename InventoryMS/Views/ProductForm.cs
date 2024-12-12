using System;
using System.Windows.Forms;
using InventoryMS.Controllers;
using InventoryMS.Models;
using System.Linq;

namespace InventoryMS.Views
{
    public partial class ProductForm : Form
    {
        private ProductController _productController;
        private CategoryController _categoryController; // Added for categories

        public ProductForm()
        {
            InitializeComponent();
            _productController = new ProductController();
            _categoryController = new CategoryController();
            InitializeListView();
            LoadProducts();
            LoadCategories(); // Load categories into the dropdown
        }

        private void InitializeListView()
        {
            listViewProducts.Columns.Add("Product ID", 100, HorizontalAlignment.Left);
            listViewProducts.Columns.Add("Product Name", 200, HorizontalAlignment.Left);
            listViewProducts.Columns.Add("Description", 250, HorizontalAlignment.Left);
            listViewProducts.Columns.Add("Quantity", 100, HorizontalAlignment.Left);
            listViewProducts.Columns.Add("Unit Price", 100, HorizontalAlignment.Right);
            listViewProducts.Columns.Add("Category Name", 150, HorizontalAlignment.Left);

            listViewProducts.View = View.Details;
            listViewProducts.FullRowSelect = true;
            listViewProducts.GridLines = true;
        }

        private void LoadProducts()
        {
            listViewProducts.Items.Clear();
            var products = _productController.GetAllProducts();

            foreach (var product in products)
            {
                ListViewItem item = new ListViewItem(product.Id.ToString());
                item.SubItems.Add(product.Name);
                item.SubItems.Add(product.Description);
                item.SubItems.Add(product.Quantity.ToString());
                item.SubItems.Add(product.UnitPrice.ToString("C"));
                item.SubItems.Add(product.CategoryName);

                listViewProducts.Items.Add(item);
            }
        }

        private void LoadCategories()
        {
            var categories = _categoryController.GetAllCategories();

            // Populate dropdown for adding and updating products
            comboCategory.Items.Clear();

            foreach (var category in categories)
            {
                comboCategory.Items.Add(new { Text = category.Name, Value = category.Id });
            }

            comboCategory.DisplayMember = "Text";
            comboCategory.ValueMember = "Value";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int productId = int.Parse(txtUpdateId.Text);
            var updateForm = new UpdateProductForm(productId, _productController, _categoryController);
            updateForm.FormClosed += (s, args) => LoadProducts(); // Refresh products after updating
            txtUpdateId.Text = "";
            updateForm.Show();
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            if (comboCategory.SelectedItem == null)
            {
                MessageBox.Show("Please select a category!");
                return;
            }

            var selectedCategory = (dynamic)comboCategory.SelectedItem;
            var product = new Product
            {
                Name = txtName.Text,
                Quantity = int.Parse(txtQuantity.Text),
                Description = txtDescription.Text,
                UnitPrice = double.Parse(txtUnitPrice.Text),
                CategoryId = selectedCategory.Value
            };

            _productController.AddProduct(product);
            MessageBox.Show("Product Added Successfully!");

            txtName.Text = "";
            txtQuantity.Text = "";
            txtDescription.Text = "";
            txtUnitPrice.Text = "";
            comboCategory.SelectedIndex = -1;
            LoadProducts();
        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            int productId = int.Parse(txtDeleteId.Text);
            _productController.DeleteProduct(productId);
            MessageBox.Show("Product Deleted Successfully!");
            txtDeleteId.Text = "";
            LoadProducts();
        }
    }
}
