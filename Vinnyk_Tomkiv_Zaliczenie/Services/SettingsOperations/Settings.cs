using System;
using Vinnyk_Tomkiv_Zaliczenie.Models;
using Vinnyk_Tomkiv_Zaliczenie.Services.BankAccManagement;
using Vinnyk_Tomkiv_Zaliczenie.Services.CustomerManagements;
using Vinnyk_Tomkiv_Zaliczenie.Services.MenuOperation;
using Vinnyk_Tomkiv_Zaliczenie.Services.OptionOperations;

namespace Vinnyk_Tomkiv_Zaliczenie.Services.SettingsOperations
{
    public class Settings : MenuScreen
    {
        private readonly IBankAccountManagement _bankAccountManagement;

        public Settings(Storage storage) : base(storage)
        {
            _bankAccountManagement = new BankAccountManagement();
        }

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
                        var storageUser = Storage.User;
                        _bankAccountManagement.CreateNewBankAccount(ref storageUser);
                        Storage.User = storageUser;

                        Console.WriteLine("New Bank account have been added");
                        Console.ReadKey();
                        break;

                    case 3:
                        RemoveBankAccount();
                        exit = false;
                        break;

                    case 4:
                        Console.WriteLine("User removed");
                        Console.ReadKey();
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

        private void ShowBankAccounts()
        {
            Console.Clear();
            User user = Storage.User;

            for (int i = 0; i < user.Accounts.Count; i++)
            {
                BankAccount account = user.Accounts[i];
                Console.Write($"{i + 1}.Account num: ");
                Console.Write(account.AccountNumber);
                Console.Write("; Balance: ");
                Console.WriteLine(account.Balance + "\n");
            }

            Console.WriteLine("Please choose the account");
            int index = int.Parse(Console.ReadLine());

            if (index > 0 && index <= user.Accounts.Count)
            {
                var account = user.Accounts[index - 1];

                Console.WriteLine("You have switched to account number: " + account.AccountNumber);

                Console.ReadKey();
                Storage.BankAccount = account;

            }
            else
            {
                Console.WriteLine("Invalid account number selection");
            }
        }

        public void RemoveBankAccount()
        {
            Console.Clear();
            User user = Storage.User;

            for (int i = 0; i < user.Accounts.Count; i++)
            {
                BankAccount account = user.Accounts[i];
                Console.Write($"{i + 1}.Account num: ");
                Console.Write(account.AccountNumber);
                Console.Write("; Balance: ");
                Console.WriteLine(account.Balance + "\n");
            }

            Console.WriteLine("Please choose the account");
            int index = int.Parse(Console.ReadLine());

            if (index > 0 && index <= user.Accounts.Count)
            {
                var account = user.Accounts[index - 1];

                Console.WriteLine($"Your account number: {account.AccountNumber}, have been removed");

                Storage.User = _bankAccountManagement.RemoveFromUserBankAccList(account.AccountNumber, user.Login);

                Console.ReadKey();
                Storage.BankAccount = Storage.User.Accounts[0];
            }
            else
            {
                Console.WriteLine("Invalid account number selection");
            }
        }
    }
}
