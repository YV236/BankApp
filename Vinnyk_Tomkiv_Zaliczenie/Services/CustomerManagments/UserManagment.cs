using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vinnyk_Tomkiv_Zaliczenie.Models;
using Vinnyk_Tomkiv_Zaliczenie.Services.MenuOperation;

namespace Vinnyk_Tomkiv_Zaliczenie.Services.CustomerManagments
{
    public class UserManagment : IUserManagement
    {
        public void AddUser(User customer)
        {
            string userListStr = File.ReadAllText(ConstVar.FileUserpath);

            var userList = JsonConvert.DeserializeObject<List<User>>(userListStr);

            userList.Add(customer);

            File.WriteAllText(ConstVar.FileUserpath, JsonConvert.SerializeObject(userList));
        }

        public void GivePropose()
        {
            Console.Clear();
            MenuManagment menuManagment = new MenuManagment();
            int choice = 0;
            Console.WriteLine("If you don't remember your login, or password.");
            Console.WriteLine("1.Try again");
            Console.WriteLine("2.Create a new user");
            Console.WriteLine("3.Exit to main menu");

            choice=int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    menuManagment.WriteUserLog();
                    break;

                case 2:
                    menuManagment.RenderRegisterMenu();
                    break;

                case 3:
                    menuManagment.Menu();
                    break;
                default:
                    Console.WriteLine("Choose the option 1-3");
                    break;
            }
        }

        public bool IsPasswordRight(string login, string password)
        {
            string userListStr = File.ReadAllText(ConstVar.FileUserpath);

            var userList = JsonConvert.DeserializeObject<List<User>>(userListStr);

            var user = userList.FirstOrDefault(x => x.Login == login);

            if (user != null)
            {
                return user.Password == password;
            }

            return false;
        }

        public void GetUserInfo(string Login)
        {
            Console.WriteLine("You loginned to your account your welcome: " + Login);
            Console.ReadKey();
        }

        public bool IsUserExist(string newLogin)
        {
            string userListStr = File.ReadAllText(ConstVar.FileUserpath);

            var userList = JsonConvert.DeserializeObject<List<User>>(userListStr);

            return userList.Any(x => x.Login == newLogin);
        }

        public void RemoveUser(User customer)
        {
            throw new NotImplementedException();
        }
    }
}
