using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InventoryMS.Controllers;
using InventoryMS.Models;

namespace InventoryMS.Views
{
    public partial class CategoryForm : Form
    {
        private CategoryController _categoryController;

        public CategoryForm()
        {
            InitializeComponent();
            _categoryController = new CategoryController();
            InitializeListView();
            LoadCategories();
        }

        private void InitializeListView()
        {
            // Configure ListView for categories
            listViewCategories.Columns.Add("Category ID", 100, HorizontalAlignment.Left);
            listViewCategories.Columns.Add("Name", 200, HorizontalAlignment.Left);
            listViewCategories.Columns.Add("Description", 250, HorizontalAlignment.Left);

            listViewCategories.View = View.Details;
            listViewCategories.FullRowSelect = true;
            listViewCategories.GridLines = true;
        }

        private void LoadCategories()
        {
            // Clear the ListView before reloading
            listViewCategories.Items.Clear();

            // Fetch all categories
            var categories = _categoryController.GetAllCategories();

            foreach (var category in categories)
            {
                ListViewItem item = new ListViewItem(category.Id.ToString());
                item.SubItems.Add(category.Name);
                item.SubItems.Add(category.Description);

                listViewCategories.Items.Add(item);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close(); // Close the form
        }

        private void label4_Click(object sender, EventArgs e)
            {

            }

        private void button1_Click(object sender, EventArgs e)
        {
            var category = new Category
            {
                Name = txtCategoryName.Text,
                Description = txtCategoryDescription.Text
            };

            // Add the category
            _categoryController.AddCategory(category);

            // Show success message
            MessageBox.Show("Category added successfully!");

            // Refresh the ListView
            LoadCategories();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtDeleteCategoryId.Text, out int id))
            {
                // Delete the category
                _categoryController.DeleteCategory(id);

                // Show success message
                MessageBox.Show("Category deleted successfully!");

                // Refresh the ListView
                LoadCategories();
            }
            else
            {
                MessageBox.Show("Please enter a valid Category ID.");
            }
        }
    }
}
