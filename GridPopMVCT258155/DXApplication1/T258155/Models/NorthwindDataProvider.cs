
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Web.Configuration;
using System.Linq;
public class NorthwindDataProvider {
    public static List<Product> products;
    public NorthwindDataProvider()
    {
       
    }

    public static IEnumerable GetProducts() {
        if (products == null)
        {
            products = new System.Collections.Generic.List<Product>
            {
	            new Product
	            {
		            ProductID = 1,
		            ProductName = "Chai",
		            UnitPrice = 18,
		            UnitsOnOrder = 3123,
                    TestEnum=TestEnum.BBB
	            },
	            new Product
	            {
		            ProductID = 2,
		            ProductName = "Chang",
		            UnitPrice = 19,
		            UnitsOnOrder = 40
	            },
	            new Product
	            {
		            ProductID = 3,
		            ProductName = "Aniseed Syrup",
		            UnitPrice = 10,
		            UnitsOnOrder = 70
	            },
	            new Product
	            {
		            ProductID = 4,
		            ProductName = "Chef Anton's Cajun Seasoning",
		            UnitPrice = 22,
		            UnitsOnOrder = 0
	            },
	            new Product
	            {
		            ProductID = 5,
		            ProductName = "Chef Anton's Gumbo Mix",
		            UnitPrice = 2135,
		            UnitsOnOrder = 0
	            },
	            new Product
	            {
		            ProductID = 6,
		            ProductName = "Grandma's Boysenberry Spread",
		            UnitPrice = 25,
		            UnitsOnOrder = 0
	            },
	            new Product
	            {
		            ProductID = 7,
		            ProductName = "Uncle Bob's Organic Dried Pears",
		            UnitPrice = 30,
		            UnitsOnOrder = 0
	            },
	            new Product
	            {
		            ProductID = 8,
		            ProductName = "Northwoods Cranberry Sauce",
		            UnitPrice = 401,
		            UnitsOnOrder = 0
	            },
	            new Product
	            {
		            ProductID = 9,
		            ProductName = "Mishi Kobe Niku",
		            UnitPrice = 97,
		            UnitsOnOrder = 0
	            },
	            new Product
	            {
		            ProductID = 10,
		            ProductName = "Ikura",
		            UnitPrice = 31,
		            UnitsOnOrder = 0
	            },
	            new Product
	            {
		            ProductID = 11,
		            ProductName = "Queso Cabrales",
		            UnitPrice = 21,
		            UnitsOnOrder = 30
	            },
	            new Product
	            {
		            ProductID = 12,
		            ProductName = "Queso Manchego La Pastora",
		            UnitPrice = 38,
		            UnitsOnOrder = 0
	            },
	            new Product
	            {
		            ProductID = 13,
		            ProductName = "Konbu",
		            UnitPrice = 6,
		            UnitsOnOrder = 0
	            },
	            new Product
	            {
		            ProductID = 14,
		            ProductName = "Tofu",
		            UnitPrice = 2325,
		            UnitsOnOrder = 0
	            },
	            new Product
	            {
		            ProductID = 15,
		            ProductName = "Genen Shouyu",
		            UnitPrice = 155,
		            UnitsOnOrder = 0
	            },
	            new Product
	            {
		            ProductID = 16,
		            ProductName = "Pavlova",
		            UnitPrice = 1745,
		            UnitsOnOrder = 0
	            },
	            new Product
	            {
		            ProductID = 17,
		            ProductName = "Alice Mutton",
		            UnitPrice = 39,
		            UnitsOnOrder = 0
	            },
	            new Product
	            {
		            ProductID = 18,
		            ProductName = "Carnarvon Tigers",
		            UnitPrice = 625,
		            UnitsOnOrder = 0
	            },
	            new Product
	            {
		            ProductID = 19,
		            ProductName = "Teatime Chocolate Biscuits",
		            UnitPrice = 92,
		            UnitsOnOrder = 0
	            },
	            new Product
	            {
		            ProductID = 20,
		            ProductName = "Sir Rodney's Marmalade",
		            UnitPrice = 81,
		            UnitsOnOrder = 0
	            },
	            new Product
	            {
		            ProductID = 21,
		            ProductName = "Sir Rodney's Scones",
		            UnitPrice = 10,
		            UnitsOnOrder = 40
	            },
	            new Product
	            {
		            ProductID = 22,
		            ProductName = "Gustaf's Knäckebröd",
		            UnitPrice = 21,
		            UnitsOnOrder = 0
	            },
	            new Product
	            {
		            ProductID = 23,
		            ProductName = "Tunnbröd",
		            UnitPrice = 9,
		            UnitsOnOrder = 0
	            },
	            new Product
	            {
		            ProductID = 24,
		            ProductName = "Guaraná Fantástica",
		            UnitPrice = 45,
		            UnitsOnOrder = 0
	            },
	            new Product
	            {
		            ProductID = 25,
		            ProductName = "NuNuCa Nuß-Nougat-Creme",
		            UnitPrice = 14,
		            UnitsOnOrder = 0
	            },
	            new Product
	            {
		            ProductID = 26,
		            ProductName = "Gumbär Gummibärchen",
		            UnitPrice = 3123,
		            UnitsOnOrder = 0
	            },
	            new Product
	            {
		            ProductID = 27,
		            ProductName = "Schoggi Schokolade",
		            UnitPrice = 439,
		            UnitsOnOrder = 0
	            },
	            new Product
	            {
		            ProductID = 28,
		            ProductName = "Rössle Sauerkraut",
		            UnitPrice = 456,
		            UnitsOnOrder = 0
	            },
	            new Product
	            {
		            ProductID = 29,
		            ProductName = "Thüringer Rostbratwurst",
		            UnitPrice = 12379,
		            UnitsOnOrder = 0
	            },
	            new Product
	            {
		            ProductID = 30,
		            ProductName = "Nord-Ost Matjeshering",
		            UnitPrice = 2589,
		            UnitsOnOrder = 0
	            },
	            new Product
	            {
		            ProductID = 31,
		            ProductName = "Gorgonzola Telino",
		            UnitPrice = 125,
		            UnitsOnOrder = 70
	            },
	            new Product
	            {
		            ProductID = 32,
		            ProductName = "Mascarpone Fabioli",
		            UnitPrice = 32,
		            UnitsOnOrder = 40
	            },
	            new Product
	            {
		            ProductID = 33,
		            ProductName = "Geitost",
		            UnitPrice = 25,
		            UnitsOnOrder = 0
	            },
	            new Product
	            {
		            ProductID = 34,
		            ProductName = "Sasquatch Ale",
		            UnitPrice = 14,
		            UnitsOnOrder = 0
	            },
	            new Product
	            {
		            ProductID = 35,
		            ProductName = "Steeleye Stout",
		            UnitPrice = 18,
		            UnitsOnOrder = 0
	            },
	            new Product
	            {
		            ProductID = 36,
		            ProductName = "Inlagd Sill",
		            UnitPrice = 19,
		            UnitsOnOrder = 0
	            },
	            new Product
	            {
		            ProductID = 37,
		            ProductName = "Gravad lax",
		            UnitPrice = 26,
		            UnitsOnOrder = 50
	            },
	            new Product
	            {
		            ProductID = 38,
		            ProductName = "Côte de Blaye",
		            UnitPrice = 2635,
		            UnitsOnOrder = 0
	            },
	            new Product
	            {
		            ProductID = 39,
		            ProductName = "Chartreuse verte",
		            UnitPrice = 18,
		            UnitsOnOrder = 0
	            },
	            new Product
	            {
		            ProductID = 40,
		            ProductName = "Boston Crab Meat",
		            UnitPrice = 184,
		            UnitsOnOrder = 0
	            },
	            new Product
	            {
		            ProductID = 41,
		            ProductName = "Jack's New England Clam Chowder",
		            UnitPrice = 965,
		            UnitsOnOrder = 0
	            },
	            new Product
	            {
		            ProductID = 42,
		            ProductName = "Singaporean Hokkien Fried Mee",
		            UnitPrice = 14,
		            UnitsOnOrder = 0
	            },
	            new Product
	            {
		            ProductID = 43,
		            ProductName = "Ipoh Coffee",
		            UnitPrice = 46,
		            UnitsOnOrder = 10
	            },
	            new Product
	            {
		            ProductID = 44,
		            ProductName = "Gula Malacca",
		            UnitPrice = 1945,
		            UnitsOnOrder = 0
	            },
	            new Product
	            {
		            ProductID = 45,
		            ProductName = "Røgede sild",
		            UnitPrice = 95,
		            UnitsOnOrder = 70
	            },
	            new Product
	            {
		            ProductID = 46,
		            ProductName = "Spegesild",
		            UnitPrice = 12,
		            UnitsOnOrder = 0
	            },
	            new Product
	            {
		            ProductID = 47,
		            ProductName = "Zaanse koeken",
		            UnitPrice = 95,
		            UnitsOnOrder = 0
	            },
	            new Product
	            {
		            ProductID = 48,
		            ProductName = "Chocolade",
		            UnitPrice = 1275,
		            UnitsOnOrder = 70
	            },
	            new Product
	            {
		            ProductID = 49,
		            ProductName = "Maxilaku",
		            UnitPrice = 20,
		            UnitsOnOrder = 60
	            },
	            new Product
	            {
		            ProductID = 50,
		            ProductName = "Valkoinen suklaa",
		            UnitPrice = 1625,
		            UnitsOnOrder = 0
	            },
	            new Product
	            {
		            ProductID = 51,
		            ProductName = "Manjimup Dried Apples",
		            UnitPrice = 53,
		            UnitsOnOrder = 0
	            },
	            new Product
	            {
		            ProductID = 52,
		            ProductName = "Filo Mix",
		            UnitPrice = 7,
		            UnitsOnOrder = 0
	            },
	            new Product
	            {
		            ProductID = 53,
		            ProductName = "Perth Pasties",
		            UnitPrice = 328,
		            UnitsOnOrder = 0
	            },
	            new Product
	            {
		            ProductID = 54,
		            ProductName = "Tourtière",
		            UnitPrice = 745,
		            UnitsOnOrder = 0
	            },
	            new Product
	            {
		            ProductID = 55,
		            ProductName = "Pâté chinois",
		            UnitPrice = 24,
		            UnitsOnOrder = 0
	            },
	            new Product
	            {
		            ProductID = 56,
		            ProductName = "Gnocchi di nonna Alice",
		            UnitPrice = 38,
		            UnitsOnOrder = 10
	            },
	            new Product
	            {
		            ProductID = 57,
		            ProductName = "Ravioli Angelo",
		            UnitPrice = 195,
		            UnitsOnOrder = 0
	            },
	            new Product
	            {
		            ProductID = 58,
		            ProductName = "Escargots de Bourgogne",
		            UnitPrice = 1325,
		            UnitsOnOrder = 0
	            },
	            new Product
	            {
		            ProductID = 59,
		            ProductName = "Raclette Courdavault",
		            UnitPrice = 55,
		            UnitsOnOrder = 0
	            },
	            new Product
	            {
		            ProductID = 60,
		            ProductName = "Camembert Pierrot",
		            UnitPrice = 34,
		            UnitsOnOrder = 0
	            },
	            new Product
	            {
		            ProductID = 61,
		            ProductName = "Sirop d'érable",
		            UnitPrice = 285,
		            UnitsOnOrder = 0
	            },
	            new Product
	            {
		            ProductID = 62,
		            ProductName = "Tarte au sucre",
		            UnitPrice = 493,
		            UnitsOnOrder = 0
	            },
	            new Product
	            {
		            ProductID = 63,
		            ProductName = "Vegie-spread",
		            UnitPrice = 439,
		            UnitsOnOrder = 0
	            },
	            new Product
	            {
		            ProductID = 64,
		            ProductName = "Wimmers gute Semmelknödel",
		            UnitPrice = 3325,
		            UnitsOnOrder = 80
	            },
	            new Product
	            {
		            ProductID = 65,
		            ProductName = "Louisiana Fiery Hot Pepper Sauce",
		            UnitPrice = 2105,
		            UnitsOnOrder = 0
	            },
	            new Product
	            {
		            ProductID = 66,
		            ProductName = "Louisiana Hot Spiced Okra",
		            UnitPrice = 17,
		            UnitsOnOrder = 100
	            },
	            new Product
	            {
		            ProductID = 67,
		            ProductName = "Laughing Lumberjack Lager",
		            UnitPrice = 14,
		            UnitsOnOrder = 0
	            },
	            new Product
	            {
		            ProductID = 68,
		            ProductName = "Scottish Longbreads",
		            UnitPrice = 125,
		            UnitsOnOrder = 10
	            },
	            new Product
	            {
		            ProductID = 69,
		            ProductName = "Gudbrandsdalsost",
		            UnitPrice = 36,
		            UnitsOnOrder = 0
	            },
	            new Product
	            {
		            ProductID = 70,
		            ProductName = "Outback Lager",
		            UnitPrice = 15,
		            UnitsOnOrder = 10
	            },
	            new Product
	            {
		            ProductID = 71,
		            ProductName = "Fløtemysost",
		            UnitPrice = 215,
		            UnitsOnOrder = 0
	            },
	            new Product
	            {
		            ProductID = 72,
		            ProductName = "Mozzarella di Giovanni",
		            UnitPrice = 348,
		            UnitsOnOrder = 0
	            },
	            new Product
	            {
		            ProductID = 73,
		            ProductName = "Röd Kaviar",
		            UnitPrice = 15,
		            UnitsOnOrder = 0
	            },
	            new Product
	            {
		            ProductID = 74,
		            ProductName = "Longlife Tofu",
		            UnitPrice = 10,
		            UnitsOnOrder = 20
	            },
	            new Product
	            {
		            ProductID = 75,
		            ProductName = "Rhönbräu Klosterbier",
		            UnitPrice = 775,
		            UnitsOnOrder = 0
	            },
	            new Product
	            {
		            ProductID = 76,
		            ProductName = "Lakkalikööri1",
		            UnitPrice = 18,
		            UnitsOnOrder = 0
	            },
	            new Product
	            {
		            ProductID = 77,
		            ProductName = "Original Frankfurter grüne Soße",
		            UnitPrice = 13,
		            UnitsOnOrder = 0
	            },
	            new Product
	            {
		            ProductID = 80,
		            ProductName = "12312",
		            UnitPrice = 123,
		            UnitsOnOrder = 121
	            },
	            new Product
	            {
		            ProductID = 81,
		            ProductName = "aa",
		            UnitPrice = 1,
		            UnitsOnOrder = 1
	            }
            };
        }
        //List<Product> products = new List<Product>();
            
        //using (OleDbConnection connection = new OleDbConnection(WebConfigurationManager.ConnectionStrings["Northwind"].ConnectionString)) {
        //    OleDbCommand selectCommand = new OleDbCommand("SELECT * FROM Products ORDER BY ProductID", connection);

        //    connection.Open();

        //    OleDbDataReader reader = selectCommand.ExecuteReader(CommandBehavior.CloseConnection);

        //    while (reader.Read()) {
        //        products.Add(new Product() {
        //            ProductID = (int)reader["ProductID"],
        //            ProductName = (string)reader["ProductName"],
        //            UnitPrice = (reader["UnitPrice"] == DBNull.Value ? null : (decimal?)reader["UnitPrice"]),
        //            UnitsOnOrder = (reader["UnitsOnOrder"] == DBNull.Value ? null : (short?)reader["UnitsOnOrder"])
        //        });
        //    }

        //    reader.Close();
        //}

        return products;
    }

