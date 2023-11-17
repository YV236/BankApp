using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vinnyk_Tomkiv_Zaliczenie;
using Vinnyk_Tomkiv_Zaliczenie.Services.MenuOperation;
using Vinnyk_Tomkiv_Zaliczenie.Models;

namespace Vinnyk_Tomkiv_Zaliczenie
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            Storage storage = new Storage();

            if (!File.Exists(ConstVar.FileUserpath))
            {
                File.Create(ConstVar.FileUserpath).Close();
                File.WriteAllText(ConstVar.FileUserpath, "[]");
            }
            
            var menuManagement = new MenuScreen(storage);
            menuManagement.Menu();
        }
    }

}
