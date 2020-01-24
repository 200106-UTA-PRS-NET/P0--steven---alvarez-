using PizzaBox.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;


namespace PizzaBox.Domain.Models
{
    public class OrderDetails : AbstractModel
    {
        public long CustId { get; set; }
        public List<PizzaDetails> Pizzas { get; set; }
       // public DateTime OrderDate { get; set; }
        public string StoreName { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal OrderTotal
        {
            get
            {
                decimal sum = 0;
                foreach (var pizza in this.Pizzas)
                {
                    sum += pizza.Price;
                }
                return sum;
            }
        }
        public OrderDetails()
        {
            Pizzas = new List<PizzaDetails>();
            OrderDate = DateTime.Now;
        }
        public OrderDetails(List<PizzaDetails> P)
        {
            this.Pizzas = P;
            OrderDate = DateTime.Now;
        }
        public void PrintOrder()
        {
            Console.WriteLine("Date: {0}", this.OrderDate);
            Console.WriteLine("Total: ${0}", this.OrderTotal);
            Console.WriteLine("Customer ID: {0}\nStore: {1}", this.CustId, this.StoreName);
            Console.WriteLine("Pizzas:");
            foreach (var P in Pizzas)
            {
                Console.WriteLine(P);
            }

        }
    }
}