    public static void InsertProduct(Product product) {
        //using (OleDbConnection connection = new OleDbConnection(WebConfigurationManager.ConnectionStrings["Northwind"].ConnectionString)) {
        //    OleDbCommand insertCommand = new OleDbCommand("INSERT INTO [Products] ([ProductName], [UnitPrice], [UnitsOnOrder]) VALUES (?, ?, ?)", connection);

        //    insertCommand.Parameters.AddWithValue("ProductName", product.ProductName);
                
        //    if (product.UnitPrice.HasValue)
        //        insertCommand.Parameters.AddWithValue("UnitPrice", product.UnitPrice);
        //    else
        //        insertCommand.Parameters.AddWithValue("UnitPrice", DBNull.Value);
                
        //    if (product.UnitsOnOrder.HasValue)
        //        insertCommand.Parameters.AddWithValue("UnitsOnOrder", product.UnitsOnOrder);
        //    else
        //        insertCommand.Parameters.AddWithValue("UnitsOnOrder", DBNull.Value);

        //    connection.Open();
        //    insertCommand.ExecuteNonQuery();
        //}
        products.Add(product);
    }

    public static void UpdateProduct(Product product) {
        //using (OleDbConnection connection = new OleDbConnection(WebConfigurationManager.ConnectionStrings["Northwind"].ConnectionString)) {
        //    OleDbCommand updateCommand = new OleDbCommand("UPDATE [Products] SET [ProductName] = ?, [UnitPrice] = ?, [UnitsOnOrder] = ? WHERE [ProductID] = ?", connection);

        //    updateCommand.Parameters.AddWithValue("ProductName", product.ProductName);
        //    updateCommand.Parameters.AddWithValue("UnitPrice", product.UnitPrice);
        //    updateCommand.Parameters.AddWithValue("UnitsOnOrder", product.UnitsOnOrder);
        //    updateCommand.Parameters.AddWithValue("ProductID", product.ProductID);

        //    connection.Open();
        //    updateCommand.ExecuteNonQuery();
        //}
        var pd = products.Where(t => t.ProductID == product.ProductID).FirstOrDefault();
        products.Remove(pd);
        products.Add(product);
    }

    public static void DeleteProduct(int productID) {
        //using (OleDbConnection connection = new OleDbConnection(WebConfigurationManager.ConnectionStrings["Northwind"].ConnectionString)) {
        //    OleDbCommand deleteCommand = new OleDbCommand("DELETE FROM Products WHERE ProductID = " + productID.ToString(), connection);

        //    connection.Open();
        //    deleteCommand.ExecuteNonQuery();
        //}
        var pd = products.Where(t => t.ProductID == productID).FirstOrDefault();
        products.Remove(pd);
    }
}
