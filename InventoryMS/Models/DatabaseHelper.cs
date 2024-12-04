using System;
using System.Data;
using System.Data.SQLite;

namespace InventoryMS.Models
{
    public class DatabaseHelper
    {
        private static SQLiteConnection _connection;

        public static SQLiteConnection GetConnection()
        {
            if (_connection == null)
            {
                _connection = new SQLiteConnection("Data Source=C:\\Users\\latif\\Documents\\InventoryMS\\InventoryMS\\Inventory.db;Version=3;");
                _connection.Open();
            }
            return _connection;
        }
    }
}
