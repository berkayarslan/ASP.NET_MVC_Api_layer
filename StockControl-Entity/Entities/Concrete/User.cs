using StockControl_Entity.Entities.Abstract;
using StockControl_Entity.Entities.Concrete;

namespace StockControl_Entity
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Adress { get; set; }
        public string Password { get; set; }
        public string? PhotoUrl { get; set; }
        public string Photo { get; set; }

        public UserRole UserRole { get; set; }

        public List<Order> Orders { get; set; }

        public User()
        {
            Orders = new List<Order>();
        }
    }
}