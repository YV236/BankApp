using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vinnyk_Tomkiv_Zaliczenie.Models;
using Vinnyk_Tomkiv_Zaliczenie.Services.MenuOperation;
using Vinnyk_Tomkiv_Zaliczenie.Services.OptionOperations;

namespace Vinnyk_Tomkiv_Zaliczenie.Services.CustomerManagements
{
    public class UserManagement : IUserManagement
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
            MenuManagement menuManagment = new MenuManagement();
            int choice = 0;
            bool exit = true;

           while(exit)
            {
                Console.Clear();

                Console.WriteLine("If you don't remember your login, or password.");
                Console.WriteLine("1.Try again");
                Console.WriteLine("2.Create a new user");
                Console.WriteLine("3.Exit to main menu");

                choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        menuManagment.WriteUserLog();
                        exit = false;
                        break;

                    case 2:
                        menuManagment.RenderRegisterMenu();
                        exit = false;
                        break;

                    case 3:
                        exit = false;
                        break;
                    default:
                        Console.WriteLine("Choose the option 1-3");
                        break;
                }
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

        public User GetUserInfo(string login)
        {
            string userListStr = File.ReadAllText(ConstVar.FileUserpath);
            var userList = JsonConvert.DeserializeObject<List<User>>(userListStr);

            User user = userList.FirstOrDefault(x => x.Login == login);

            return user;
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
