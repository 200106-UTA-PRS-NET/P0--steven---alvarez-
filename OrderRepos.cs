
using System.Collections.Generic;
using System.Linq;
using PizzaBox.Domain.Models;
using PizzaBox.Storing.Adapters;

namespace PizzaBox.Storing.Repositories
{ 
    public class OrderRepos

    {
        private static List<OrderDetails> _orderList;
        private const string _path = @"orders.xml";
        public OrderRepos()
        {
            if (_orderList == null)
            {
                try
                {
                    _orderList = FileAdapter.ReadFromXml<List<OrderDetails>>(_path);
                }
                catch
                {
                    _orderList = new List<OrderDetails>();
                    Save();
                }
            }
        }

        public void Create(OrderDetails order)
        {
            _orderList.Add(order);
            Save();
        }

        public List<OrderDetails> Read(OrderDetails order)
        {
            if (order == null)
            {
                return _orderList;
            }
            return _orderList.Where(O => O.Id == order.Id).ToList();
        }

        public void Update(OrderDetails order)
        {
            var orderItem = _orderList.FirstOrDefault(O => O.Id == order.Id);

            orderItem = order;
            Save();
        }

        public void Delete(OrderDetails order)
        {
            var orderItem = _orderList.FirstOrDefault(O => O.Id == order.Id);
            _orderList.Remove(orderItem);
            Save();
        }
        
        private void Save()
        {
            FileAdapter.WriteToXml<List<OrderDetails>>(_orderList, _path);
        }
    }
}

