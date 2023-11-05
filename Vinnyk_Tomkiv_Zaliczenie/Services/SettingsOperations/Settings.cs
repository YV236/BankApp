using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vinnyk_Tomkiv_Zaliczenie.Models;
using Vinnyk_Tomkiv_Zaliczenie.Services.BankAccManagement;
using Vinnyk_Tomkiv_Zaliczenie.Services.CustomerManagements;

namespace Vinnyk_Tomkiv_Zaliczenie.Services.OptionOperations
{
    public class Settings : ISettings
    {
        private readonly IBankAccountManagement _bankAccountManagement;
        private readonly IUserManagement _userManagement;

        public Settings()
        {
            _bankAccountManagement = new BankAccountManagement();
            _userManagement = new UserManagement();
        }

        public void SettingsMenu(string login)
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
                        ShowBankAccounts(login);
                        Console.ReadKey();
                        break;

                    case 2:
                        BankAccount bankAccount = _bankAccountManagement.BankAccReg();
                        _bankAccountManagement.AddBankAcc(bankAccount, login);
                        break;

                    case 3:
                        Console.WriteLine("Bank acc removed");
                        break;

                    case 4:
                        Console.WriteLine("User removed");
                        break;

                    case 5:
                        exit = false;
                        break;
                }
            }
        }

        public void ShowBankAccounts(string login)
        {
            Console.Clear();
            User user = _userManagement.GetUserInfo(login);

            for (int i = 0; i < user.Accounts.Count; i++)
            {
                BankAccount account = user.Accounts[i];
                Console.Write($"{i+1}.Account num: ");
                Console.WriteLine(account.AccountNumber);
                Console.Write("Balance: ");
                Console.WriteLine(account.Balance);
            }
        }
    }
}
