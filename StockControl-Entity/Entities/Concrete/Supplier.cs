using StockControl_Entity.Entities.Abstract;

namespace StockControl_Entity
{
    public class Supplier:BaseEntity
    {
        public string SupplierName { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        
        // navigation
        public List<Product> Products { get; set; }

        public Supplier()
        {
            Products = new List<Product>();
        }
    }
}