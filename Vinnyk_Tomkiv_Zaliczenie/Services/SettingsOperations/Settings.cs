using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vinnyk_Tomkiv_Zaliczenie.Models;
using Vinnyk_Tomkiv_Zaliczenie.Services.BankAccManagment;

namespace Vinnyk_Tomkiv_Zaliczenie.Services.OptionPorations
{
    public class Settings : ISettings
    {
        private readonly IBankAccountManagment _bankAccountManagment;
        public Settings()
        {
            _bankAccountManagment = new BankAccountManagment();
        }

        public void SettingsMenu(string login)
        {
            int choice;
            bool exit = true;

            while (exit)
            {
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
                        Console.WriteLine("Bank acc changed");
                        Console.ReadKey();
                        break;

                    case 2:
                        BankAccount bankAccount = _bankAccountManagment.BankAccReg();
                        _bankAccountManagment.AddBankAcc(bankAccount, login);
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

        public void ShowBankAccounts()
        {

        }
    }
}
