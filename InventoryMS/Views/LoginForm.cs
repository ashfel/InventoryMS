using System;
using System.Windows.Forms;
using InventoryMS.Controllers;

namespace InventoryMS.Views
{
    public partial class LoginForm : Form
    {
        private UserController _userController;

        public LoginForm()
        {
            InitializeComponent();
            _userController = new UserController();
            txtPassword.PasswordChar = '*';
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please fill both the username and password fields.");
                return;
            }

            // Validate login credentials
            if (_userController.VerifyUser(username, password))
            {
                MessageBox.Show("Login successful!");

                // Open the main form (or dashboard)
                MainForm mainForm = new MainForm();
                mainForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
