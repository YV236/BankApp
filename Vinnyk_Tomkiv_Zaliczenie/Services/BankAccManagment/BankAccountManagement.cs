using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Vinnyk_Tomkiv_Zaliczenie.Models;
using Vinnyk_Tomkiv_Zaliczenie.Services.CustomerManagements;

namespace Vinnyk_Tomkiv_Zaliczenie.Services.BankAccManagement
{
    public class BankAccountManagement : IBankAccountManagement
    {
        private readonly IUserManagement _userManagement;

        public BankAccountManagement()
        {
            _userManagement = new UserManagement();
        }

        public User RemoveFromUserBankAccList(string accountNum, string userLogin)
        {
            string userListStr = File.ReadAllText(ConstVar.FileUserpath);

            var userList = JsonConvert.DeserializeObject<List<User>>(userListStr);

            User user = userList.FirstOrDefault(u => u.Login == userLogin);
            BankAccount bankAccount = user.Accounts.FirstOrDefault(b => b.AccountNumber == accountNum);

            if (user != null)
            {
                user.Accounts.Remove(bankAccount);
                File.WriteAllText(ConstVar.FileUserpath, JsonConvert.SerializeObject(userList));
            }
            else
            {
                Console.WriteLine("Bank Account not found with the specified number.");
            }

            return user;
        }

        private void AddToUserBankAccList(BankAccount bankAccount, string userLogin)
        {
            string userListStr = File.ReadAllText(ConstVar.FileUserpath);

            var userList = JsonConvert.DeserializeObject<List<User>>(userListStr);

            User user = userList.FirstOrDefault(u => u.Login == userLogin);

            if (user != null)
            {
                bankAccount.UserLogin = userLogin;
                user.Accounts.Add(bankAccount);
                File.WriteAllText(ConstVar.FileUserpath, JsonConvert.SerializeObject(userList));
            }
            else
            {
                Console.WriteLine("User not found with the specified Login.");
            }
        }

        public BankAccount CreateNewBankAccount(ref User user)
        {
            var bankAccount = new BankAccount
            {
                AccountNumber = GenerateBankAccId(user.Accounts),
                Balance = 0,
                UserLogin = user.Login
            };

            AddToUserBankAccList(bankAccount, user.Login);

            user = _userManagement.GetUserInfo(user.Login);

            return bankAccount;
        }

        private string GenerateBankAccId(List<BankAccount> bankAccounts)
        {
            Random random = new Random();

            int accNumber = 0;
            bool exit = true;

            while (exit)
            {
                accNumber = random.Next(100000000, 1000000001);

                var number = accNumber;
                if (bankAccounts.All(x => x.AccountNumber != number.ToString()))
                {
                    exit = false;
                }
            }

            return accNumber.ToString();
        }

        public void Deposit(ref User user, ref BankAccount account, double amount)
        {
            string userListStr = File.ReadAllText(ConstVar.FileUserpath);

            var userList = JsonConvert.DeserializeObject<List<User>>(userListStr);

            var userLogin = user.Login;

            User userDb = userList.FirstOrDefault(x => x.Login == userLogin);

            var accountNumber = account.AccountNumber;
            var selectedAccount = userDb?.Accounts.FirstOrDefault(x => x.AccountNumber == accountNumber);

            if (selectedAccount == null)
            {
                return;
            }

            selectedAccount.Balance += amount;

            File.WriteAllText(ConstVar.FileUserpath, JsonConvert.SerializeObject(userList));

            user = userDb;
            account = selectedAccount;

        }

        public void Withdraw(ref User user, ref BankAccount account, double amount)
        {
            string userListStr = File.ReadAllText(ConstVar.FileUserpath);

            var userList = JsonConvert.DeserializeObject<List<User>>(userListStr);

            var userLogin = user.Login;

            User userDb = userList.FirstOrDefault(x => x.Login == userLogin);

            var accountNumber = account.AccountNumber;
            BankAccount selectedAccount = userDb?.Accounts.FirstOrDefault(x => x.AccountNumber == accountNumber);
            if (selectedAccount == null)
                return;

            selectedAccount.Balance -= amount;

            File.WriteAllText(ConstVar.FileUserpath, JsonConvert.SerializeObject(userList));

            user = userDb;
            account = selectedAccount;

        }

        public virtual void Transfer(BankAccount targetAccount, BankAccount bankAccount, double amount)
        {
        }
    }
}