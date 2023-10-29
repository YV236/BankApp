using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vinnyk_Tomkiv_Zaliczenie;
using Vinnyk_Tomkiv_Zaliczenie.Models;
using Vinnyk_Tomkiv_Zaliczenie.Services.CustomerManagments;

namespace Vinnyk_Tomkiv_Zaliczenie.Services.MenuOperation
{
    public class MenuManagment : IMenu
    {
        private readonly IUserManagement _customerManagement;

        public MenuManagment() 
        {
            _customerManagement = new UserManagment();
        }

        public void Menu()
        {
            User customer = new User();
            int choice;
            bool exit = true;

            while (exit)
            {
                Console.Clear();
                Console.WriteLine("Welcome to polyBank Application\nPlease enter your choice");
                Console.WriteLine("1.Create a new user");
                Console.WriteLine("2.Login as a user");
                Console.WriteLine("3.Remove user");
                Console.WriteLine("5.Exit Program");
                choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        RenderRegisterMenu();
                        break;

                    case 2:
                        Console.WriteLine("user successfully logined");
                        break;

                    case 3:
                        Console.WriteLine("user successfully removed");
                        break;

                    case 4:
                        break;

                    case 5:
                        exit = false;
                        break;

                    default:
                        Console.WriteLine("Error try another option 1-5.");
                        break;

                }


            }
        }

        private void RenderRegisterMenu()
        {
            Console.Clear();

            bool log = true;
            bool pas = true;

            string login = string.Empty;
            string password = string.Empty;

            while (log)
            {
                Console.Clear();
                Console.Write("Create your login please:");
                login = Console.ReadLine();
                if(login != string.Empty)
                {
                    if (!_customerManagement.IsUserExist(login))
                    {
                        log = false;
                    }
                }
            }

            while (pas)
            {
                Console.Clear();

                Console.Write("Create your password please:");
                password = Console.ReadLine();
                if(password != string.Empty)
                {
                    Console.Write("Repeat your password please:");
                    if (password == Console.ReadLine())
                    {
                        Console.WriteLine("Your account successfully created");
                        pas = false;
                    }
                }
                
            }

            User customer = new User { Login = login, Password = password };
            _customerManagement.AddUser(customer);

            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
    }
}
