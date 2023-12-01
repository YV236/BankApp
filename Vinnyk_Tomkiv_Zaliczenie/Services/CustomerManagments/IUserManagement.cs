using System;
using Vinnyk_Tomkiv_Zaliczenie;
using Vinnyk_Tomkiv_Zaliczenie.Models;

namespace Vinnyk_Tomkiv_Zaliczenie.Services.CustomerManagements
{
    // Customer management interface
    public interface IUserManagement
    {
        User RemoveUser(string login);
        bool IsPasswordRight(string login, string password);
        void AddUser(User customer);
        User GetUserInfo(string login);
        bool IsUserExist(string newLogin);
    }

}
