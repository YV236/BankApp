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
        private readonly IMenu _menu;

        public Settings()
        {
            _bankAccountManagement = new BankAccountManagement();
            _userManagement = new UserManagement();
            _menu = new MenuManagement();
        }

        public void SettingsMenu(string login, int index)
        {
            int choice;
            bool exit = true;
            while(exit)
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
                        ShowBankAccounts(login);
                        exit = false;
                        break;

                    case 2:
                        BankAccount bankAccount = _bankAccountManagement.BankAccReg();
                        _bankAccountManagement.AddToUserBankAccList(bankAccount, login);
                        Console.WriteLine("New Bank account have been added");
                        Console.ReadKey();
                        break;

                    case 3:
                        Console.WriteLine("Bank acc removed");
                        Console.ReadKey();
                        exit = false;
                        break;

                    case 4:
                        Console.WriteLine("User removed");
                        Console.ReadKey();
                        exit = false;
                        break;

                    case 5:
                        _menu.UserLoginedMenu(login, index);
                        exit = false;
                        break;

                    default:
                        Console.WriteLine("Error try another option 1-5.");
                        Console.ReadKey();
                        break;

                }
            }
        }

        public void ShowBankAccounts(string login)
        {
            Console.Clear();
            BankAccount bankAccount = _bankAccountManagement.GetBankAccInfo(login, 0);
            User user = _userManagement.GetUserInfo(login);

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
                bankAccount = _bankAccountManagement.GetBankAccInfo(login, index);

                //_bankAccountManagement.ChangeBankAccount(index,login);

                Console.WriteLine("You have switched to account number: " + bankAccount.AccountNumber);

                Console.ReadKey();

                _menu.UserLoginedMenu(login, index);

            }
            else
            {
                Console.WriteLine("Invalid account number selection");
            }
        }
    }
}
