﻿using System.Collections.Generic;
using System.Data.SQLite;
using InventoryMS.Models;

namespace InventoryMS.Controllers
{
    public class CategoryController
    {
        public List<Category> GetAllCategories()
        {
            List<Category> categories = new List<Category>();
            var connection = DatabaseHelper.GetConnection();
            string query = "SELECT * FROM Categories";

            using (var command = new SQLiteCommand(query, connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    categories.Add(new Category
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Description = reader.IsDBNull(2) ? "" : reader.GetString(2)
                    });
                }
            }
            return categories;
        }

        public void AddCategory(Category category)
        {
            var connection = DatabaseHelper.GetConnection();
            string query = "INSERT INTO Categories (Name, Description) VALUES (@Name, @Description)";

            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Name", category.Name);
                command.Parameters.AddWithValue("@Description", category.Description);
                command.ExecuteNonQuery();
            }
        }

        public void DeleteCategory(int categoryId)
        {
            var connection = DatabaseHelper.GetConnection();
            string query = "DELETE FROM Categories WHERE Id = @Id";

            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", categoryId);
                command.ExecuteNonQuery();
            }
        }
    }
}