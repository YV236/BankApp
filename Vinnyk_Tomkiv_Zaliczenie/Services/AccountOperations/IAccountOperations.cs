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
    // Інтерфейс для операцій на рахунках
    public interface IAccountOperations
    {
        void Deposit(decimal amount);
        void Withdraw(decimal amount);
        void Transfer(BankAccount targetAccount, decimal amount);
    }

}
