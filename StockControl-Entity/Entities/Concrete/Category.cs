using StockControl_Entity.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl_Entity.Entities.Concrete
{
    public class Category : BaseEntity
    {
        public string CateGoryName { get; set; }
        public string Description { get; set; }
        

        //Navigation
        public List<Product> Products { get; set; }
        public Category() 
        { 
            Products = new List<Product>();
        }

    }
}
