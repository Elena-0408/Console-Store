using Store.Entities;
using Store.Validation;
using System;
using System.Collections.Generic;


namespace Store.GetUserData
{
   public static class GetDataForAdmin
    {
        public static int DataForCreateNewOrder()
        {
            Console.WriteLine($"Enter the id of the product to be added to the order:");
            try
            {
                int productId = Convert.ToInt32(Console.ReadLine());
                return productId;
            }
            catch (FormatException)
            {

                throw new StoreException("Id must be a number");
            }
        }

        public static string DataForSearchGoodsByName()
        {
            Console.WriteLine("Enter the name of the product you are looking for: ");
            string name = Console.ReadLine();
            return name;
        }

        public static Tuple<string, string, decimal, string> DataForAddProduct()
        {
            Console.Write("Enter name of product: ");
            string name = Console.ReadLine();
            Console.Write("Enter category of product: ");
            string category = Console.ReadLine();
            Console.Write("Enter price of product: ");
            decimal price = Convert.ToDecimal(Console.ReadLine());
            Console.Write("Enter description of product: ");
            string description = Console.ReadLine();
            
            return Tuple.Create(name, category, price, description);
        }

        public static int DataForGetInformationOfUser()
        {
            Console.WriteLine("Enter the id of user: ");
            try
            {
                int userId = Convert.ToInt32(Console.ReadLine());
                return userId;
            }
            catch (FormatException)
            {
                throw new StoreException("Id must be a number");
            }
        }
        
        public static Tuple<int, string, string , string, string> DataForChangeInformationOfUser()
        {
            Console.WriteLine("Enter the id of user: ");
            int userId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter name of user: ");
            string userName = Console.ReadLine();
            Console.WriteLine("Enter email of user: ");
            string userEmail = Console.ReadLine();
            if (!userEmail.Contains("@"))
            {
                Console.WriteLine("Email entered incorrectly! Please enter your email again");
                userEmail = Console.ReadLine();
            }
            Console.WriteLine("Enter password of user: ");
            string userPassword = Console.ReadLine();
            Console.WriteLine("Enter phone of user: ");
            string userPhone = Console.ReadLine();
            if (!GetDataForUser.CheckPhone(userPhone))
            {
                userPhone = Console.ReadLine();
            }

            return Tuple.Create(userId, userName, userEmail, userPassword, userPhone);
        }

        public static Tuple<int, string, string, decimal, string> DataForChangeProductInformation()
        {
            Console.Write("Enter the id of the product: ");
            
            string inputId = Console.ReadLine();
            bool isNumId = int.TryParse(inputId, out int productId);
            while (!isNumId)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Id must contain only numbers!");
                Console.ResetColor();
                inputId = Console.ReadLine();
                isNumId = int.TryParse(inputId, out productId);
                
            }
                Console.Write("Enter name of product: ");
                string name = Console.ReadLine();
                Console.Write("Enter category of product: ");
                string category = Console.ReadLine();
                Console.Write("Enter description of product: ");
                string description = Console.ReadLine();
                Console.Write("Enter price of product: ");
                string inputPrice = Console.ReadLine();
                bool isNumPrice = decimal.TryParse(inputPrice, out decimal price);
                if (!isNumPrice)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Price must contain only numbers!");
                    Console.ResetColor();
                    DataForChangeProductInformation();
                }
                
            return Tuple.Create(productId, name, category, price, description);
        }

        public static Tuple<int, StatusOfOrder> DataForCgangeStatus()
        {
            StatusOfOrder valueStatus = StatusOfOrder.New;
            Console.Write("Enter the id of the order: ");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the status of the order: ");
            
            var status = new Dictionary<int, string>()
            {
                [1] = "New",
                [2] = "CanceledByAdministrator",
                [3] = "PaymentReceived",
                [4] = "Sent",
                [5] = "Received",
                [6] = "Completed",
                [7] = "CanceledByUser"

            };

            foreach (var item in status)
            {
                PrintData.Print($"{item.Key} - {item.Value}");
            }
            int statusId = Convert.ToInt32(Console.ReadLine());
            foreach (var item in status)
            {
                if (statusId == item.Key)
                {
                    string value = item.Value;
                    valueStatus = (StatusOfOrder)Enum.Parse(typeof(StatusOfOrder), value);
                }
            }
            return Tuple.Create(id, valueStatus);
        }
    }
}
