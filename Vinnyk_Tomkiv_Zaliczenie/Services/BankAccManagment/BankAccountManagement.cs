using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vinnyk_Tomkiv_Zaliczenie.Models;

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
        public BankAccount GetBankAccInfo(string Id)
        {
            string bankAccStr = File.ReadAllText(ConstVar.FileBankAccpath);
            var bankList = JsonConvert.DeserializeObject<List<BankAccount>>(bankAccStr);

            BankAccount bank = bankList.FirstOrDefault(x => x.Id == Id);

            return bank;
        }

        public void AddBankAcc(BankAccount bankAccount, string login)
        {
            string bankAccStr = File.ReadAllText(ConstVar.FileBankAccpath);

            var bankList = JsonConvert.DeserializeObject<List<BankAccount>>(bankAccStr);

            bankAccount.Id = login;

            bankList.Add(bankAccount);

            File.WriteAllText(ConstVar.FileBankAccpath, JsonConvert.SerializeObject(bankList));

            AddToUserBankAccList(bankAccount, bankAccount.Id);
        }

        public void AddToUserBankAccList(BankAccount bankAccount, string Id)
        {
            string userListStr = File.ReadAllText(ConstVar.FileUserpath);

            var userList = JsonConvert.DeserializeObject<List<User>>(userListStr);

            User user = userList.FirstOrDefault(u => u.Login == Id);

            if (user != null)
            {
                bankAccount.Id = Id;
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

        public virtual void Deposit(double amount)
        {


        }

        public virtual void Withdraw(double amount)
        {


        }
        public virtual void Transfer(BankAccount targetAccount, double amount)
        {


        }
    }
}
