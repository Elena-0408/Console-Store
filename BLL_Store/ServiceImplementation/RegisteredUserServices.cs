using Store.Data;
using Store.Entities;
using Store.Service;
using System.Linq;
using System;
using System.Collections.Generic;
using Store.Validation;
using Store.GetUserData;

namespace Store.ServiceImplementation
{
    public class RegisteredUserServices : IRegisteredUserServices
    {
        private readonly IDataStore context;
        
        public RegisteredUserServices(IDataStore dataContext)
        {
            context = dataContext;
        }

        public bool SearchGoodsByName(string name)
        {
            try
            {
                var prod = context.Products.ToArray();
                var product = prod.FirstOrDefault(p => p.Name == name).ToString();
                PrintData.Print(product); 
                
                return true;
            }
            catch (NullReferenceException)
            {
                PrintData.Print("Product not found :(");
                throw new StoreException("Product not found :(");
            }
        }

        public bool GetListOfGoods()
        {
            var p = context.Products.ToList();
            if (!p.Any())
            {
                throw new StoreException("List is empty");
            }
            else
            {
                foreach (var product in p)
                {
                    PrintData.Print(product.ToString());
                }
                return true;
            }
        }
        public bool CreateNewOrder()
        {
            var customer = context.RegisteredUsers.First(c => c.IsLoginIn);
            PrintData.Print($"To complete the order press F1, press enter to add next product.");
            var goods = context.Products.ToArray();
            int i = goods.Length;
            while (Console.ReadKey().Key != ConsoleKey.F1)
            {
                i++;
                PrintData.Print("Enter the id of the product to be added to the order: ");
                int productId = Convert.ToInt32(Console.ReadLine());
                var product = goods.Where(p => p.Id == productId).ToList();
                while (product.Count == 0)
                {
                    PrintData.Print("There is no product with this ID in the store. Enter the id of the product to be added to the order: ");
                    productId = Convert.ToInt32(Console.ReadLine());
                }
                var newItem = new Order()
                {
                    Id = i,
                    Product = product,
                    Customer = customer.Name,
                    Status = StatusOfOrder.New,
                };

                PrintData.Print($"Product added to shopping cart!");
                
                context.Orders.Add(newItem);
            }
            return true;
        }

        public bool Cancellation(int id)
        {
            var order = context.Orders.FirstOrDefault(o => o.Id == id);
            if (order.Status != StatusOfOrder.Received)
            {
                order.Status = StatusOfOrder.CanceledByUser;
            }

            if (order.Status != StatusOfOrder.CanceledByUser)
            {
                return false;
            }
           
            PrintData.PrintWithChangeColor($"Status of order with ID {id} is {order.Status}.", ConsoleColor.Green);

            return true;
        }

        public bool SignOut()
        {
            var loginInUser = context.RegisteredUsers.First(u => u.IsLoginIn);
            loginInUser.IsLoginIn = false;
            return true;
        }

        public bool ChangeStatus(int id)
        {  
            StatusOfOrder status = StatusOfOrder.Received;
            var order = context.Orders.FirstOrDefault(o => o.Id == id);
            order.Status = status;
            if (status != StatusOfOrder.Received)
            {
                return false;
            }
            
            PrintData.PrintWithChangeColor($"Status of order with ID {id} is {status}.", ConsoleColor.Green);

            return true;
        }

        public bool GetOrdersHistory()
        {
            var customer = context.RegisteredUsers.First(c => c.IsLoginIn);
            var history = context.Orders.Where(or => or.Customer == customer.Name);

            if (history.ToArray().Length == 0)
            {
                PrintData.Print("You haven't ordered anything yet :(");
                return false;
            }
            else
            {
                PrintData.Print(customer.Name + ", your orders:");
                foreach (var order in history)
                {
                    for (int product = 0; product < order.Product.ToArray().Length; product++)
                    {
                        PrintData.PrintWithChangeColor("ID: " + order.Id + " " + order.Product[product].Name + " is " + order.Status.ToString(), ConsoleColor.Blue);
                    }
                }
            }
            return true;
        }

        public bool Basket()
        {
            var customer = context.RegisteredUsers.First(c => c.IsLoginIn);
            var history = context.Orders.Where(or => or.Customer == customer.Name);

            if (history.ToArray().Length == 0)
            {
                PrintData.Print("Basket is empty :(");
                return false;
            }
            else
            {
                var newOrder = context.Orders.Where(or => or.Status == StatusOfOrder.New && or.Customer == customer.Name);
                PrintData.Print("Items in the basket: ");
                foreach (var order in newOrder)
                {
                    for (int product = 0; product < order.Product.ToArray().Length; product++)
                    {
                        PrintData.PrintWithChangeColor("ID: " + order.Id + " " + order.Product[product].Name + " is " + order.Status.ToString(), ConsoleColor.Blue);
                    }
                }
                OrderConfirmation();
            }
               return true;
        }
        
        public bool OrderConfirmation()
        {
            var newOrder = context.Orders.Where(or => or.Status == StatusOfOrder.New);
            PrintData.Print("Do you want to pay for the order?");
            int action = 0;
            var answer = new Dictionary<int, string>()
            {
                [1] = "Yes :)",
                [2] = "No :(",

            };
            foreach (var item in answer)
            {
                PrintData.Print($"{item.Key} - {item.Value}");
            }
            try
            {
                action = Convert.ToInt32(Console.ReadLine());

            }
            catch (Exception)
            {
                throw new StoreException("It must be a number");
            }
            foreach (var d in answer.Keys.Where(d => d == action))
            {
                if (d <= 2)
                {
                    if (d == 1)
                    {
                        foreach (var order in newOrder)
                        {
                            order.Status = StatusOfOrder.PaymentReceived;
                        }
                        PrintData.PrintWithChangeColor("Payment successful!", ConsoleColor.Green);
                    }
                    else if (d == 2)
                    {
                        PrintData.PrintWithChangeColor("Goods will be waiting for you.", ConsoleColor.DarkYellow);
                        return false;
                    }
                }
            }
            return true;
        }

        public bool ChangePersonalInformation(string userEmail, string userPassword, string userPhone)
        {
            var customer = context.RegisteredUsers.First(c => c.IsLoginIn);
            
            var userData = context.RegisteredUsers.FirstOrDefault(u => u.Name == customer.Name);
            var changedUserData = new RegisteredUser
            {
                Id = userData.Id,
                Email = userEmail,
                Name = customer.Name,
                Password = userPassword,
                Phone = userPhone,
                IsLoginIn = true

            };
            context.RegisteredUsers.Remove(userData);
            context.RegisteredUsers.Add(changedUserData);
            PrintData.Print($"{customer.Name}, you have successfully changed the information!");
            return true;
        }

    }
}
