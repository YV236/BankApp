using Vinnyk_Tomkiv_Zaliczenie.Models;

namespace Vinnyk_Tomkiv_Zaliczenie.Services.BankAccManagement
{
    public interface IBankAccountManagement
    {
        BankAccount CreateNewBankAccount(ref User user);
        User RemoveFromUserBankAccList(string accountNum, string userLogin);
        void Deposit(ref User user, ref BankAccount account, double amount);
        void Withdraw(ref User user, ref BankAccount account, double amount);
    }
}