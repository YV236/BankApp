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
using Vinnyk_Tomkiv_Zaliczenie.Services.SettingsOperations;

namespace Vinnyk_Tomkiv_Zaliczenie.Services.MenuOperation
{
    /// <MenuScreen_Class>
     
    /// A class for highlighting the main menus, the registration menu and the account login menu
    
    /// </MenuScreen_Class>
    public class MenuScreen
    {
        // A private field _userManagement is declared, which represents an object that implements the IUserManagement interface
        private readonly IUserManagement _userManagement;
        // A private field _bankAccountManagement is declared, which represents an object that implements the IBankAccountManagement interface.
        private readonly IBankAccountManagement _bankAccountManagement;
        // Declares a protected Storage field that represents an object of the Storage class. This field can be used in derived classes.
        protected readonly Storage Storage;
        // A MaxAttempts constant with a value of 6 is declared to represent the maximum number of attempts during the registration process.
        private const int MaxAttempts = 6;

        /// <Constructor>
         
        /// This is the constructor of the MenuScreen class.
        /// Accepts a storage parameter of type Storage and initializes the Storage field with this value.
        /// Creates a new UserManagement object and assigns it to the _userManagement field.
        /// Similarly, it creates a BankAccountManagement object and assigns it to the _bankAccountManagement field.
         
        /// </Constructor>
        /// <param name="storage"></param>
        public MenuScreen(Storage storage)
        {
            Storage = storage;
            _userManagement = new UserManagement();
            _bankAccountManagement = new BankAccountManagement();
        }

        /// <Menu>
        /// This Menu method represents the textual user interface for the application's main menu.
        /// </Menu>
        public virtual void Menu()
        {
            // A logical variable exit is created and initialized with the value true. This variable is used to control loop exit.
            bool exit = true;

            // A while loop that continues until exit is true.
            while (exit)
            {
                // Clearing the console and displaying a text menu to the user.
                Console.Clear();
                Console.WriteLine("Welcome to PolyBank Application\nPlease enter your choice\n");
                Console.WriteLine("1.Create a new user");
                Console.WriteLine("2.Log in to user");
                Console.WriteLine("3.Exit Program");

                // Reading the user's selection from the console and converting it to an integer.
                // If User write something wrong, or press enter without text, program will catch this and say about this error.

                if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0)
                {
                    /// <summary>

                    /// A switch construct that handles user selection.
                    /// Calls the appropriate methods depending on the selection.
                    /// If menu item 3 is selected, the exit variable is set to false, which will exit the loop and terminate the program.
                    /// If the selection is incorrect, an error message is displayed.

                    /// </summary>

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
                else
                {
                    Console.WriteLine("Invalid choice selection, please try again.");
                    Console.ReadKey();
                }                
            }
        }

        /// <RenderRegisterMenu>
        /// This RenderRegisterMenu method is responsible for displaying the user's registration menu.
        /// </RenderRegisterMenu>

