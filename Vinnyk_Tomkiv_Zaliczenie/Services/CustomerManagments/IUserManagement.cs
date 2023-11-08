using System;
using Vinnyk_Tomkiv_Zaliczenie;
using Vinnyk_Tomkiv_Zaliczenie.Models;

namespace Vinnyk_Tomkiv_Zaliczenie.Services.CustomerManagements
{
    // Інтерфейс для керування клієнтами
    public interface IUserManagement
    {
        bool IsPasswordRight(string login, string password);
        void AddUser(User customer);
        void RemoveUser(User customer);
        User GetUserInfo(string login);
        bool IsUserExist(string newLogin);
    }

}
