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
            MenuManagment menuManagment = new MenuManagment();
            menuManagment.Menu();
        }
    }

}
