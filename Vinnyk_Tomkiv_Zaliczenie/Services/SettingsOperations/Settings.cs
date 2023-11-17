using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vinnyk_Tomkiv_Zaliczenie.Models;
using Vinnyk_Tomkiv_Zaliczenie.Services.BankAccManagement;
using Vinnyk_Tomkiv_Zaliczenie.Services.CustomerManagements;
using Vinnyk_Tomkiv_Zaliczenie.Services.MenuOperation;

namespace Vinnyk_Tomkiv_Zaliczenie.Services.OptionOperations
{
    public class Settings : ISettings
    {
        private readonly IBankAccountManagement _bankAccountManagement;
        private readonly IUserManagement _userManagement;
        private readonly Storage _storage;

        public Settings(Storage storage)
        {
            _bankAccountManagement = new BankAccountManagement();
            _userManagement = new UserManagement();
            _storage = storage;
        }

        public void SettingsMenu()
        {
            int choice;
            bool exit = true;

            while (exit)
            {

                Console.Clear();
                Console.WriteLine("What would you like to do");
                Console.WriteLine("1.Change bank account");
                Console.WriteLine("2.Add new bank account");
                Console.WriteLine("3.Remove bank account");
                Console.WriteLine("4.Remove user");
                Console.WriteLine("5.Exit settings");

                choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        ShowBankAccounts();
                        exit = false;
                        break;

                    case 2:
                        BankAccount bankAccount = _bankAccountManagement.BankAccReg();
                        _bankAccountManagement.AddBankAcc(bankAccount, _storage.User.Login);
                        Console.WriteLine("New Bank account have been added");
                        Console.ReadKey();
                        break;

                    case 3:
                        Console.WriteLine("Bank acc removed");
                        Console.ReadKey();
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

        public void ShowBankAccounts()
        {
            Console.Clear();
            User user = _storage.User;

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
                int accNum = int.Parse(user.Accounts[index - 1].AccountNumber);

                BankAccount bankAccount = _bankAccountManagement.GetBankAccInfo(user.Login, accNum);

                //_bankAccountManagement.ChangeBankAccount(index,login);

                Console.WriteLine("You have switched to account number: " + accNum);

                Console.ReadKey();
                _storage.BankAccount = bankAccount;
                
            }
            else
            {
                Console.WriteLine("Invalid account number selection");
            }
        }
    }
}
