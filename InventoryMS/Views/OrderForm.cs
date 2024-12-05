using System;
using System.Windows.Forms;
using InventoryMS.Controllers;
using InventoryMS.Models;

namespace InventoryMS.Views
{
    public partial class OrderForm : Form
    {
        private OrderController orderController;

        public OrderForm()
        {
            InitializeComponent();
            orderController = new OrderController();
            InitializeListView();
            LoadOrders();
        }

        private void InitializeListView()
        {
            listViewOrders.View = View.Details;
            listViewOrders.FullRowSelect = true;
            listViewOrders.GridLines = true;

            // Define columns for ListView
            listViewOrders.Columns.Add("Order ID", 70, HorizontalAlignment.Left);
            listViewOrders.Columns.Add("Order Date", 100, HorizontalAlignment.Left);
            listViewOrders.Columns.Add("Order Detail", 150, HorizontalAlignment.Left);
            listViewOrders.Columns.Add("Price", 70, HorizontalAlignment.Right);
            listViewOrders.Columns.Add("Quantity", 70, HorizontalAlignment.Center);
            listViewOrders.Columns.Add("Status", 80, HorizontalAlignment.Left);
            listViewOrders.Columns.Add("Total Amount", 100, HorizontalAlignment.Right);
        }

        private void LoadOrders()
        {
            listViewOrders.Items.Clear();
            var orders = orderController.GetAllOrders();

            foreach (var order in orders)
            {
                var item = new ListViewItem(order.Id.ToString());
                item.SubItems.Add(order.OrderDate);
                item.SubItems.Add(order.OrderDetail);
                item.SubItems.Add(order.Price.ToString("F2"));
                item.SubItems.Add(order.Quantity.ToString());
                item.SubItems.Add(order.Status);
                item.SubItems.Add(order.TotalAmount.ToString("F2"));
                listViewOrders.Items.Add(item);
            }
        }

        private void button1_Click(object sender, EventArgs e) // Add Order
        {
            if (string.IsNullOrEmpty(txtOrderDetail.Text) || string.IsNullOrEmpty(txtPrice.Text) ||
                string.IsNullOrEmpty(txtQuantity.Text) || string.IsNullOrEmpty(txtStatus.Text))
            {
                MessageBox.Show("Please fill all the fields.");
                return;
            }

            var order = new Order
            {
                OrderDate = DateTime.Now.ToString("yyyy-MM-dd"),
                OrderDetail = txtOrderDetail.Text,
                Price = double.Parse(txtPrice.Text),
                Quantity = int.Parse(txtQuantity.Text),
                Status = txtStatus.Text,
                TotalAmount = double.Parse(txtPrice.Text) * int.Parse(txtQuantity.Text)
            };

            orderController.AddOrder(order);
            LoadOrders();
            ClearInputs();
        }

        private void button2_Click(object sender, EventArgs e) // Delete Order
        {
            if (string.IsNullOrEmpty(txtId.Text))
            {
                MessageBox.Show("Please enter an ID to delete.");
                return;
            }

            int orderId;
            if (!int.TryParse(txtId.Text, out orderId))
            {
                MessageBox.Show("Invalid ID format.");
                return;
            }

            orderController.DeleteOrder(orderId);
            LoadOrders();
            txtId.Clear();
        }

        private void ClearInputs()
        {
            txtOrderDetail.Clear();
            txtPrice.Clear();
            txtQuantity.Clear();
            txtStatus.Clear();
        }
    }
}
