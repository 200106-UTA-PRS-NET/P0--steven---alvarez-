
using System.Collections.Generic;
using System.Linq;
using PizzaBox.Domain.Models;
using PizzaBox.Storing.Adapters;
using Microsoft.EntityFrameworkCore;



namespace PizzaBox.Storing.Repositories

{
    public class StoreRepos

    {
        public List<StoreDetails> _storeList;
        private const string _path = @"stores2.xml";
        public StoreRepos()
        {
            if (_storeList == null)
            {
                try
                {
                    _storeList = FileAdapter.ReadFromXml<List<StoreDetails>>(_path);
                }
                catch
                {
                    _storeList = new List<StoreDetails>();
                    
                    
                    Save();
                }
                
            }
        }
        public void Create(StoreDetails S)
            {
                _storeList.Add(S);
                Save();
            }


        public StoreDetails Get(StoreDetails S)
        {
            Create(S);
            /* 
             if ((S.Name == null) && (S.StoreLocation == null))
             {
                 return null;
             }
             return _storeList.Find( x => S.Name == S.Name && S.StoreLocation == S.StoreLocation);
             */

            
            if (S == null)
            {
                return null;
            }
            
            return _storeList.FirstOrDefault(c => { if (c == null||c.Name==null) { return false; } else { return c.Name.Equals( S.Name); } });

            
        }
        /*public List<StoreDetails> Read(StoreDetails S)
        {
            if (S == null)
            {
                return _storeList;
            }
            return _storeList.Find(x => x.Name == S.Name);

            //return _orderList.Where(O => O.Id == S.Id).ToList();
        }*/

        public void Update(StoreDetails Store)
        {
            var sItem = _storeList.FirstOrDefault(O => { if (O == null) { return false; } else { return O.Id == Store.Id ; }});

            sItem = Store;
            Save();
        }
        public void Delete(StoreDetails S)
        {
            var sItem = _storeList.FirstOrDefault(O => O.Id == S.Id);

            _storeList.Remove(sItem);
            Save();
        }

        public void Save()
        {
            FileAdapter.WriteToXml<List<StoreDetails>>(_storeList, _path);
        }
    }
}

