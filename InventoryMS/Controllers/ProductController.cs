using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using InventoryMS.Models;

namespace InventoryMS.Controllers
{
    public class ProductController
    {
        public List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();
            var connection = DatabaseHelper.GetConnection();
            string query = "SELECT * FROM Products";

            using (var command = new SQLiteCommand(query, connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    products.Add(new Product
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Quantity = reader.GetInt32(2),
                        Description = reader.GetString(3),
                        UnitPrice = reader.GetDouble(4)
                    });
                }
            }
            return products;
        }

        public void AddProduct(Product product)
        {
            var connection = DatabaseHelper.GetConnection();
            string query = "INSERT INTO Products (Name, Quantity, Description, UnitPrice) VALUES (@Name, @Quantity, @Description, @UnitPrice)";

            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Name", product.Name);
                command.Parameters.AddWithValue("@Quantity", product.Quantity);
                command.Parameters.AddWithValue("@Description", product.Description);
                command.Parameters.AddWithValue("@UnitPrice", product.UnitPrice);
                command.ExecuteNonQuery();
            }
        }

        public void DeleteProduct(int productId)
        {
            var connection = DatabaseHelper.GetConnection();
            string query = "DELETE FROM Products WHERE Id = @Id";

            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", productId);
                command.ExecuteNonQuery();
            }
        }
    }
}
