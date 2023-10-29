using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vinnyk_Tomkiv_Zaliczenie;

namespace Vinnyk_Tomkiv_Zaliczenie
{
    // Клас для клієнта банку
    public class Customer
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string ContactInfo { get; set; }
        public List<BankAccount> Accounts { get; set; }
    }

}
