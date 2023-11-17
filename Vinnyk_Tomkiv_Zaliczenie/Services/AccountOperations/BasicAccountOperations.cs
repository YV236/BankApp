using System;
using Vinnyk_Tomkiv_Zaliczenie.Models;
using Vinnyk_Tomkiv_Zaliczenie.Services.BankAccManagment;
using Vinnyk_Tomkiv_Zaliczenie.Services.CustomerManagements;

namespace Vinnyk_Tomkiv_Zaliczenie.Services.AccountOperations
{
    // Клас для рахунку зберігання
    public class BasicAccountOperations
    {
        private readonly IBankAccountManagement _bankAccountManagement;
        private readonly IUserManagement _userManagement;
        private readonly Storage _storage;

        public BasicAccountOperations(Storage storage)
        {
            _storage = storage;
            _bankAccountManagement = new BankAccountManagement();
            _userManagement = new UserManagement();
        }

        public void OperationsMenu()
        {
            Console.Clear();

            Console.WriteLine("1.Deposit");
            Console.WriteLine("2.Withdraw");
            Console.WriteLine("3.Transfer to another user");
            Console.WriteLine("4.Exit to menu");

            var choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Deposit();
                    break;

                case 2:
                    Withdraw(_storage.BankAccount.AccountNumber);
                    break;

                case 3:
                    Console.WriteLine("Transfer");
                    break;

                case 4:
                    break;
            }
        }

        // Логіка внесення грошей на рахунок зберігання
        private void Deposit()
        {
            while (true)
            {
                Console.Clear();

                Console.Write("How much do you want deposit to your account: ");

                if (double.TryParse(Console.ReadLine(), out double amount) && amount > 0)
                {
                    var userTemp = _storage.User;
                    var bankAccountTemp = _storage.BankAccount;
                    _bankAccountManagement.Deposit(ref userTemp, ref bankAccountTemp, amount);

                    _storage.User = userTemp;
                    _storage.BankAccount = bankAccountTemp;

                    break;
                }

                Console.WriteLine("Please write how much do you want deposit to your account");
                Console.ReadKey();
            }

            Console.ReadKey();
        }

        // Логіка зняття грошей з рахунку зберігання
        private void Withdraw(string accNum)
        {
            BankAccount account = _storage.BankAccount;

            while (true)
            {
                Console.Clear();

                Console.Write("How much do you want Withdraw from your account: ");

                if (double.TryParse(Console.ReadLine(), out double amount) && amount > 0)
                {
                    if (amount < account.Balance)
                    {
                        var userTemp = _storage.User;
                        var bankAccountTemp = _storage.BankAccount;
                        _bankAccountManagement.Withdraw(ref userTemp, ref bankAccountTemp, amount);

                        _storage.User = userTemp;
                        _storage.BankAccount = bankAccountTemp;
                        
                        break;
                    }

                    Console.WriteLine("There are not enough funds in your balance to withdraw");
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
        public void Transfer(BankAccount targetAccount, BankAccount bankAccount, double amount)
        {
            Console.Clear();
            string login = targetAccount.UserLogin;
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