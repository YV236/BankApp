using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vinnyk_Tomkiv_Zaliczenie;
using Vinnyk_Tomkiv_Zaliczenie.Models;
using Vinnyk_Tomkiv_Zaliczenie.Services.BankAccManagement;
using Vinnyk_Tomkiv_Zaliczenie.Services.CustomerManagements;
using Vinnyk_Tomkiv_Zaliczenie.Services.OptionOperations;

namespace Vinnyk_Tomkiv_Zaliczenie.Services.MenuOperation
{
    public class MenuManagement : IMenu
    {
        private readonly IUserManagement _userManagement;

        private readonly IBankAccountManagement _bankAccountManagement;

        public MenuManagement() 
        {
            _userManagement = new UserManagement();
            _bankAccountManagement = new BankAccountManagement();
        }

        public void Menu()
        {
            int choice;
            bool exit = true;

            while (exit)
            {
                Console.Clear();
                Console.WriteLine("Welcome to PolyBank Application\nPlease enter your choice\n");
                Console.WriteLine("1.Create a new user");
                Console.WriteLine("2.Log in to user");
                Console.WriteLine("3.Exit Program");
                choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        RenderRegisterMenu();
                        break;

                    case 2:
                        WriteUserLogin();
                        break;

                    case 3:
                        exit = false;
                        break;

                    default:
                        Console.WriteLine("Error try another option 1-3.");
                        break;
                }
            }
        }

        public void RenderRegisterMenu()
        {
            Console.Clear();

            bool log = true;
            bool pas = true;

            string login = string.Empty;
            string password = string.Empty;

            while (log)
            {
                Console.Clear();
                Console.Write("Create your login please: ");
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

                Console.Write("Create your password please: ");
                password = Console.ReadLine();
                if(password != string.Empty)
                {
                    Console.Write("Repeat your password please: ");
                    if (password == Console.ReadLine())
                    {
                        Console.WriteLine("Your account successfully created");
                        pas = false;
                    }
                }
                
            }

            User customer = new User { Login = login, Password = password };
            _userManagement.AddUser(customer);

            //BankAccount bankAccount = _bankAccountManagement.BankAccReg();
            //_bankAccountManagement.AddToUserBankAccList(bankAccount, customer.Login);
            BankAccount bankAccount = _bankAccountManagement.BankAccReg();
            _bankAccountManagement.AddBankAcc(bankAccount, customer.Login);

            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        public void WriteUserLogin()
        {
            string logLogin = string.Empty;
            string logPassword = string.Empty;
            int index = 0;

            bool log = true;

            int i = 0;

            while (log)
            {
                Console.Clear();

                Console.Write("Write your login please: ");
                logLogin = Console.ReadLine();

                if (logLogin != string.Empty)
                {
                    if(_userManagement.IsUserExist(logLogin))
                    {
                        Console.Write("Write your password: ");
                        logPassword = Console.ReadLine();

                        if (_userManagement.IsPasswordRight(logLogin, logPassword) == true)
                        {
                            log = false;
                        }
                        else
                        {
                            Console.WriteLine("You wrote the wrong password please try again");
                            ++i;
                            Console.ReadKey();
                        }
                    }
                    else
                    {
                        Console.WriteLine("You wrote the wrong login please try again");
                        ++i;
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine("You wrote the wrong login please try again");
                    ++i;
                }

                if (i >= 6)
                {
                    log = false;
                }
            }
            if(i < 6)
            {
                UserLoginedMenu(logLogin, index);
            }
            else
            {
                GivePropose();
            }
        }

        private void ShowUserInfo(string login, int accNum)
        {
            Console.Clear();

            User user = _userManagement.GetUserInfo(login);
            BankAccount account = _bankAccountManagement.GetBankAccInfo(login, accNum);

            Console.WriteLine("Bank account info");
            Console.WriteLine($"Login: {user.Login}");
            Console.WriteLine($"Your bank account number: {account.AccountNumber}");
            Console.WriteLine($"Balance: {account.Balance}");
            Console.ReadKey();
        }

        public void UserLoginedMenu(string login, int index)
        {
            int choice;
            bool exit = true;

            User user = _userManagement.GetUserInfo(login);
            Settings settings = new Settings();

            BankAccount bankAccount = new BankAccount();
            bankAccount.AccountNumber = user.Accounts[index].AccountNumber;

            BasicAccountOperations accountOperations = new BasicAccountOperations();

            while (exit)
            {
                Console.Clear();

                Console.WriteLine("You loginned successfully, welcome: " + login + "\n");
                Console.WriteLine("Choose the option 1-3");
                Console.WriteLine("1.Show bank account details");
                Console.WriteLine("2.Options");
                Console.WriteLine("3.Operations");
                Console.WriteLine("4.Exit to main menu");

                choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        ShowUserInfo(login, int.Parse(bankAccount.AccountNumber));
                            break;

                    case 2:
                        settings.SettingsMenu(login, int.Parse(bankAccount.AccountNumber));
                        exit = false;
                        break;

                    case 3:
                        accountOperations.OperationsMenu(login, int.Parse(bankAccount.AccountNumber));
                        break;

                    case 4:
                        exit = false;
                        break;

                    default:
                        Console.WriteLine("Choose the option 1-3");
                        break;

                }
            }
        }

       

        private void GivePropose()
        {
            int choice = 0;
            bool exit = true;

            while (exit)
            {
                Console.Clear();

                Console.WriteLine("If you don't remember your login, or password.");
                Console.WriteLine("1.Try again");
                Console.WriteLine("2.Create a new user");
                Console.WriteLine("3.Exit to main menu");

                choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        WriteUserLogin();
                        exit = false;
                        break;

                    case 2:
                        RenderRegisterMenu();
                        exit = false;
                        break;

                    case 3:
                        exit = false;
                        break;
                    default:
                        Console.WriteLine("Choose the option 1-3");
                        break;
                }
            }
        }
    }
}
