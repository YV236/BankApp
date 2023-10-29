using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vinnyk_Tomkiv_Project;

namespace Vinnyk_Tomkiv_Project
{

    // Інтерфейс для операцій на рахунках
    public interface IAccountOperations
    {
        void Deposit(double amount);
        void Withdraw(double amount);
        void Transfer(BankAccount targetAccount, double amount);
    }

    // Інтерфейс для керування клієнтами
    public interface ICustomerManagement
    {
        void AddAccount(string filePath, string filepath1);
        void RemoveAccount(BankAccount account);
        string GetCustomerInfo();
    }

    // Інтерфейс для транзакцій
    public interface ITransaction
    {
        void ProcessTransaction();
    }
}
