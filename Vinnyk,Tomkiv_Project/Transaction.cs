using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vinnyk_Tomkiv_Project;

namespace Vinnyk_Tomkiv_Project
{
    // Клас для представлення транзакцій

    public class Transaction : ITransaction
    {
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

        public void ProcessTransaction()
        {
            // Логіка виконання транзакції
        }
    }
}
