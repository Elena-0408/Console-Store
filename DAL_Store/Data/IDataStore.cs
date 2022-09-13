using Store.Entities;
using System.Collections.Generic;


namespace Store.Data
{
   public interface IDataStore
    {
        public List<Administrator> Administrators { get; set; }
        public List<RegisteredUser> RegisteredUsers { get; set; }
        public List<Order> Orders { get; set; }
        public List<Goods> Products { get; set; }
    }
}
