using System;
using Vinnyk_Tomkiv_Zaliczenie.Models;
using Vinnyk_Tomkiv_Zaliczenie.Services.BankAccManagement;
using Vinnyk_Tomkiv_Zaliczenie.Services.CustomerManagements;

namespace Vinnyk_Tomkiv_Zaliczenie.Services.AccountOperations
{
    /// <summary>
    /// A class with a menu interface for conducting basic monetary operations, which uses methods from the BankAccountManagement class,
    /// which contains the logic of these operations.
    /// </summary>
    
    public class BasicAccountOperations
    {
        // An interface for managing a bank account that has methods for depositing and withdrawing money.
        private readonly IBankAccountManagement _bankAccountManagement;
        // User management interface that may provide access to user information.
        private readonly IUserManagement _userManagement;
        // A data storage class used to store information about users and their bank accounts.
        private readonly Storage _storage;

        /// <summary>
        /// The constructor gets the Storage object for data storage and initializes the _storage, _bankAccountManagement, and _userManagement fields.
        /// </summary>
        /// <param name="storage"></param>

        public BasicAccountOperations(Storage storage)
        {
            _storage = storage;
            _bankAccountManagement = new BankAccountManagement();
            _userManagement = new UserManagement();
        }

        /// <summary>
        /// A method for displaying a menu with a selection of operations that the user can perform with the current bank account.
        /// </summary>

