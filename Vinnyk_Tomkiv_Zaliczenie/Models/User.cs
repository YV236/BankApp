using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vinnyk_Tomkiv_Zaliczenie;

namespace Vinnyk_Tomkiv_Zaliczenie.Models
{
    // Клас для клієнта банку
    public class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public List<BankAccount> Accounts { get; set; }

        public User()
        {
            Accounts = new List<BankAccount>();
        }
    }

}
