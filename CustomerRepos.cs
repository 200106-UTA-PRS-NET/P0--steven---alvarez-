using System.Collections.Generic;
using System.Linq;
using PizzaBox.Storing.Adapters;
using PizzaBox.Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace PizzaBox.Storing.Repositories
{
    public class CustomerRepos
    {
        private static List<CustomerDetails> _customerList;

        private const string _path = @"customers.xml";
        public CustomerRepos()
        {
            if (_customerList
                == null)
            {
                try
                { 
                    _customerList = FileAdapter.ReadFromXml<List<CustomerDetails>>(_path);
                }
                catch
                { 
                    _customerList = new List<CustomerDetails>();
                    Save();
                }
            }
        }
        public void Create(CustomerDetails customer)
        {
            _customerList.Add(customer);
            Save();
        }
        public CustomerDetails Read(CustomerDetails customer)
        {
            if (customer == null)
            {
                return null;
            }
            return _customerList.Find(x => x.Email == customer.Email && x.Password == customer.Password);
            // return _customerList.Where(x => x.FirstName== customer.FirstName).ToList();
        }
        public void Update(CustomerDetails customer)
        {
            var customerItem = _customerList.FirstOrDefault(O => O.Id == customer.Id);
            customerItem = customer;
            Save();
        }
        public void Delete(CustomerDetails customer)
        {
            var cItem = _customerList.FirstOrDefault(O => O.Id == customer.Id);
            _customerList.Remove(cItem);
            Save();
        }



        private void Save ()

        {

            FileAdapter.WriteToXml<List<CustomerDetails>>(_customerList, _path);
            
            
        }


    }
}
