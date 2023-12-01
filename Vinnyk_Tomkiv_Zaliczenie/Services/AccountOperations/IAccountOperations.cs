using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vinnyk_Tomkiv_Zaliczenie;
using Vinnyk_Tomkiv_Zaliczenie.Models;

namespace Vinnyk_Tomkiv_Zaliczenie
{
    // Interface for operations on accounts
    public interface IAccountOperations
    {
        void Deposit(double amount);
        void Withdraw(double amount);
        void Transfer(BankAccount targetAccount, double amount);
    }

}
