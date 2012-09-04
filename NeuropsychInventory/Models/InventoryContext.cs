using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace NeuropsychInventory.Models
{
    public class InventoryContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Explicitly definte composite key for OrderItem join table
            modelBuilder.Entity<OrderItem>().HasKey(p => new { p.ProductId, p.OrderId });
            modelBuilder.Entity<InventoryItem>().HasKey(p => new { p.ProductId, p.InventoryId });
        }

    }
}