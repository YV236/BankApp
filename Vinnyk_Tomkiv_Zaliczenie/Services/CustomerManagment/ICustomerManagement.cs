namespace Vinnyk_Tomkiv_Zaliczenie
{
    // Інтерфейс для керування клієнтами
    public interface ICustomerManagement
    {
        void AddAccount(BankAccount account);
        void RemoveAccount(BankAccount account);
        string GetCustomerInfo();
    }

}