        private void RenderRegisterMenu()
        {
            // Clearing the console to display a new login screen.
            Console.Clear();

            // Cycle for entering a login. The loop continues as long as log is true.

            bool log = true;
            string login = string.Empty;

            while (log)
            {
                Console.Clear();
                Console.Write("Create your login please: ");
                login = Console.ReadLine();

               // Checking if the user with the entered login already exists.
               // If not, the loop terminates; otherwise, a message is displayed that a user with this login already exists.

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

            // Password input cycle. The loop continues as long as pas is true.

            bool pas = true;
            string password = string.Empty;
            
            while (pas)
            {
                Console.Clear();

                Console.Write("Create your password please: ");
                password = Console.ReadLine();

                // Checking whether the entered passwords match.                 
                // If the condition is met, the loop ends and a message about the successful creation of the account is displayed.

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

            // Create a new user object and call the method to create a new bank account for him.

            var customer = new User { Login = login, Password = password };
            _userManagement.AddUser(customer);
            
            _bankAccountManagement.CreateNewBankAccount(ref customer);

            // Outputting a message and waiting for any key to be pressed to continue.

            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        /// <WriteUserLogin>
        /// This WriteUserLogin method is responsible for entering the user's login and password
        /// </WriteUserLogin>

        private void WriteUserLogin()
        {
            string logLogin = string.Empty;

            int i = 0;

            // Cycle for entering login and password. The loop continues as long as log is true.

            bool log = true;
            while (log)
            {
                // Clearing the console to display a new login and password screen
                Console.Clear();

                Console.Write("Write your login please: ");
                logLogin = Console.ReadLine();

                // If the condition is met, the loop ends and the user is successfully logged in.
                // Otherwise, an error message is displayed and the attempt counter is incremented.

                // If the user does not enter anything (string.Empty),
                // it is accepted as a failed attempt, which increases the number of failed attempts

                if (logLogin != string.Empty)
                {
                    // Checking whether the entered login exists

                    if (_userManagement.IsUserExist(logLogin))
                    {
                        Console.Write("Write your password: ");
                        var logPassword = Console.ReadLine();

                        // Checking if the entered password is correct for the specified user.
                        // If password is incorrect, it increases the number of failed attempts and starts the cycle again
                        // Until the number of attempts runs out

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

                // Checking whether the number of attempts has exceeded the maximum allowed. If the condition is met, the loop ends.

                if (i >= MaxAttempts)
                {
                    log = false;
                }
            }

            // In the case of a successful login (the number of attempts is less than the maximum allowed),
            // information about the user is obtained, the corresponding fields in the Storage class are set, and the UserLoggedInMenu method is called.
            

            if (i < MaxAttempts)
            {
                // An object of the User class is created,
                // which is equated with the data that was taken from the file under the specified login,
                // which is unique.

                User user = _userManagement.GetUserInfo(logLogin);

                //After that, it is written into the object of the Storage class that was initialized in the constructor
                Storage.User = user;
                Storage.BankAccount = user.Accounts.First();
                UserLoggedInMenu();
            }
            else
            {
                // Otherwise, the GivePropose method is called.
                GivePropose();
            }
        }

        /// <ShowUserInfo>
        /// This method displays information about the user and their bank account.
        /// </ShowUserInfo>

        private void ShowUserInfo()
        {
            Console.Clear();

            Console.WriteLine("Bank account info");
            Console.WriteLine($"Login: {Storage.User.Login}");
            Console.WriteLine($"Your bank account number: {Storage.BankAccount.AccountNumber}");
            Console.WriteLine($"Balance: {Storage.BankAccount.Balance}");
            Console.ReadKey();
        }

        /// <UserLoggedInMenu>
        /// This method is responsible for displaying the main menu after the user logs out.
        /// </UserLoggedInMenu>

        public void UserLoggedInMenu()
        {
            // Initialize the exit variable, which determines whether the user has decided to exit the menu or not.

            bool exit = true;

            // Creating instances of the Settings and BasicAccountOperations classes to work with bank account settings and operations.

            Settings settings = new Settings(Storage);

            BasicAccountOperations accountOperations = new BasicAccountOperations(Storage);

            while (exit)
            {
                // Checking if the user is logged in. If the condition is met, the main menu is displayed;
                // Otherwise, exit is set to false and the loop terminates.

                if (Storage.User != null)
                {
                    Console.Clear();

                    Console.WriteLine("You logged in successfully, welcome: " + Storage.User.Login + "\n");
                    Console.WriteLine("Choose the option 1-3");
                    Console.WriteLine("1.Show bank account details");
                    Console.WriteLine("2.Options");
                    Console.WriteLine("3.Operations");
                    Console.WriteLine("4.Exit to main menu");

                    // Reading the user's selection from the console and converting it to an integer.
                    // If User write something wrong, or press enter without text, program will catch this and say about this error.

                    if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0)
                    {
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
                                Console.ReadKey();
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice selection, please try again.");
                        Console.ReadKey();
                    }                    
                }
                else
                {
                    exit = false;
                }

            }
        }

        /// <GivePropose>
        /// This method displays a menu of suggestions if the user cannot enter a login and password.
        /// </GivePropose>

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


                // Reading the user's selection from the console and converting it to an integer.
                // If User write something wrong, or press enter without text, program will catch this and say about this error.
                if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0)
                {
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
                            Console.ReadKey();
                            break;
                    }
                }
               
            }
        }
    }
}