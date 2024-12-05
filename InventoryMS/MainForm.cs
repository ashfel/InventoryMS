using InventoryMS.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryMS
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var productForm = new ProductForm();
            productForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var categoryForm = new CategoryForm();
            categoryForm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var orderForm = new OrderForm();
            orderForm.Show();
        }
    }
}
