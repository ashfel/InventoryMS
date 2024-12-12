using System;
using System.Data.SQLite;
using InventoryMS.Models;

namespace InventoryMS.Controllers
{
    public class UserController
    {
        public bool VerifyUser(string username, string password)
        {
            bool isValid = false;

            var connection = DatabaseHelper.GetConnection();
            string query = "SELECT * FROM Users WHERE Username = @Username";

            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", username);

                // Open the connection if it's not already open
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string storedPassword = reader.GetString(2); // Assuming password is in the 3rd column
                        isValid = VerifyPassword(password, storedPassword);
                    }
                }
            }

            return isValid;
        }

        private bool VerifyPassword(string enteredPassword, string storedPassword)
        {
            // Compare entered password with the stored password hash (assuming password is hashed)
            return enteredPassword == storedPassword; // Replace with actual password hash verification if necessary
        }

        public void AddUser(string username, string password)
        {
            var connection = DatabaseHelper.GetConnection();
            string query = "INSERT INTO Users (Username, Password) VALUES (@Username, @Password)";

            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password); // Ensure password is hashed before storing

                // Open the connection if it's not already open
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }

                command.ExecuteNonQuery();
            }
        }
    }
}
