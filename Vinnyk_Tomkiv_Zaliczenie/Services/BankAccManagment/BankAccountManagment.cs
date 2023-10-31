using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vinnyk_Tomkiv_Zaliczenie.Models;

namespace Vinnyk_Tomkiv_Zaliczenie.Services.BankAccManagment
{
    public class BankAccountManagment : IBankAccountManagment
    {
        public bool IsBankAccExist(string accNumber)
        {
            string bankAccStr = File.ReadAllText(ConstVar.FileBankAccpath);

            var bankList = JsonConvert.DeserializeObject<List<BankAccount>>(bankAccStr);

            return bankList.Any(x => x.AccountNumber == accNumber);
        }

        public void AddBankAcc(BankAccount bankAccount, string login)
        {
            string bankAccStr = File.ReadAllText(ConstVar.FileBankAccpath);

            var bankList = JsonConvert.DeserializeObject<List<BankAccount>>(bankAccStr);

            bankAccount.Id = login;

            bankList.Add(bankAccount);

            File.WriteAllText(ConstVar.FileBankAccpath, JsonConvert.SerializeObject(bankList));
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
    }
}
