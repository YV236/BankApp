using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vinnyk_Tomkiv_Zaliczenie;

namespace Vinnyk_Tomkiv_Zaliczenie.Models
{
    // Базовий клас банківського рахунку
    public class BankAccount
    {
        public string Id { get; set; }
        public string AccountNumber { get; set; }
        public double Balance { get; set; }
        public int BankAccountIndex { get; set; }

    }

}
