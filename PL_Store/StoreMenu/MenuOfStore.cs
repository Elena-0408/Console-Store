using Store.Data;
using Store.ServiceImplementation;
using Store.Entities;
using Store.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using Store.GetUserData;

namespace Store.StoreMenu
{

    public class MenuOfStore : IMenuOfStore
    {
        public delegate bool Delegate1();
        public delegate bool Delegate2<in T>(T arg);
        public delegate bool Delegate3<in T, in T1>(T arg, T1 arg1);
        public delegate bool Delegate4<in T, in T1, in T2>(T arg, T1 arg1, T2 arg2);
        public delegate bool Delegate5<in T, in T1, in T2, in T3>(T arg, T1 arg1, T2 arg2, T3 arg3);
        public delegate bool Delegate6<in T, in T1, in T2, in T3, in T4>(T arg, T1 arg1, T2 arg2, T3 arg3, T4 arg4);

        private readonly IDataStore context;

        private readonly AdministratorServices administratorS;
        private readonly RegisteredUserServices registeredUserS;
        private readonly GuestServices guestS;


        public MenuOfStore()
        {
            context = new DataStore();

            administratorS = new AdministratorServices(context);
            registeredUserS = new RegisteredUserServices(context);
            guestS = new GuestServices(context);
        }

        public bool RoleDefinition()
        {
            var logIn = guestS.SignIn();

            var currentUserA = context.Administrators.FirstOrDefault(u => u.IsLoginIn);
            var currentUserR = context.RegisteredUsers.FirstOrDefault(u => u.IsLoginIn);
            if (logIn.Item3 == 2)
            {
                PrintData.PrintClear();
                PrintData.Print($"Hello, {currentUserR.Name}");

                RolesActions("RegisteredUser");
                return true;
            }

            else if (logIn.Item3 == 1)
            {
                PrintData.PrintClear();
                PrintData.Print($"Hello, {currentUserA.Name}");

                RolesActions("Administrator");
                return true;
            }
            else
            {
                PrintData.PrintWithChangeColor("\nA user with this login and password does not exist. Enter correct data or register.", ConsoleColor.Red);
                RoleDefinition();
            }
            return false;
        }


        public void Start()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            PrintData.Print($"Hello! Select an action:");
            var startMenu = new Dictionary<int, string>()
            {
                [1] = "Sign In",
                [2] = "Register",
                [3] = "Stay guest"
            };
            foreach (var item in startMenu)
            {
                PrintData.Print($"{item.Key} - {item.Value}");
            }

            PrintData.Print(new string('=', 60));
            Console.ResetColor();

            string input = Console.ReadLine();
            bool isNum = byte.TryParse(input, out byte num);
            if (!isNum && num <= 3)
            {
                PrintData.Print("Enter the number that corresponds to the selected action");
                Start();
            }
            else
            {
                byte actionId = Convert.ToByte(input);
                switch (actionId)
                {
                    case 1:
                        PrintData.PrintClear();
                        RoleDefinition();

                        break;
                    case 2:
                        PrintData.PrintClear();
                        guestS.SignUp();
                        RoleDefinition();

                        break;
                    case 3:
                        PrintData.PrintClear();
                        RolesActions("Guest");
                        break;
                    default:

                        Console.ForegroundColor = ConsoleColor.Red;
                        PrintData.Print("The specified action is not available!");
                        Console.ResetColor();
                        Start();
                        break;
                }
            }
        }

        public void GuestActions(string action)
        {

            Dictionary<int, Delegate> guestDict = new Dictionary<int, Delegate>
            {
                { 1, new Delegate1(guestS.GetListOfGoods) },
                { 2, new Delegate2<string>(guestS.SearchGoodsByName) },
                { 3, new Delegate1(guestS.SignUp) },
                { 4, new Delegate1(RoleDefinition) }
            };

            foreach (var d in guestDict.Keys.Where(d => d.ToString() == action))
            {
                if (d <= 4)
                {
                    if (d == 2)
                    {
                        guestDict[d].DynamicInvoke(GetDataForUser.DataForSearchGoodsByName());
                        RolesActions("Guest");
                    }
                    else
                    {
                        guestDict[d].DynamicInvoke();
                        RolesActions("Guest");
                    }
                }
                else
                {
                    PrintData.PrintWithChangeColor("The specified action is not available!", ConsoleColor.Red);
                }

            }
        }