        public void OperationsMenu()
        {
            // Loop exit variable.
            bool exit = true;

            while (exit)
            {
                Console.Clear();
                Console.WriteLine("What would you like to do\n");
                Console.WriteLine("1.Deposit");
                Console.WriteLine("2.Withdraw");
                Console.WriteLine("3.Transfer to another user");
                Console.WriteLine("4.Exit to menu");

                // Reading the user's selection from the console and converting it to an integer.
                // If User write something wrong, or press enter without text, program will catch this and say about this error.

                if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0)
                {

                    switch (choice)
                    {
                        case 1:
                            Deposit();
                            exit = false;
                            break;

                        case 2:
                            Withdraw();
                            exit = false;
                            break;

                        case 3:
                            Transfer();
                            exit = false;
                            break;

                        case 4:
                            exit = false;
                            break;

                        default:
                            Console.WriteLine("Try another option between 1-4");
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

        // The logic of depositing money into a storage current bank account

        private void Deposit()
        {
            // Loop exit variable.
            // If the user makes many mistakes, the program will exit the loop and go to UserLoggedInMenu. 
            int tries = 0;

            while (true)
            {
                Console.Clear();

                Console.Write("How much do you want deposit to your account: ");

                // Reading the user's selection from the console and converting it to an integer.
                // If User write something wrong, or press enter without text, program will catch this and say about this error.

                if (double.TryParse(Console.ReadLine(), out double amount) && amount > 0)
                {

                    // Initialize the var type element for work with the method
                    var userTemp = _storage.User;
                    var bankAccountTemp = _storage.BankAccount;
                    _bankAccountManagement.Deposit(ref userTemp, ref bankAccountTemp, amount);

                    // Update user and bank account objects in the storing class.
                    _storage.User = userTemp;
                    _storage.BankAccount = bankAccountTemp;

                    // Display a message about a successful deposit, indicating the amount, account number and new balance.
                    Console.WriteLine(
                        $"Deposited {amount} PLN to account {bankAccountTemp.AccountNumber} . New balance:  {bankAccountTemp.Balance} PLN");

                    Console.ReadKey();
                    break;
                }
                else
                {
                    Console.WriteLine("Please write how much do you want deposit to your account");
                    tries++;
                    Console.ReadKey();
                }

                if(tries == 3)
                {
                    break;
                }
            }

        }

        // The logic of withdrawing money from a deposit account
        private void Withdraw()
        {
            // Initialize the BankAccount type element for work with the method
            BankAccount account = _storage.BankAccount;

            // Loop exit variable.
            // If the user makes many mistakes, the program will exit the loop and go to UserLoggedInMenu. 
            int tries = 0;

            while (true)
            {
                Console.Clear();

                Console.Write("How much do you want Withdraw from your account: ");

                // Reading the user's selection from the console and converting it to an integer.
                // If User write something wrong, or press enter without text, program will catch this and say about this error.

                if (double.TryParse(Console.ReadLine(), out double amount) && amount > 0)
                {
                    // Checking whether the entered amount is less than or equal to the amount on the user's balance.
                    if (amount <= account.Balance)
                    {
                        // Initialize the var type element for work with the method
                        var userTemp = _storage.User;
                        var bankAccountTemp = _storage.BankAccount;
                        _bankAccountManagement.Withdraw(ref userTemp, ref bankAccountTemp, amount);

                        // Update user and bank account objects in the storing class.
                        _storage.User = userTemp;
                        _storage.BankAccount = bankAccountTemp;

                        // Display a message about a successful withdraw, indicating the amount, account number and new balance.
                        Console.WriteLine(
                            $"Withdrawed {amount} PLN from account {bankAccountTemp.AccountNumber}. New balance: {bankAccountTemp.Balance} PLN");

                        Console.ReadKey();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("There are not enough funds in your balance to withdraw");
                        Console.ReadKey();
                        tries++;
                        break;
                    }

                }
                else
                {
                    Console.WriteLine("Please write how much do you want Withdraw from your account");
                    tries++;
                    Console.ReadKey();
                }

                if (tries == 3)
                {
                    break;
                }
            }

        }

        // Логіка переказу з рахунку зберігання на інший рахунок
        public void Transfer()
        {
            Console.Clear();
            Console.WriteLine("Please, write the user login");
            string login = Console.ReadLine();
            // Getting information about the user with the entered login using _userManagement.
            User user = _userManagement.GetUserInfo(login);

            // Loop exit variable.
            // If the user makes many mistakes, the program will exit the loop and go to UserLoggedInMenu. 
            int tries = 0;

            // Checking if the user with the entered login exists.
            if (user != null)
            {
                while (true)
                {
                    Console.Clear();

                    // Display information about all user accounts (number and balance).
                    for (int i = 0; i < user.Accounts.Count; i++)
                    {
                        BankAccount account = user.Accounts[i];
                        Console.Write($"{i + 1}.Account num: ");
                        Console.Write(account.AccountNumber);
                        Console.Write("; Balance: ");
                        Console.WriteLine(account.Balance + "\n");
                    }

                    Console.WriteLine("Please choose the account");

                    // Reading the index of the selected account.
                    // If User write something wrong, or press enter without text, program will catch this and say about this error.

                    if (int.TryParse(Console.ReadLine(), out int index) && index > 0)
                    {
                        if (index > 0 && index <= user.Accounts.Count)
                        {
                            // Selection of account for transfer based on composite index.
                            var account = user.Accounts[index - 1];

                            Console.Clear();
                            Console.Write("How much you want to transfer: ");

                            // Checking the correctness of the entered amount for transfer.
                            // If User write something wrong, or press enter without text, program will catch this and say about this error.

                            if (double.TryParse(Console.ReadLine(), out double amount) && amount > 0)
                            {
                                // Checking whether there are enough funds in the user's account to perform the transfer.
                                if (amount <= _storage.BankAccount.Balance)
                                {
                                    // Calling the method for depositing money to the selected by user account.
                                    _bankAccountManagement.Deposit(ref user, ref account, amount);

                                    // Initialize the var type element for work with the method
                                    var userTemp = _storage.User;
                                    var bankAccountTemp = _storage.BankAccount;
                                    _bankAccountManagement.Withdraw(ref userTemp, ref bankAccountTemp, amount);

                                    // Update user and bank account objects in the storing class.
                                    _storage.User = userTemp;
                                    _storage.BankAccount = bankAccountTemp;

                                    // Displaying a message about a successful money transfer.
                                    Console.WriteLine(
                                        $"Transfered {amount} from your {bankAccountTemp.AccountNumber} account to {user.Login}" +
                                        $" {account.AccountNumber} account number");

                                    Console.ReadKey();
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("There are not enough funds in your balance to transfer");
                                    Console.ReadKey();
                                    tries++;
                                    break;
                                }

                            }
                            else
                            {
                                Console.WriteLine("Please write how much do you want Withdraw from your account");
                                tries++;
                                Console.ReadKey();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid account number selection");
                            tries++;
                            Console.ReadKey();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid account number selection");
                        tries++;
                        Console.ReadKey();
                    }

                    if (tries == 3)
                    {
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("There is no user with this login, please try another one");
                Console.ReadKey();
            }            
        }
    }
}