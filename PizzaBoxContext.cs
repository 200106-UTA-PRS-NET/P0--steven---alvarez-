using Microsoft.EntityFrameworkCore;
using PizzaBox.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaBox.Domain
{
    class PizzaBoxContext : DbContext
    {
        public PizzaBoxContext(DbContextOptions<PizzaBoxContext> options) : base(options)
        {

        }
        public DbSet<CustomerDetails> Customers { get; set; }
        public DbSet<ManagerDetails> Managers { get; set; }
        public DbSet<OrderDetails> Orders { get; set; }
        public DbSet<PizzaDetails> Pizzas { get; set; }
        public DbSet<StoreDetails> Stores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-9K2Q9T1\SQLEXPRESS;Database=PizzaBoxDB;Trusted_Connection=True;");

        }
    }
}
  
