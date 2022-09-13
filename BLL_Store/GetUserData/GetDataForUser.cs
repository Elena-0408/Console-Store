using Store.ServiceImplementation;
using System;
using System.Linq;


namespace Store.GetUserData
{
    public static class GetDataForUser
    {
        public static string DataForSearchGoodsByName()
        {
            Console.WriteLine("Enter the name of the product you are looking for: ");
            string name = Console.ReadLine();
            return name;
        }

        public static int DataForChangeStatus()
        {
            Console.WriteLine("Enter the id of the order: ");
            int id = Convert.ToInt32(Console.ReadLine());
            return id;
        }

        public static Tuple<string, string, string> DataForChangePersonalInformation()
        {
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

            if (!CheckPhone(userPhone))
            {
                userPhone = Console.ReadLine();
            }

            return Tuple.Create(userEmail, userPassword, userPhone);
        }

        public static Tuple<string, string> DataForSignIn()
        {
            Console.Write("Enter your login: ");
            string login = Console.ReadLine();

            Console.Write("Enter your password: ");
            string password = GuestServices.GetHiddenConsoleInput();
            return Tuple.Create(login, password);
        }

        public static bool CheckPhone(string phone)
        {
            bool IsDigit = phone.Length == phone.Count(c => char.IsDigit(c));
            if (!IsDigit)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                PrintData.Print("Phone number must contain only numbers. Please enter your phone again:");
                Console.ResetColor();
                return false;

            }

            return true;
        }

        public static bool CheckEmail(string email)
        {
            if (!email.Contains("@"))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                PrintData.Print("Email entered incorrectly! Please enter your email again:");
                Console.ResetColor();
                return false;
            }
            return true;
        }
    }
}
