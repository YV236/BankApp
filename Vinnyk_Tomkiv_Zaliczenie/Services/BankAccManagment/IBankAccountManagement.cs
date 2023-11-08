using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vinnyk_Tomkiv_Zaliczenie.Models;


namespace Vinnyk_Tomkiv_Zaliczenie.Services.BankAccManagement
{
    public interface IBankAccountManagement
    {
        bool IsBankAccExist(string accNumber);
        void AddToUserBankAccList(BankAccount bankAccount, string Id);
        BankAccount BankAccReg();
        void AddBankAcc(BankAccount bankAccount, string login, int Id);
        BankAccount GetBankAccInfo(string Id, int index);
        void ChangeBankAccount(int accnum, string login);
    }
}
