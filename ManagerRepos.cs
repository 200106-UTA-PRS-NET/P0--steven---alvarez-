using System.Collections.Generic;
using System.Linq;
using PizzaBox.Domain.Models;
using PizzaBox.Storing.Adapters;



namespace PizzaBox.Storing.Repositories
{
    public class ManagerRepos

    {
        public List<ManagerDetails> _managerList;
        public ManagerRepos()
        {
            Initialize();
        }

        private void Initialize()
        {
            _managerList = new List<ManagerDetails>();
            _managerList.Add(CreateManager());
        }
        private ManagerDetails CreateManager()
        {
            ManagerDetails m = new ManagerDetails();
            m.FirstName = "Admin";
            m.Address = "Arlington, TX";
            m.Email = "admin@pizzabox.com";
            m.Password = "123";
            return m;
        }
    }
}
