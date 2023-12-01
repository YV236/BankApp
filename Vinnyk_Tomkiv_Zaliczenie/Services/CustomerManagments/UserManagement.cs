using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vinnyk_Tomkiv_Zaliczenie.Models;
using Vinnyk_Tomkiv_Zaliczenie.Services.MenuOperation;

namespace Vinnyk_Tomkiv_Zaliczenie.Services.CustomerManagements
{
    /// <UserManagement>
    /// This class inherits from the IUserManagement interface.
    /// </UserManagement>
    public class UserManagement : IUserManagement
    {
        /// <summary>
        /// This RemoveUser method removes a user by their login.
        /// First, it reads user data from a file, deserializes it into a list of User objects,
        /// finds the user by login, removes it from the list, serializes the updated list, and writes it back to the file.
        /// It then returns null to indicate that the user has been deleted.
        /// </summary>
        /// <param name="login"></param>
        /// <returns User=null></returns>        

        public User RemoveUser(string login)
        {
            string userListStr = File.ReadAllText(ConstVar.FileUserpath);

            var userList = JsonConvert.DeserializeObject<List<User>>(userListStr);

            User user = userList.FirstOrDefault(u => u.Login == login);
            userList.Remove(user);

            File.WriteAllText(ConstVar.FileUserpath, JsonConvert.SerializeObject(userList));

            return user = null ;
        }

        /// <summary>
        /// This AddUser method adds a new user. 
        /// It also first reads user data from a file, deserializes it into a list, 
        /// adds a new user to the list, serializes the updated list, 
        /// and writes it back to the file.
        /// </summary>
        /// <param name="customer"></param>

        public void AddUser(User customer)
        {
            string userListStr = File.ReadAllText(ConstVar.FileUserpath);

            var userList = JsonConvert.DeserializeObject<List<User>>(userListStr);

            userList.Add(customer);

            File.WriteAllText(ConstVar.FileUserpath, JsonConvert.SerializeObject(userList));
        }

        /// <summary>
        /// This IsPasswordRight method checks whether the password for the specified login is correct. 
        /// It reads user data from a file, deserializes it into a list, finds the user by login and checks
        /// if their password matches the passed parameter.
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns bool="true/false"></returns>

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

        /// <summary>
        /// This GetUserInfo method returns information about a user by their login.
        /// It reads user data from a file, deserializes it into a list, and returns the user with the specified login.
        /// </summary>
        /// <param name="login"></param>
        /// <returns User></returns>

        public User GetUserInfo(string login)
        {
            string userListStr = File.ReadAllText(ConstVar.FileUserpath);
            var userList = JsonConvert.DeserializeObject<List<User>>(userListStr);

            User user = userList.FirstOrDefault(x => x.Login == login);

            return user;
        }

        /// <summary>
        /// This IsUserExist method checks whether a user with the specified login exists.
        /// It reads user data from a file, deserializes it into a list, and checks if there is a user with the specified login.
        /// If such a user exists, returns true, otherwise - false.
        /// </summary>
        /// <param name="newLogin"></param>
        /// <returns bool="true/false"></returns>

        public bool IsUserExist(string newLogin)
        {
            string userListStr = File.ReadAllText(ConstVar.FileUserpath);

            var userList = JsonConvert.DeserializeObject<List<User>>(userListStr);

            return userList.Any(x => x.Login == newLogin);
        }

    }
}
