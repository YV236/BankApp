using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Vinnyk_Tomkiv_Zaliczenie.Models;
using Vinnyk_Tomkiv_Zaliczenie.Services.CustomerManagements;

namespace Vinnyk_Tomkiv_Zaliczenie.Services.BankAccManagement
{
    /// <summary>
    /// This line defines the beginning of the BankAccountManagement class, which implements the IBankAccountManagement interface.
    /// The class has a private field _userManagement that is used to interact with user management methods.
    /// </summary>
    public class BankAccountManagement : IBankAccountManagement
    {
        private readonly IUserManagement _userManagement;

        public BankAccountManagement()
        {
            _userManagement = new UserManagement();
        }

        /// <summary>
        /// This RemoveFromUserBankAccList method removes a user's bank account by number and login.
        /// The method reads user data from a file, deserializes it into a list, finds the user by login and bank account by its number,
        /// removes the account from the list, and serializes the updated list.
        /// </summary>
        /// <param name="accountNum"></param>
        /// <param name="userLogin"></param>
        /// <returns User></returns>

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

        /// <summary>
        /// This private AddToUserBankAccList method adds a new bank account to the user's account list.
        /// The method reads user data from a file, deserializes it into a list, finds the user by login,
        /// sets the user's login to the new account, adds the account to the list, and serializes the updated list.
        /// </summary>
        /// <param name="bankAccount"></param>
        /// <param name="userLogin"></param>

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

        /// <summary>
        /// This CreateNewBankAccount method creates a new bank account for the user. 
        /// The method generates a unique account number, creates a new BankAccount object,
        /// adds it to the list of user accounts, and serializes the updated user data.
        /// </summary>
        /// <param name="user"></param>
        /// <returns BankAccount></returns>

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

        /// <summary>
        /// This private GenerateBankAccId method generates a unique number for a new bank account
        /// by checking whether the number already exists in the list of existing accounts.
        /// </summary>
        /// <param name="bankAccounts"></param>
        /// <returns AccountNumber></returns>

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

        /// <summary>
        /// This Deposit method performs the operation of replenishing the balance of the bank account.
        /// The method reads user data from a file, finds the user and his account by login and number,
        /// increases the account balance by the specified amount, and serializes the updated data.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="account"></param>
        /// <param name="amount"></param>

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

        /// <summary>
        /// This Withdraw method performs the operation of withdrawing funds from the bank account. 
        /// The method reads user data from a file, finds the user and his account by login and number,
        /// reduces the account balance by the specified amount, and serializes the updated data.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="account"></param>
        /// <param name="amount"></param>

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