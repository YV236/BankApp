using System;
using Vinnyk_Tomkiv_Zaliczenie;
using Vinnyk_Tomkiv_Zaliczenie.Models;

namespace Vinnyk_Tomkiv_Zaliczenie.Services.CustomerManagments
{
    // Інтерфейс для керування клієнтами
    public interface IUserManagement
    {
        void UserLoginedMenu(string login);
        void GivePropose();
        bool IsPasswordRight(string login, string password);
        void AddUser(User customer);
        void RemoveUser(User customer);
        void GetUserInfo(string login);
        bool IsUserExist(string newLogin);
    }

}
