using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vinnyk_Tomkiv_Zaliczenie;
using Vinnyk_Tomkiv_Zaliczenie.Models;
using Vinnyk_Tomkiv_Zaliczenie.Services.BankAccManagment;
using Vinnyk_Tomkiv_Zaliczenie.Services.CustomerManagments;

namespace Vinnyk_Tomkiv_Zaliczenie.Services.MenuOperation
{
    public class MenuManagment : IMenu
    {
        private readonly IUserManagement _userManagement;

        private readonly IBankAccountManagment _bankAccountManagment;

        public MenuManagment() 
        {
            _userManagement = new UserManagment();
            _bankAccountManagment = new BankAccountManagment();
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
                        WriteUserLog();
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
                    if (!_userManagement.IsUserExist(login))
                    {
                        log = false;
                    }
                    else
                    {
                        Console.WriteLine("User with this login already exist");
                        Console.ReadKey();
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
            _userManagement.AddUser(customer);

            BankAccount bankAccount = _bankAccountManagment.BankAccReg();
            _bankAccountManagment.AddBankAcc(bankAccount, customer.Login);

            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        private void WriteUserLog()
        {
            string logLogin = string.Empty;
            string logPassword = string.Empty;

            bool log = true;
            bool pas = true;

            int i = 0;

            while (log)
            {
                Console.Clear();

                Console.Write("Write your login please:");
                logLogin = Console.ReadLine();

                if (logLogin != string.Empty)
                {
                    if(_userManagement.IsUserExist(logLogin))
                    {
                        Console.Write("Write your password:");
                        logPassword = Console.ReadLine();
                        if(_userManagement.IsPasswordRight(logLogin, logPassword) == true)
                        {
                            log = false;
                        }
                        else
                        {
                            Console.WriteLine("Wrong password");
                            Console.ReadKey();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Wrong Login");
                        Console.ReadKey();
                    }
                }
            }
            Console.WriteLine("You loginned to your account your welcome: " + logLogin);
            Console.ReadKey();
        }
    }
}
