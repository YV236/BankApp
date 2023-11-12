using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vinnyk_Tomkiv_Zaliczenie;

namespace Vinnyk_Tomkiv_Zaliczenie.Services.MenuOperation
{
    public interface IMenu
    {
        void Menu();
        void UserLoginedMenu(string login, int index);
    }
}
