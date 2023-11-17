using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vinnyk_Tomkiv_Zaliczenie;
using Vinnyk_Tomkiv_Zaliczenie.Models;
using Vinnyk_Tomkiv_Zaliczenie.Services.BankAccManagement;
using Vinnyk_Tomkiv_Zaliczenie.Services.CustomerManagements;
using Vinnyk_Tomkiv_Zaliczenie.Services.MenuOperation;

namespace Vinnyk_Tomkiv_Zaliczenie
{
    // Клас для рахунку зберігання
    public class BasicAccountOperations : BankAccountManagement
    {
        private readonly IBankAccountManagement _bankAccountManagement;
        private readonly IUserManagement _userManagement;

        public BasicAccountOperations()
        {
            _bankAccountManagement = new BankAccountManagement();
            _userManagement = new UserManagement();
        }
        public void OperationsMenu(string login, int accNum)
        {
            int choice;

            Console.Clear();

            Console.WriteLine("1.Deposit");
            Console.WriteLine("2.Withdraw");
            Console.WriteLine("3.Transfer to another user");
            Console.WriteLine("4.Exit to menu");

            choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Deposit(login, accNum, 0);
                    break;

                case 2:
                    Withdraw(login, accNum, 0);
                    break;

                case 3:
                    Console.WriteLine("Transfer");
                    break;

                case 4:
                    break;
            }
        }

        // Логіка внесення грошей на рахунок зберігання
        public override void Deposit(string login, int accNum, double amount)
        {
            BankAccountManagement bankAccountManagement = new BankAccountManagement();

            while (true)
            {
                Console.Clear();

                Console.Write("How much do you want deposit to your account: ");

                if (double.TryParse(Console.ReadLine(), out amount) && amount > 0)
                {
                    bankAccountManagement.Deposit(login, accNum, amount);
                    break;
                }
                else
                {
                    Console.WriteLine("Please write how much do you want deposit to your account");
                    Console.ReadKey();
                }
            }
            Console.ReadKey();

        }

        // Логіка зняття грошей з рахунку зберігання
        public override void Withdraw(string login, int accNum, double amount)
        {
            BankAccountManagement bankAccountManagement = new BankAccountManagement();
            BankAccount account = bankAccountManagement.GetBankAccInfo(login, accNum);

            while (true)
            {
                Console.Clear();

                Console.Write("How much do you want Withdraw from your account: ");

                if (double.TryParse(Console.ReadLine(), out amount) && amount > 0)
                {
                    if(amount< account.Balance)
                    {
                        bankAccountManagement.Withdraw(login, accNum, amount);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("There are not enough funds in your balance to withdraw");
                    }
                }
                else
                {
                    Console.WriteLine("Please write how much do you want Withdraw from your account");
                    Console.ReadKey();
                }
            }
            Console.ReadKey();

        }

        // Логіка переказу з рахунку зберігання на інший рахунок
        public override void Transfer(BankAccount targetAccount, BankAccount bankAccount, double amount)
        {
            Console.Clear();
            string login = targetAccount.Id;
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
        }
    }

}