        public void RegisteredUserActions(string action)
        {

            Dictionary<int, Delegate> userDict = new Dictionary<int, Delegate>
            {
                { 1, new Delegate1(registeredUserS.GetListOfGoods) },
                { 2, new Delegate2<string>(registeredUserS.SearchGoodsByName) },
                { 3, new Delegate1(registeredUserS.CreateNewOrder) },
                { 4, new Delegate2<int>(registeredUserS.Cancellation) },
                { 5, new Delegate1(registeredUserS.GetOrdersHistory) },
                { 6, new Delegate2<int>(registeredUserS.ChangeStatus) },
                { 7, new Delegate1(registeredUserS.Basket) },
                { 8, new Delegate4<string, string,string>(registeredUserS.ChangePersonalInformation) },
                { 9, new Delegate1(registeredUserS.SignOut) }
            };


            foreach (var d in userDict.Keys.Where(d => d.ToString() == action))
            {
                if (d <= 8)
                {
                    if (d == 2)
                    {
                        userDict[d].DynamicInvoke(GetDataForUser.DataForSearchGoodsByName());
                        RolesActions("RegisteredUser");
                    }
                    else if (d == 4 || d == 6)
                    {
                        userDict[d].DynamicInvoke(GetDataForUser.DataForChangeStatus());
                        RolesActions("RegisteredUser");
                    }
                    else if (d == 7)
                    {
                        userDict[d].DynamicInvoke();
                        RolesActions("RegisteredUser");
                    }
                    else if (d == 8)
                    {
                        var dataOfUser = GetDataForUser.DataForChangePersonalInformation();
                        userDict[d].DynamicInvoke(dataOfUser.Item1, dataOfUser.Item2, dataOfUser.Item3);
                        RolesActions("RegisteredUser");
                    }
                    else if (d.ToString() == "9")
                    {
                        userDict[d].DynamicInvoke();
                        RolesActions("Guest");
                    }
                    userDict[d].DynamicInvoke();
                    RolesActions("RegisteredUser");
                }
                else
                {
                    PrintData.PrintWithChangeColor("The specified action is not available!", ConsoleColor.Red);
                    RolesActions("RegisteredUser");
                }
            }
        }

        public void AdministratorActions(string action)
        {
            Dictionary<int, Delegate> adminDict = new Dictionary<int, Delegate>
            {
                { 1, new Delegate1(administratorS.GetListOfGoods) },
                { 2, new Delegate2<string>(administratorS.SearchGoodsByName) },
                { 3, new Delegate1(administratorS.CreateNewOrder) },
                { 4, new Delegate2<int>(administratorS.GetInformationOfUser) },
                { 5, new Delegate6<int, string, string, string, string>(administratorS.ChangeInformationOfUser) },
                { 6, new Delegate5<string, string, decimal, string>(administratorS.AddProduct) },
                { 7, new Delegate6<int, string, string, decimal, string>(administratorS.ChangeProductInformation) },
                { 8, new Delegate3<int, StatusOfOrder>(administratorS.ChangeStatus) },
                { 9, new Delegate1(administratorS.SignOut) }
            };


            foreach (var d in adminDict.Keys.Where(d => d.ToString() == action))
            {
                if (d <= 8)
                {
                    if (d == 2)
                    {
                        adminDict[d].DynamicInvoke(GetDataForAdmin.DataForSearchGoodsByName());
                        RolesActions("Administrator");
                    }
                    else if (d == 4)
                    {
                        adminDict[d].DynamicInvoke(GetDataForAdmin.DataForGetInformationOfUser());
                        RolesActions("Administrator");
                    }
                    else if (d == 5)
                    {
                        var dataOfUser = GetDataForAdmin.DataForChangeInformationOfUser();
                        adminDict[d].DynamicInvoke(dataOfUser.Item1, dataOfUser.Item2, dataOfUser.Item3, dataOfUser.Item4, dataOfUser.Item5);
                        RolesActions("Administrator");
                    }
                    else if (d == 6)
                    {
                        var productData = GetDataForAdmin.DataForAddProduct();
                        adminDict[d].DynamicInvoke(productData.Item1, productData.Item2, productData.Item3, productData.Item4);
                        RolesActions("Administrator");
                    }
                    else if (d == 7)
                    {
                        var changeData = GetDataForAdmin.DataForChangeProductInformation();

                        adminDict[d].DynamicInvoke(changeData.Item1, changeData.Item2, changeData.Item3, changeData.Item4, changeData.Item5);
                        RolesActions("Administrator");
                    }
                    else if (d.ToString() == "8")
                    {
                        var statusData = GetDataForAdmin.DataForCgangeStatus();
                        adminDict[d].DynamicInvoke(statusData.Item1, statusData.Item2);
                        RolesActions("Guest");
                    }
                    else if (d.ToString() == "9")
                    {
                        adminDict[d].DynamicInvoke();
                        RolesActions("Guest");
                    }
                    adminDict[d].DynamicInvoke();
                    RolesActions("Administrator");
                }
                else
                {
                    PrintData.PrintWithChangeColor("The specified action is not available!", ConsoleColor.Red);
                    RolesActions("RegisteredUser");
                }
            }
        }

