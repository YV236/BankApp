using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vinnyk_Tomkiv_Zaliczenie;
using Vinnyk_Tomkiv_Zaliczenie.Models;
using Vinnyk_Tomkiv_Zaliczenie.Services.BankAccManagement;

namespace Vinnyk_Tomkiv_Zaliczenie
{
    // Клас для рахунку зберігання
    public class BasicAccountOperations : BankAccountManagement
    {
        // Реалізація методів IAccountOperations для рахунку зберігання

        // Логіка внесення грошей на рахунок зберігання
        public override void Deposit(double amount)
        {


        }

        // Логіка зняття грошей з рахунку зберігання
        public override void Withdraw(double amount)
        {


        }

        // Логіка переказу з рахунку зберігання на інший рахунок
        public override void Transfer(BankAccount targetAccount, double amount)
        {


        }
    }

}
