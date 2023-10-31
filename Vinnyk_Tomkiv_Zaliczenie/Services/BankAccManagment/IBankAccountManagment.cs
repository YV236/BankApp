using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vinnyk_Tomkiv_Zaliczenie.Models;


namespace Vinnyk_Tomkiv_Zaliczenie.Services.BankAccManagment
{
    public interface IBankAccountManagment
    {
        bool IsBankAccExist(string accNumber);
        BankAccount BankAccReg();
        void AddBankAcc(BankAccount bankAccount,string login);
    }
}
