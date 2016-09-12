
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Web.Configuration;

public class NorthwindDataProvider {
    public static IEnumerable GetProducts() {
        List<Product> products = new List<Product>();
            
        using (OleDbConnection connection = new OleDbConnection(WebConfigurationManager.ConnectionStrings["Northwind"].ConnectionString)) {
            OleDbCommand selectCommand = new OleDbCommand("SELECT * FROM Products ORDER BY ProductID", connection);

            connection.Open();

            OleDbDataReader reader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);

            while (reader.Read()) {
                products.Add(new Product() {
                    ProductID = (int)reader["ProductID"],
                    ProductName = (string)reader["ProductName"],
                    UnitPrice = (reader["UnitPrice"] == DBNull.Value ? null : (decimal?)reader["UnitPrice"]),
                    UnitsOnOrder = (reader["UnitsOnOrder"] == DBNull.Value ? null : (short?)reader["UnitsOnOrder"])
                });
            }

            reader.Close();
        }

        return products;
    }

    public static void InsertProduct(Product product) {
        using (OleDbConnection connection = new OleDbConnection(WebConfigurationManager.ConnectionStrings["Northwind"].ConnectionString)) {
            OleDbCommand insertCommand = new OleDbCommand("INSERT INTO [Products] ([ProductName], [UnitPrice], [UnitsOnOrder]) VALUES (?, ?, ?)", connection);

            insertCommand.Parameters.AddWithValue("ProductName", product.ProductName);
                
            if (product.UnitPrice.HasValue)
                insertCommand.Parameters.AddWithValue("UnitPrice", product.UnitPrice);
            else
                insertCommand.Parameters.AddWithValue("UnitPrice", DBNull.Value);
                
            if (product.UnitsOnOrder.HasValue)
                insertCommand.Parameters.AddWithValue("UnitsOnOrder", product.UnitsOnOrder);
            else
                insertCommand.Parameters.AddWithValue("UnitsOnOrder", DBNull.Value);

            connection.Open();
            insertCommand.ExecuteNonQuery();
        }
    }

    public static void UpdateProduct(Product product) {
        using (OleDbConnection connection = new OleDbConnection(WebConfigurationManager.ConnectionStrings["Northwind"].ConnectionString)) {
            OleDbCommand updateCommand = new OleDbCommand("UPDATE [Products] SET [ProductName] = ?, [UnitPrice] = ?, [UnitsOnOrder] = ? WHERE [ProductID] = ?", connection);

            updateCommand.Parameters.AddWithValue("ProductName", product.ProductName);
            updateCommand.Parameters.AddWithValue("UnitPrice", product.UnitPrice);
            updateCommand.Parameters.AddWithValue("UnitsOnOrder", product.UnitsOnOrder);
            updateCommand.Parameters.AddWithValue("ProductID", product.ProductID);

            connection.Open();
            updateCommand.ExecuteNonQuery();
        }
    }

    public static void DeleteProduct(int productID) {
        using (OleDbConnection connection = new OleDbConnection(WebConfigurationManager.ConnectionStrings["Northwind"].ConnectionString)) {
            OleDbCommand deleteCommand = new OleDbCommand("DELETE FROM Products WHERE ProductID = " + productID.ToString(), connection);

            connection.Open();
            deleteCommand.ExecuteNonQuery();
        }
    }
}
