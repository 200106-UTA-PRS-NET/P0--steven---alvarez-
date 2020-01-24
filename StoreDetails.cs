using PizzaBox.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace PizzaBox.Domain.Models
{
    public class StoreDetails: AbstractModel
    {
        //public string Name { get; set; }
        public string Name { get; set; }
        public List<OrderDetails> CompletedOrders { get; set; }
        public string StoreLocation { get; set; }
        
        public StoreDetails()
        {
            
            CompletedOrders = new List<OrderDetails>();
        }
        public StoreDetails(string name, string location)
        {
            this.Name = name;

            this.StoreLocation = location;


            this.CompletedOrders = new List<OrderDetails>();
        }
        
        public void ViewStoreOrders()
        {
            if (CompletedOrders.Count == 0)
            {
                Console.WriteLine("No orders found");
                return;
            }
            Console.WriteLine("\nOrders for {0}", this.Name);
            foreach (var order in this.CompletedOrders)
            { 
                order.PrintOrder();
                Console.WriteLine();
            }
        }
        public void AddOrder(OrderDetails O)
        {
            this.CompletedOrders.Add(O);
        }
        public override string ToString()
        {
            return $"{this.Name}";
        }
    }
}
