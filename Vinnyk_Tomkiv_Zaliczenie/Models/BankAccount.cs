using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vinnyk_Tomkiv_Zaliczenie;

namespace Vinnyk_Tomkiv_Zaliczenie.Models
{
    // Basic bank account class
    public class BankAccount
    {
        // Using the user's login as an ID (User Login is unique)
        public string UserLogin { get; set; }
        // Bank account number which is unique
        public string AccountNumber { get; set; }
        // Bank account balance
        public double Balance { get; set; }

    }

}
