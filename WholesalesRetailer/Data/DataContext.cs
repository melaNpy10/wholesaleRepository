using Microsoft.EntityFrameworkCore;
using WholesalesRetailer.Shared.Models;

namespace WholesalesRetailer.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Rebate> Rebates { get; set; }
        public DbSet<CashBack> CashBacks { get; set; }
    }
}