        public void RolesActions(string role)
        {
            switch (role)
            {
                case "Administrator":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    PrintData.Print(new string('=', 60));

                    var Administrator = new Dictionary<int, string>()
                    {
                        [1] = "View the list of goods",
                        [2] = "Search for a product by name",
                        [3] = "Create a new order",
                        [4] = "View personal information of users",
                        [5] = "Change personal information of users",
                        [6] = "Add a new product",
                        [7] = "Change of information about the product",
                        [8] = "Change the status of the order",
                        [9] = "Sign out of the account"
                    };
                    foreach (var item in Administrator)
                    {
                        PrintData.Print($"{item.Key}  - {item.Value}");
                    }

                    PrintData.Print(new string('=', 60));
                    Console.ResetColor();

                    PrintData.PrintLine("Enter the number corresponding to the selected action: ");
                    int action = Convert.ToInt32(Console.ReadLine());
                    PrintData.PrintClear();
                    AdministratorActions(action.ToString());

                    break;

                case "RegisteredUser":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    PrintData.Print(new string('=', 60));

                    var RegisteredUser = new Dictionary<int, string>()
                    {
                        [1] = "View the list of goods",
                        [2] = "Search for a product by name",
                        [3] = "Create a new order",
                        [4] = "Cancellation",
                        [5] = "Review the history of orders and the status of their delivery",
                        [6] = "Setting the status of the order 'Received'",
                        [7] = "Basket",
                        [8] = "Change of personal information",
                        [9] = "Sign out of the account"
                    };
                    foreach (var item in RegisteredUser)
                    {
                        PrintData.Print($"{item.Key} - {item.Value}");
                    }
                    PrintData.Print(new string('=', 60));
                    Console.ResetColor();

                    PrintData.PrintLine("Enter the number corresponding to the selected action: ");
                    action = Convert.ToInt32(Console.ReadLine());
                    PrintData.PrintClear();
                    RegisteredUserActions(action.ToString());

                    break;

                case "Guest":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    PrintData.Print(new string('=', 60));

                    var Guest = new Dictionary<int, string>()
                    {
                        [1] = "View the list of goods",
                        [2] = "Search for a product by name",
                        [3] = "Sign Up",
                        [4] = "Sign In"
                    };
                    foreach (var item in Guest)
                    {
                        PrintData.Print($"{item.Key} - {item.Value}");
                    }
                    PrintData.Print(new string('=', 60));
                    Console.ResetColor();

                    PrintData.Print("Enter the number corresponding to the selected action: ");
                    try
                    {
                        action = Convert.ToInt32(Console.ReadLine());
                        GuestActions(action.ToString());
                    }
                    catch (Exception)
                    {
                        throw new StoreException("It must be a number");
                    }
                    break;
                default:
                    PrintData.PrintWithChangeColor("The specified action is not available!", ConsoleColor.Red);
                    break;
            }
        }
    }
}
