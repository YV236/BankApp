using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vinnyk_Tomkiv_Zaliczenie;
using Vinnyk_Tomkiv_Zaliczenie.Models;
using Vinnyk_Tomkiv_Zaliczenie.Services.AccountOperations;
using Vinnyk_Tomkiv_Zaliczenie.Services.BankAccManagement;
using Vinnyk_Tomkiv_Zaliczenie.Services.CustomerManagements;
using Vinnyk_Tomkiv_Zaliczenie.Services.OptionOperations;
using Vinnyk_Tomkiv_Zaliczenie.Services.SettingsOperations;

namespace Vinnyk_Tomkiv_Zaliczenie.Services.MenuOperation
{
    /// <MenuScreen_Class>
    /// 
    /// A class for highlighting the main menus, the registration menu and the account login menu
    /// 
    /// </MenuScreen_Class>
    public class MenuScreen
    {
        //A private field _userManagement is declared, which represents an object that implements the IUserManagement interface
        private readonly IUserManagement _userManagement;
        //A private field _bankAccountManagement is declared, which represents an object that implements the IBankAccountManagement interface.
        private readonly IBankAccountManagement _bankAccountManagement;
        //Declares a protected Storage field that represents an object of the Storage class. This field can be used in derived classes.
        protected readonly Storage Storage;
        //A MaxAttempts constant with a value of 6 is declared to represent the maximum number of attempts during the registration process.
        private const int MaxAttempts = 6;

        /// <Constructor>
        /// 
        /// This is the constructor of the MenuScreen class.
        /// Accepts a storage parameter of type Storage and initializes the Storage field with this value.
        /// Creates a new UserManagement object and assigns it to the _userManagement field.
        /// Similarly, it creates a BankAccountManagement object and assigns it to the _bankAccountManagement field.
        /// 
        /// </Constructor>
        /// <param name="storage"></param>
        public MenuScreen(Storage storage)
        {
            Storage = storage;
            _userManagement = new UserManagement();
            _bankAccountManagement = new BankAccountManagement();
        }

        /// <Menu_Method>
        /// This Menu method represents the textual user interface for the application's main menu.
        /// </Menu_Method>
        public virtual void Menu()
        {
            bool exit = true;

            while (exit)
            {
                Console.Clear();
                Console.WriteLine("Welcome to PolyBank Application\nPlease enter your choice\n");
                Console.WriteLine("1.Create a new user");
                Console.WriteLine("2.Log in to user");
                Console.WriteLine("3.Exit Program");
                
                var choice = int.Parse(Console.ReadLine());

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
                        Console.ReadKey();
                        break;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>

        private void RenderRegisterMenu()
        {
            Console.Clear();

            bool log = true;
            string login = string.Empty;

            while (log)
            {
                Console.Clear();
                Console.Write("Create your login please: ");
                login = Console.ReadLine();
                
                if (login != string.Empty)
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

            bool pas = true;
            string password = string.Empty;
            
            while (pas)
            {
                Console.Clear();

                Console.Write("Create your password please: ");
                password = Console.ReadLine();
                if (password != string.Empty)
                {
                    Console.Write("Repeat your password please: ");
                    if (password == Console.ReadLine())
                    {
                        Console.WriteLine("Your account successfully created");
                        pas = false;
                    }
                }
            }

            var customer = new User { Login = login, Password = password };
            _userManagement.AddUser(customer);
            
            _bankAccountManagement.CreateNewBankAccount(ref customer);

            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        private void WriteUserLogin()
        {
            string logLogin = string.Empty;

            int i = 0;

            bool log = true;
            while (log)
            {
                Console.Clear();

                Console.Write("Write your login please: ");
                logLogin = Console.ReadLine();

                if (logLogin != string.Empty)
                {
                    if (_userManagement.IsUserExist(logLogin))
                    {
                        Console.Write("Write your password: ");
                        var logPassword = Console.ReadLine();

                        if (_userManagement.IsPasswordRight(logLogin, logPassword))
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

                if (i >= MaxAttempts)
                {
                    log = false;
                }
            }

            if (i < MaxAttempts)
            {
                User user = _userManagement.GetUserInfo(logLogin);
                Storage.User = user;
                Storage.BankAccount = user.Accounts.First();
                UserLoggedInMenu();
            }
            else
            {
                GivePropose();
            }
        }

        private void ShowUserInfo()
        {
            Console.Clear();

            Console.WriteLine("Bank account info");
            Console.WriteLine($"Login: {Storage.User.Login}");
            Console.WriteLine($"Your bank account number: {Storage.BankAccount.AccountNumber}");
            Console.WriteLine($"Balance: {Storage.BankAccount.Balance}");
            Console.ReadKey();
        }

        public void UserLoggedInMenu()
        {
            bool exit = true;

            Settings settings = new Settings(Storage);

            BasicAccountOperations accountOperations = new BasicAccountOperations(Storage);

            while (exit)
            {
                if(Storage.User != null)
                {
                    Console.Clear();

                    Console.WriteLine("You logged in successfully, welcome: " + Storage.User.Login + "\n");
                    Console.WriteLine("Choose the option 1-3");
                    Console.WriteLine("1.Show bank account details");
                    Console.WriteLine("2.Options");
                    Console.WriteLine("3.Operations");
                    Console.WriteLine("4.Exit to main menu");

                    var choice = int.Parse(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            ShowUserInfo();
                            break;

                        case 2:
                            settings.Menu();
                            break;

                        case 3:
                            accountOperations.OperationsMenu();
                            break;

                        case 4:
                            exit = false;
                            break;

                        default:
                            Console.WriteLine("Choose the option 1-3");
                            break;
                    }
                }
                else
                {
                    exit = false;
                }

            }
        }
        
        private void GivePropose()
        {
            bool exit = true;

            while (exit)
            {
                Console.Clear();

                Console.WriteLine("If you don't remember your login, or password.");
                Console.WriteLine("1.Try again");
                Console.WriteLine("2.Create a new user");
                Console.WriteLine("3.Exit to main menu");

                var choice = int.Parse(Console.ReadLine());

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