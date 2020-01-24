using PizzaBox.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;


namespace PizzaBox.Domain.Models
{
    public class ManagerDetails : AbstractUser
    {

        public StoreDetails CurrentStore { get; set; }
        public ManagerDetails()
        {
          StoreDetails  CurrentStore =  new StoreDetails();
           // if(CurrentStore == null)
            
        }
        public override string ToString()
        {
            return $"{this.FirstName}";
        }
    }
}

