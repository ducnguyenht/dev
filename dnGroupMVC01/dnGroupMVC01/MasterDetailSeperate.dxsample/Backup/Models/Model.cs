using System.Web;
using System.Data;
using System.Data.OleDb;
using System.Collections.Generic;

namespace CS.Model {
    public class MyModel {

        public static DataTable GetProducts() {
            DataTable dataTableProducts = new DataTable();
            using (OleDbConnection connection = GetConnection()) {
                OleDbDataAdapter adapter = new OleDbDataAdapter(string.Empty, connection);
                adapter.SelectCommand.CommandText = "SELECT * FROM [Products]";

                adapter.Fill(dataTableProducts);


            }
            return dataTableProducts;
        }

        public static DataTable GetCategories() {
            DataTable dataTableCategories = new DataTable();
            using (OleDbConnection connection = GetConnection()) {
                OleDbDataAdapter adapter = new OleDbDataAdapter(string.Empty, connection);
                adapter.SelectCommand.CommandText = "SELECT [CategoryID], [CategoryName], [Description] FROM [Categories]";

                adapter.Fill(dataTableCategories);
            }
            return dataTableCategories;
        }

        public static DataTable GetProductsByCategory(int? categoryID) {
            DataTable dataTableProducts = new DataTable();
            using (OleDbConnection connection = GetConnection()) {
                OleDbDataAdapter adapter = new OleDbDataAdapter(string.Empty, connection);
                adapter.SelectCommand.CommandText = string.Format("SELECT * FROM [Products] WHERE ([CategoryID] = {0})", categoryID);
                adapter.Fill(dataTableProducts);
            }
            return dataTableProducts;
        }

        static OleDbConnection GetConnection() {
            OleDbConnection connection = new OleDbConnection() { ConnectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}", HttpContext.Current.Server.MapPath("~/App_Data/nwind.mdb")) };
            return connection;
        }

    }

    public class MyViewModel {

        public DataTable Products { get; set; }
        public DataTable Categories { get; set; }
    }
}