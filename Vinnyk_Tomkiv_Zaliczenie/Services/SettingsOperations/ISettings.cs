using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vinnyk_Tomkiv_Zaliczenie.Services.OptionOperations
{
    public interface ISettings
    {
        void SettingsMenu(string login);
        void ShowBankAccounts(string login);
    }
}
