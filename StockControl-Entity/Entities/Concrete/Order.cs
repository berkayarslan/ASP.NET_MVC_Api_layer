using StockControl_Entity.Entities.Abstract;
using StockControl_Entity.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace StockControl_Entity.Entities.Concrete
{
    public class Order : BaseEntity
    {
        [ForeignKey("User")]
        public int UserID { get; set; }
        public User User { get; set; }

        public Statu Statu { get; set; }

        //NAVIGATION

        public List<OrderDetails> OrderDetails { get; set; }

        public Order()
        {
            OrderDetails = new List<OrderDetails>();
        }
    }
}