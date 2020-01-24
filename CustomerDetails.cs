using PizzaBox.Domain.Abstracts;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


namespace PizzaBox.Domain.Models
{
    public class CustomerDetails : AbstractUser
    {
        public List<OrderDetails> Orders { get; }
        public CustomerDetails()
        {
            this.Orders = new List<OrderDetails>();
        }
        public void AddOrder(OrderDetails O)
        {
            Orders.Add(O);
        }

        public OrderDetails GetLastOrder()
        {
            if (Orders.Count == 0)
                return null;
            else
                return this.Orders[Orders.Count - 1];
        }

        public void ViewOrderHistory()
        {
            if (Orders.Count == 0)
            {
                Console.WriteLine("No orders found");
                return;
            }
            Console.WriteLine("\nOrders for {0}:", this.FirstName);
            foreach (var O in Orders)
            {

                O.PrintOrder();
                Console.WriteLine();
            }
        }
        public override string ToString()
        {
            return $"{this.FirstName}";

        }
    }
    
}

