
using System.ComponentModel.DataAnnotations;

public class Product {
    public int ProductID { get; set; }
    [Required(ErrorMessage = "Product Name is required")]
    public string ProductName { get; set; }
    public decimal? UnitPrice { get; set; }
    public short? UnitsOnOrder { get; set; }
}
