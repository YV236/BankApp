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
            // Creating an object of the Storage class for further work with the user and saving data.
            Storage storage = new Storage();

            // Checking the existence of a user storage file.
            // If the file does not exist, an empty file is created at the specified path
            if (!File.Exists(ConstVar.FileUserpath))
            {
                File.Create(ConstVar.FileUserpath).Close();
                // Recording [] for further work with the JSON file.
                File.WriteAllText(ConstVar.FileUserpath, "[]");
            }
            // Creating an instance of the MenuScreen class by passing the Storage object to the constructor.
            var menuManagement = new MenuScreen(storage);
            // Call the Menu method to start the program execution.
            menuManagement.Menu();
        }
    }

}
