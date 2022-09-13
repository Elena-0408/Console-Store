using Store.Data;
using Store.Entities;
using Store.Service;
using System;
using System.Linq;
using Store.Validation;
using Store.GetUserData;


namespace Store.ServiceImplementation
{
    public class AdministratorServices : IAdministratorServices
    {
        private readonly IDataStore context;
        
        public AdministratorServices(IDataStore dataContext)
        {
            context = dataContext;
        }
               
        public bool SearchGoodsByName(string name)
        {
            var prod = context.Products.ToArray();
           
            try
            {
                PrintData.Print(prod.FirstOrDefault(p => p.Name == name).ToString()); 
                return true;
            }
            catch (NullReferenceException)
            {
                throw new StoreException("This product is not in the store!");
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
            var customer = context.Administrators.First(c => c.IsLoginIn);
            
            var goods = context.Products.ToArray();
            int i = goods.Length;
            int productId = 0;
            PrintData.Print("To complete the order press F1, press enter to add next product.");
            while (Console.ReadKey().Key != ConsoleKey.F1)
            {
                i++;
                productId = GetUserData.GetDataForAdmin.DataForCreateNewOrder();
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
            PrintData.Print(context.Orders[i-1].ToString());
            return true;
        }
        public bool SignOut()
        {
            var loginInUser = context.Administrators.First(u => u.IsLoginIn);
            loginInUser.IsLoginIn = false;
            return true;
        }

        public bool AddProduct(string name, string category, decimal price, string description)
        {
            var productId = context.Products.ToArray();
            var newProduct = new Goods
            {
                Id = productId.Length + 1,
                Name = name,
                Category = category,
                Price = price,
                Description = description
            };
            context.Products.Add(newProduct);
            PrintData.Print("You have successfully added a product :)");
            return true;
        }

        public bool ChangeProductInformation(int productId, string name, string category, decimal price, string description)
        {
            var prod = context.Products.ToArray();
            var product = prod.FirstOrDefault(p => p.Id == productId);
            var changedProduct = new Goods
            {
                Id = productId,
                Name = name,
                Category = category,
                Price = price,
                Description = description
            };
            context.Products.Remove(product);
            context.Products.Add(changedProduct);
            PrintData.Print($"Information of product with Id {productId} is changed!");
            return true;
        }


        public bool ChangeStatus(int id, StatusOfOrder status)
        {
            var order = context.Orders.FirstOrDefault(o => o.Id == id);
            order.Status = status;
            if (status == StatusOfOrder.New || status == StatusOfOrder.CanceledByAdministrator ||
                status == StatusOfOrder.PaymentReceived || status == StatusOfOrder.Sent
                || status == StatusOfOrder.Received || status == StatusOfOrder.Completed)
            {
                PrintData.PrintWithChangeColor($"Status of order with ID {id} is {status}.", ConsoleColor.Green);
                return true;
            }
            else
            {
                PrintData.PrintWithChangeColor($"Unable to set specified status! Now status of order with ID {id} is {status}.", ConsoleColor.Red);

                throw new StoreException("This status does not exist.");
            }

        }

        public bool GetInformationOfUser(int userId)
        {
            var userData = context.RegisteredUsers.FirstOrDefault(u => u.Id == userId);
            if (userData!=null)
            {
                PrintData.Print(userData.ToString());
                return true;
            }
            else
            {
                PrintData.Print("Users not found :(");
                return false;
            }
        }

        public bool ChangeInformationOfUser(int userId, string userName, string userEmail, string userPassword, string userPhone)
        {
            var userData = context.RegisteredUsers.FirstOrDefault(u => u.Id == userId);
            var changedUserData = new RegisteredUser
            {
                Id = userId,
                Email = userEmail,
                Name = userName,
                Password = userPassword,
                Phone = userPhone,
                IsLoginIn = false
            };

            context.RegisteredUsers.Remove(userData);
            context.RegisteredUsers.Add(changedUserData);

            return true;
        }
    }
}
