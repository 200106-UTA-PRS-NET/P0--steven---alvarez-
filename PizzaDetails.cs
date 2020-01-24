using PizzaBox.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace PizzaBox.Domain.Models
{
    public class PizzaDetails : AbstractModel
    {
        public string Crust { get; set; }
        public string Size { get; set; }
        public decimal Price
        {
            get
            {
                if (this.Size == "Small")
                    return 4.99M;
                else if (this.Size == "Medium")
                    return 7.99M;
                else if (this.Size == "Large")
                    return 9.99M;
                else
                    return 250.00M;
            }
        }
        public string Topping { get; set; }
        public PizzaDetails()
        {
        }
        public PizzaDetails(string s, string c, string t)
        {
            this.Size = s;
            this.Crust = c;
            this.Topping = t;
        }
        public override string ToString()
        {
            return $"{this.Size} pizza with {this.Crust} crust and {this.Topping}";
            
        }
        
    }
}

