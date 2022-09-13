using Store.Entities;
using System.Collections.Generic;


namespace Store.Data
{
   public class DataStore : IDataStore
    {
        public List<Administrator> Administrators { get; set; }
        public List<RegisteredUser> RegisteredUsers { get; set; }
        public List<Order> Orders { get; set; }
        public List<Goods> Products { get; set; }

        public DataStore()
        {
            Administrators = new List<Administrator>();
            RegisteredUsers = new List<RegisteredUser>();
            Orders = new List<Order>();
            Products = new List<Goods>();
            Initialize();
        }

        public void Initialize()
        {
            Administrator admin1 = new Administrator()
            {
                Id = 1,
                Name = "Stiven",
                Email = "stiv@gmail.com",
                Password = "admin",
                Phone = "380652312587"
            };
            Administrator admin2 = new Administrator()
            {
                Id = 1,
                Name = "Jake",
                Email = "Jake@gmail.com",
                Password = "admin00",
                Phone = "380852314796"
            };
            Administrators.Add(admin1);
            Administrators.Add(admin2);

            RegisteredUser user1 = new RegisteredUser()
            {
                Id = 1,
                Name = "Shannon Glenn",
                Email = "ShannonGl@gmail.com",
                Password = "1234",
                Phone = "38062547811"
                
            };
            RegisteredUser user2 = new RegisteredUser()
            {
                Id = 2,
                Name = "Clement Sullivan",
                Email = "Sullivan@gmail.com",
                Password = "qwerty",
                Phone = "380652897361"
                
            };
            RegisteredUser user3 = new RegisteredUser()
            {
                Id = 3,
                Name = "Aubrie Booth",
                Email = "AuBooth@gmail.com",
                Password = "mypass",
                Phone = "38097412354"
            };
            RegisteredUser user4 = new RegisteredUser()
            {
                Id = 4,
                Name = "Thomasine Briggs",
                Email = "ThBr@gmail.com",
                Password = "00000",
                Phone = "383682178525"
            };
            RegisteredUser user5 = new RegisteredUser()
            {
                Id = 5,
                Name = "Meghan Clarke",
                Email = "Meg@gmail.com",
                Password = "c777777",
                Phone = "380502879314"
            };
            
            RegisteredUsers.Add(user1);
            RegisteredUsers.Add(user2);
            RegisteredUsers.Add(user3);
            RegisteredUsers.Add(user4);
            RegisteredUsers.Add(user5);
           
            Goods product1 = new Goods()
            {
                Id = 1,
                Name = "Notebook HP 15s",
                Category = "Laptop",
                Price = 19999,
                Description = "Convenient to take with you. High performance for everyday tasks 11th Gen Intel Core processors offer the perfect combination of features to empower you.Full HD Display"
            };
            Goods product2 = new Goods()
            {
                Id = 2,
                Name = "Router ASUS RT-AC1200_V2",
                Category = "Router",
                Price = 1469,
                Description = "Dual Band Wi-Fi Router (AC1200) with Four External Antennas and Parental Controls"
            };
            Goods product3 = new Goods()
            {
                Id = 3,
                Name = "Headphones TWS Philips TAT2236 Black",
                Category = "Headphones",
                Price = 1599,
                Description = "IPX4 splash and sweat resistant. monophonic mode. Built-in microphone. Easy Pairing"
            };
            Goods product4 = new Goods()
            {
                Id = 4,
                Name = "TV LG 43UP75006LF",
                Category = "TV",
                Price = 14699,
                Description = "Screen resolution: 4K UHD 3840x2160. Purpose: For the living room, For the bedroom. Year of issue: 2021. Technology: AI Home, AI Recommendation, AI Sound, Apple Homekit, Quad Core 4K"
            };
            Goods product5 = new Goods()
            {
                Id = 5,
                Name = "Smartphone Samsung Galaxy S21 8/128 Phantom Pink",
                Category = "Smartphone",
                Price = 25299,
                Description = "Display (diagonal): 6.2. Display(max.resolution): 2400x1080. Built -in memory: 128 GB. RAM(capacity): 8 GB."
            };
            Products.Add(product1);
            Products.Add(product2);
            Products.Add(product3);
            Products.Add(product4);
            Products.Add(product5);
            
            Order order1 = new Order()
            {
                Id = 1,
                Customer = user4.Name,
                Product = new List<Goods> { product2},
                Status = StatusOfOrder.New
            };
            Order order2 = new Order()
            {
                Id = 2,
                Customer = user1.Name,
                Product = new List<Goods> { product2, product4, product5 },
                Status = StatusOfOrder.CanceledByAdministrator

            };
            Order order3 = new Order()
            {
                Id = 3,
                Customer = user3.Name,
                Product = new List<Goods> { product4 },
                Status = StatusOfOrder.Sent

            };
            Order order4 = new Order()
            {
                Id = 4,
                Customer = user5.Name,
                Product = new List<Goods> { product1, product2 },
                Status = StatusOfOrder.Received

            };
            var order5 = new Order()
            {
                Id = 5,
                Customer = user1.Name,
                Product = new List<Goods> { product3, product5 },
                Status = StatusOfOrder.Completed

            };
            Orders = new List<Order>()
            {
                order1,
                order2,
                order3, 
                order4,
                order5 
            };
        }
    }
}
