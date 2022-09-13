using Store.Data;
using Store.Entities;
using Store.GetUserData;
using Store.Service;
using Store.Validation;
using System;
using System.Linq;
using System.Text;

namespace Store.ServiceImplementation
{
    
    public class GuestServices : IGuestServices
    {
        public delegate void PrintHandlerG(string message);
        
        private readonly IDataStore context;

        
        public GuestServices(IDataStore dataContext)
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

        public bool SignUp()
        {
            PrintData.Print("Enter your name: ");
            var name = Console.ReadLine();
            PrintData.Print("Enter your Email: ");
            var email = Console.ReadLine();
            if (!GetDataForUser.CheckEmail(email))
            {
                email = Console.ReadLine();
            }
            PrintData.Print("Enter your password: ");
            var password = Console.ReadLine();
            PrintData.Print("Enter your phone: ");
            var phone = Console.ReadLine();
           
            if (!GetDataForUser.CheckPhone(phone))
            {
                phone = Console.ReadLine();
            }
            var checkup = context.RegisteredUsers.FirstOrDefault(e => e.Email == email);

            if (checkup != null)
            {
                PrintData.Print("A user with this login already exists.");
            }
            else
            {
                var lastId = context.RegisteredUsers.ToArray();
                var newUser = new RegisteredUser
                {
                    Id = lastId.Length + 1,
                    Name = name,
                    Email = email,
                    Password = password,
                    Phone = phone,
                    IsLoginIn = false
                };
                context.RegisteredUsers.Add(newUser);
                
                PrintData.PrintWithChangeColor("Congratulations! You have successfully registered :)", ConsoleColor.Green);
            }
            return true;
        }

        

        public byte CheckLogin(string login, string password)
        {
            bool log = true;
           
            if (!GetDataForUser.CheckEmail(login))
            {
                login = Console.ReadLine();
                log = false;
            }
            if (log)
            {
                var admin = context.Administrators.FirstOrDefault(ad => ad.Email == login && ad.Password == password);
                var regUser = context.RegisteredUsers.FirstOrDefault(r => r.Email == login && r.Password == password);
                if (admin != null)
                {
                    admin.IsLoginIn = true;
                    return 1;
                }
                else if (regUser != null)
                {
                    regUser.IsLoginIn = true;
                    return 2;
                }
            }
            return 0;
        }

        public static string GetHiddenConsoleInput()
        {
            StringBuilder password = new StringBuilder();
            while (true)
            {
                ConsoleKeyInfo i = Console.ReadKey(true);

                if (i.Key == ConsoleKey.Enter)
                {
                    break;
                }
                else if (i.Key == ConsoleKey.Backspace)
                {
                    if (password.Length > 0)
                    {
                        password = password.Remove(password.Length - 1, 1);
                        PrintData.PrintLine("\b \b");
                    }
                }
                else
                {
                    password.Append(i.KeyChar);
                    PrintData.PrintLine("*");
                }
            }
            return password.ToString();
        }

        public Tuple<string, string, byte> SignIn()
        {
           var data =  GetDataForUser.DataForSignIn();
            byte check = CheckLogin(data.Item1, data.Item2);
            return Tuple.Create(data.Item1, data.Item2, check);
        }
    }
}
