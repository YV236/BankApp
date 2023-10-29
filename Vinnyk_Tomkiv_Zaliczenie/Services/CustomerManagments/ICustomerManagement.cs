using System;
using Vinnyk_Tomkiv_Zaliczenie;
using Vinnyk_Tomkiv_Zaliczenie.Models;

namespace Vinnyk_Tomkiv_Zaliczenie.Services.CustomerManagments
{
    // Інтерфейс для керування клієнтами
    public interface ICustomerManagement
    {
        void AddUser(Customer customer);
        void RemoveUser(Customer customer);
        string GetCustomerInfo();
        bool IsUserExist(string newLogin);
    }

}
