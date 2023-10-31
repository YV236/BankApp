using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vinnyk_Tomkiv_Zaliczenie.Models;

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




        //public bool IsPasswordRight(string login, string password)
        //{
        //    string userListStr = File.ReadAllText(ConstVar.FileUserpath);

        //    var userList = JsonConvert.DeserializeObject<List<User>>(userListStr);

        //    userList.Any(x => x.Login == login);
        //    return
        //}

        public string GetUserInfo()
        {
            throw new NotImplementedException();
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
