using System;
using Vinnyk_Tomkiv_Zaliczenie.Models;
using Vinnyk_Tomkiv_Zaliczenie.Services.BankAccManagement;
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

            Console.WriteLine("What would you like to do\n");
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
                    Withdraw();
                    break;

                case 3:
                    Transfer();
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

                    Console.WriteLine(
                        $"Deposited {amount} PLN to account {bankAccountTemp.AccountNumber} . New balance:  {bankAccountTemp.Balance} PLN");

                    break;
                }

                Console.WriteLine("Please write how much do you want deposit to your account");
                Console.ReadKey();
            }

            Console.ReadKey();
        }

        // Логіка зняття грошей з рахунку зберігання
        private void Withdraw()
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

                        Console.WriteLine(
                            $"Withdrawed {amount} PLN from account {bankAccountTemp.AccountNumber}. New balance: {bankAccountTemp.Balance} PLN");

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
        public void Transfer()
        {
            Console.Clear();
            Console.WriteLine("Please, write the user login");
            string login = Console.ReadLine();
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

           while(true)
            {
                Console.Clear();

                if (index > 0 && index <= user.Accounts.Count)
                {
                    var account = user.Accounts[index - 1];

                    Console.Write("How much you want to transfer: ");

                    if (double.TryParse(Console.ReadLine(), out double amount) && amount > 0)
                    {
                        if (amount < _storage.BankAccount.Balance)
                        {
                            _bankAccountManagement.Deposit(ref user,ref account, amount);

                            var userTemp = _storage.User;
                            var bankAccountTemp = _storage.BankAccount;
                            _bankAccountManagement.Withdraw(ref userTemp, ref bankAccountTemp, amount);

                            _storage.User = userTemp;
                            _storage.BankAccount = bankAccountTemp;

                            Console.WriteLine(
                                $"Transfered {amount} from your {bankAccountTemp.AccountNumber} account to {user.Login}" +
                                $" {account.AccountNumber} account number");

                            break;
                        }

                        Console.WriteLine("There are not enough funds in your balance to withdraw");
                    }

                }
                else
                {
                    Console.WriteLine("Invalid account number selection");
                }
            }

            Console.ReadKey();
        }
    }
}