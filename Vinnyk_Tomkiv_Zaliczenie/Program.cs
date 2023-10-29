using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vinnyk_Tomkiv_Zaliczenie;
using Vinnyk_Tomkiv_Zaliczenie.Services.MenuOperation;

namespace Vinnyk_Tomkiv_Zaliczenie
{
    class Program
    {
        static void Main(string[] args)
        {
            MenuManagment menuManagment = new MenuManagment();
            menuManagment.Menu();
        }
    }

}
