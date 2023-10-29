using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vinnyk_Tomkiv_Zaliczenie;

namespace Vinnyk_Tomkiv_Zaliczenie
{
    // Базовий клас банківського рахунку
    public abstract class BankAccount
    {
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }

    }

}
