using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vinnyk_Tomkiv_Zaliczenie;

namespace Vinnyk_Tomkiv_Zaliczenie.Models
{
    // A class for a bank client
    public class User
    {
        // User login which he creates by his own, which is unique
        public string Login { get; set; }
        //User password which he creates by his own
        public string Password { get; set; }
        //The list of BankAccount class.
        public List<BankAccount> Accounts { get; set; }

        //This constructor of the User class initializes a new instance of the class by setting the Accounts property to an initial empty list.

        public User()
        {
            Accounts = new List<BankAccount>();
        }
    }

}
