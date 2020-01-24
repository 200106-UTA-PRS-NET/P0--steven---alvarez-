using System;
using System.Collections.Generic;
using PizzaBox.Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace PizzaBox.Storing.Repositories
{
    public class UserRepos
    {

        public List<CustomerDetails> Customers { get; set; }
        public List<ManagerDetails> Managers { get; set; }
        public UserRepos()

        {
            Customers = new List<CustomerDetails>();
            Managers = new List<ManagerDetails>();
            Managers.Add(CreateManager());
            Customers.Add(CreateCustomer());
        }

        public void AddCustomer(CustomerDetails man)
        {
            Customers.Add(man);
        }

        public ManagerDetails CreateManager()

        {
            ManagerDetails man = new ManagerDetails();
            man.FirstName = "Admin";
            man.Address = "Arlington, TX";
            man.Email = "admin@test.com";
            man.Password = "7143";
            return man;
        }

        public CustomerDetails CreateCustomer()
        {
            CustomerDetails man = new CustomerDetails();

            man.FirstName = "Steven";
            man.LastName = "Alvarez";
            man.Address = "Arlington, TX";
            man.Email = "steven.alvarez.a@gmail.com";
            man.Password = "7143";
            return man;
        }

        public void ViewCustomers()
        {
            foreach (var customer in Customers)
            {
                Console.WriteLine(customer);
            }
        }
        public void ViewManagers()
        {
            foreach (var manager in Managers)
            {
                Console.WriteLine(manager);
            }
        }
    }
}

