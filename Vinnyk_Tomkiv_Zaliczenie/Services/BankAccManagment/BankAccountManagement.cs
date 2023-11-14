using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vinnyk_Tomkiv_Zaliczenie.Models;
using Vinnyk_Tomkiv_Zaliczenie.Services.CustomerManagements;

namespace Vinnyk_Tomkiv_Zaliczenie.Services.BankAccManagement
{
    public class BankAccountManagement : IBankAccountManagement
    {
        public bool IsBankAccExist(string accNumber)
        {
            string bankAccStr = File.ReadAllText(ConstVar.FileBankAccpath);

            var bankList = JsonConvert.DeserializeObject<List<BankAccount>>(bankAccStr);

            return bankList.Any(x => x.AccountNumber == accNumber);
        }

        public BankAccount GetBankAccInfo(string Id , int accNum)
        {
            string bankAccStr = File.ReadAllText(ConstVar.FileBankAccpath);
            var bankList = JsonConvert.DeserializeObject<List<BankAccount>>(bankAccStr);

            BankAccount bank = bankList.FirstOrDefault(x => x.Id == Id && x.AccountNumber == accNum.ToString());

            return bank;
        }

        //public void AddBankAcc(BankAccount bankAccount, string login, int accNum)
        //{
        //    string bankAccStr = File.ReadAllText(ConstVar.FileBankAccpath);

        //    var bankList = JsonConvert.DeserializeObject<List<BankAccount>>(bankAccStr);

        //    bankAccount.Id = login;
        //    bankAccount.AccountNumber = accNum.ToString();

        //    bankList.Add(bankAccount);

        //    File.WriteAllText(ConstVar.FileBankAccpath, JsonConvert.SerializeObject(bankList));
        //}

        public void AddBankAcc(BankAccount bankAccount, string login)
        {
            string bankAccStr = File.ReadAllText(ConstVar.FileBankAccpath);

            var bankList = JsonConvert.DeserializeObject<List<BankAccount>>(bankAccStr);

            bankAccount.Id = login;

            bankList.Add(bankAccount);

            File.WriteAllText(ConstVar.FileBankAccpath, JsonConvert.SerializeObject(bankList));

            AddToUserBankAccList(bankAccount, bankAccount.Id);
        }

        //public void AddToUserBankAccList(BankAccount bankAccount, string Id)
        //{
        //    string userListStr = File.ReadAllText(ConstVar.FileUserpath);

        //    var userList = JsonConvert.DeserializeObject<List<User>>(userListStr);

        //    User user = userList.FirstOrDefault(u => u.Login == Id);

        //    if (user != null)
        //    {
        //        bankAccount.Id = Id;
        //        user.Accounts.Add(bankAccount);

        //        File.WriteAllText(ConstVar.FileUserpath, JsonConvert.SerializeObject(userList));
        //    }
        //    else
        //    {
        //        Console.WriteLine("User not found with the specified Login.");
        //    }
        //    int index = 0;
        //    for(index = 0; index < user.Accounts.Count;)
        //    {
        //        index++;
        //    }

        //    AddBankAcc(bankAccount, bankAccount.Id,index);
        //}

        public void AddToUserBankAccList(BankAccount bankAccount, string login)
        {
            string userListStr = File.ReadAllText(ConstVar.FileUserpath);

            var userList = JsonConvert.DeserializeObject<List<User>>(userListStr);

            User user = userList.FirstOrDefault(u => u.Login == login);

            if (user != null)
            {
                bankAccount.Id = login;
                user.Accounts.Add(bankAccount);

                File.WriteAllText(ConstVar.FileUserpath, JsonConvert.SerializeObject(userList));
            }
            else
            {
                Console.WriteLine("User not found with the specified Login.");
            }

        }

        public BankAccount BankAccReg()
        {
            Random random = new Random();

            int accNumber = 0;
            bool exit = true;

            while (exit)
            {
                accNumber = random.Next(100000000, 1000000001);

                if (!IsBankAccExist(accNumber.ToString()))
                {
                    exit = false;
                }
            }

            BankAccount bankAccount = new BankAccount { AccountNumber = accNumber.ToString(), Balance = 0 };
            return bankAccount;
        }

        //public void ChangeBankAccount(int accnum, string login)
        //{

        //    BankAccount bankAccount = GetBankAccInfo(login,accnum);

        //    bankAccount.BankAccountIndex = accnum;
            
        //}

        public void DepositToBankAccList(string login, int accNum, double amount)
        {
            string userListStr = File.ReadAllText(ConstVar.FileUserpath);

            var userList = JsonConvert.DeserializeObject<List<User>>(userListStr);

            User user = userList.FirstOrDefault(x => x.Login == login);

            if (user != null)
            {
                BankAccount selectedAccount = user.Accounts.FirstOrDefault(x => x.AccountNumber == accNum.ToString());
                selectedAccount.Balance += amount;

                File.WriteAllText(ConstVar.FileUserpath, JsonConvert.SerializeObject(userList));

                Console.WriteLine($"Deposited {amount} PLN to account {selectedAccount.AccountNumber}. New balance: {selectedAccount.Balance} PLN");
            }
        }

        public virtual void Deposit(string login, int accNum, double amount) 
        {
            string bankAccStr = File.ReadAllText(ConstVar.FileBankAccpath);
            var bankList = JsonConvert.DeserializeObject<List<BankAccount>>(bankAccStr);

            BankAccount bank = bankList.FirstOrDefault(x => x.Id == login && x.AccountNumber == accNum.ToString());

            bank.Balance += amount;

            File.WriteAllText(ConstVar.FileBankAccpath, JsonConvert.SerializeObject(bankList));

            DepositToBankAccList(login, accNum, amount);
        }

        public void WithdrawFromBankAccList(string login, int accNum, double amount)
        {
            string userListStr = File.ReadAllText(ConstVar.FileUserpath);

            var userList = JsonConvert.DeserializeObject<List<User>>(userListStr);

            User user = userList.FirstOrDefault(x => x.Login == login);

            if (user != null)
            {
                BankAccount selectedAccount = user.Accounts.FirstOrDefault(x => x.AccountNumber == accNum.ToString());
                selectedAccount.Balance -= amount;

                File.WriteAllText(ConstVar.FileUserpath, JsonConvert.SerializeObject(userList));

                Console.WriteLine($"Withdrawed {amount} PLN from account {selectedAccount.AccountNumber}. New balance: {selectedAccount.Balance} PLN");
            }
        }

        public virtual void Withdraw(string login, int accNum, double amount)
        {
            string bankAccStr = File.ReadAllText(ConstVar.FileBankAccpath);
            var bankList = JsonConvert.DeserializeObject<List<BankAccount>>(bankAccStr);

            BankAccount bank = bankList.FirstOrDefault(x => x.Id == login && x.AccountNumber == accNum.ToString());

            bank.Balance -= amount;

            File.WriteAllText(ConstVar.FileBankAccpath, JsonConvert.SerializeObject(bankList));

            WithdrawFromBankAccList(login, accNum, amount);
        }

        public virtual void Transfer(BankAccount targetAccount, double amount) { }
    }
}
