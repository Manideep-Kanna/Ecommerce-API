using Data_Access_Layer.Models;
using Data_Access_Layer.Views;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Cart_Item> Cart_Items { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        
        public DbSet<Comment> Comments { get; set; }


    }
}
