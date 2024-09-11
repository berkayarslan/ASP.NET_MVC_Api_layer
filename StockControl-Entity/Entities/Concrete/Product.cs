using StockControl_Entity.Entities.Abstract;
using StockControl_Entity.Entities.Concrete;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockControl_Entity
{
    public class Product:BaseEntity
    {
        public string ProductName { get; set; }
        public double UnitPrice { get; set; }
        public int? Stock { get; set; }

        public DateTime? ExpireDate { get; set; }

        // NAVIGATION 
        [ForeignKey("Category")]
        public int CategoryID { get; set; }
        public Category? Category { get; set; }

        [ForeignKey("Supplier")]
        public int SupplierID { get; set; }
        public Supplier? Supplier { get; set; }

        public List<OrderDetails> OrderDetails { get; set; }

        public Product()
        {
            OrderDetails = new List<OrderDetails>();
        }
    }
}