using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vinnyk_Tomkiv_Zaliczenie.Models
{
    /// <Storage>
    /// A special class for working with a program containing BankAccount and User class objects.
    /// This class are created for better work with current bank account and current user
    /// </Storage>
    public class Storage
    {
        public BankAccount BankAccount { get; set; }
        public User User { get; set; }
    }
}
