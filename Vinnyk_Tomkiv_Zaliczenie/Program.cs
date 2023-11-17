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
    class Program
    {
        public static void Main(string[] args)
        {
            Storage storage = new Storage();

            if (!File.Exists(ConstVar.FileBankAccpath))
            {
                File.Create(ConstVar.FileBankAccpath).Close();
                File.WriteAllText(ConstVar.FileBankAccpath, "[]");
            }

            if (!File.Exists(ConstVar.FileUserpath))
            {
                File.Create(ConstVar.FileUserpath).Close();
                File.WriteAllText(ConstVar.FileUserpath, "[]");
            }
            MenuManagement menuManagment = new MenuManagement(storage);
            menuManagment.Menu();
        }
    }

}
