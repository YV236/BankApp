using System;
using Vinnyk_Tomkiv_Zaliczenie.Models;
using Vinnyk_Tomkiv_Zaliczenie.Services.BankAccManagement;
using Vinnyk_Tomkiv_Zaliczenie.Services.CustomerManagements;
using Vinnyk_Tomkiv_Zaliczenie.Services.MenuOperation;

namespace Vinnyk_Tomkiv_Zaliczenie.Services.SettingsOperations
{
    public class Settings : MenuScreen
    {
        private readonly IBankAccountManagement _bankAccountManagement;
        private readonly IUserManagement _userManagement;

        // A class constructor that takes a storage object and passes it to the MenuScreen base class

        public Settings(Storage storage) : base(storage)
        {
            // Initialize objects to manage bank accounts, menu screens, and users
            _bankAccountManagement = new BankAccountManagement();
            _userManagement = new UserManagement();
        }

        // Overridden the Menu method from the base class

        public override void Menu()
        {
            bool exit = true;

            while (exit)
            {
                Console.Clear();
                Console.WriteLine("What would you like to do\n");
                Console.WriteLine("1.Change bank account");
                Console.WriteLine("2.Add new bank account");
                Console.WriteLine("3.Remove bank account");
                Console.WriteLine("4.Remove user");
                Console.WriteLine("5.Exit settings");

                var choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        ShowBankAccounts();
                        exit = false;
                        break;

                    case 2:
                        // Adding a new bank account
                        var storageUser = Storage.User;
                        _bankAccountManagement.CreateNewBankAccount(ref storageUser);
                        Storage.User = storageUser;

                        Console.WriteLine("New Bank account have been added");
                        Console.ReadKey();
                        break;

                    case 3:
                        // Call the RemoveBankAccount method
                        RemoveBankAccount();
                        exit = false;
                        break;

                    case 4:
                        // Initialize the var type element for work with the method
                        var storageUser1 = Storage.User;
                        // The RemoveUser method return null because the User is removed
                        Storage.User = _userManagement.RemoveUser(storageUser1.Login);

                        Console.WriteLine("User removed");
                        Console.ReadKey();
                        exit = false;

                        break;

                    case 5:
                        exit = false;
                        break;

                    default:
                        Console.WriteLine("Error try another option 1-5.");
                        Console.ReadKey();
                        break;
                }
            }

        }

        // A method for changing a current bank account

        private void ShowBankAccounts()
        {
            Console.Clear();
            // Initialize the User type object for work with the method
            User user = Storage.User;

            //Showing all current user bank accounts
            for (int i = 0; i < user.Accounts.Count; i++)
            {
                BankAccount account = user.Accounts[i];
                Console.Write($"{i + 1}.Account num: ");
                Console.Write(account.AccountNumber);
                Console.Write("; Balance: ");
                Console.WriteLine(account.Balance + "\n");
            }

            //The user's selection of the desired account.
            Console.WriteLine("Please choose the account");
            int index = int.Parse(Console.ReadLine());

            //If user's selection is right the current account will change
            if (index > 0 && index <= user.Accounts.Count)
            {
                // Initialize the var type element for work with the method
                var account = user.Accounts[index - 1];

                Console.WriteLine("You have switched to account number: " + account.AccountNumber);

                Console.ReadKey();

                //Changing current bank account in storage class
                Storage.BankAccount = account;

            }
            else
            {
                Console.WriteLine("Invalid account number selection");
                Console.ReadKey();
            }
        }

        // A method for removing/deleting the chosen bank account
        public void RemoveBankAccount()
        {
            Console.Clear();
            // Initialize the User type object for work with the method
            User user = Storage.User;

            // Showing all current user bank accounts
            for (int i = 0; i < user.Accounts.Count; i++)
            {
                BankAccount account = user.Accounts[i];
                Console.Write($"{i + 1}.Account num: ");
                Console.Write(account.AccountNumber);
                Console.Write("; Balance: ");
                Console.WriteLine(account.Balance + "\n");
            }

            // The user's selection of the desired account.
            Console.WriteLine("Please choose the account");
            int index = int.Parse(Console.ReadLine());

            // If user's selection is right the current account will change
            if (index > 0 && index <= user.Accounts.Count)
            {
                // Choosing the bank account
                var account = user.Accounts[index - 1];

                // If there is money in the selected account, the program will notify the user about this and go to the UserLoggedInMenu.
                if (account.Balance == 0)
                {
                    Console.WriteLine($"Your account number: {account.AccountNumber}, have been removed");

                    // Deleting an account through the bank BankAccountManagement facility
                    Storage.User = _bankAccountManagement.RemoveFromUserBankAccList(account.AccountNumber, user.Login);

                    Console.ReadKey();

                    // Choosing the new current bank account in case the user deletes the current bank account.
                    // If he deletes the bank account in index = 0 program will change the array of bank account
                    // Storage.User.Accounts[1] will be Storage.User.Accounts[0]
                    Storage.BankAccount = Storage.User.Accounts[0];
                }
                else
                {
                    Console.WriteLine("You have funds in the selected account. Please, withdraw money from the account before deleting it.");
                    Console.ReadKey();
                }

            }
            else
            {
                Console.WriteLine("Invalid account number selection");
                Console.ReadKey();
            }
        }
    }
}
