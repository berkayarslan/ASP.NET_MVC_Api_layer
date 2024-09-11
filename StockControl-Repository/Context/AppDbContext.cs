using Microsoft.EntityFrameworkCore;
using StockControl_Entity;
using StockControl_Entity.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl_Repository.Context
{
    public class AppDbContext : DbContext
    {
        //program.cs de adddbcontext eklendikten sonra eklenmeli, aksi halde hherşeyi oluşturup en son migration başlatmaya kalktığında migration için 2 farklı desen diye hata verir

        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) { }
         

        public DbSet<Category> Categories   { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<User> Users { get; set; } 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-4810D6L\\SQLEXPRESS;Initial Catalog=API_Exercise_1;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
