using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vinnyk_Tomkiv_Project;

namespace Vinnyk_Tomkiv_Project
{
    // Клас для клієнта банку

    public class Customer : ICustomerManagement
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string TestP {  get; set; }
        public string Address { get; set; }
        public string ContactInfo { get; set; }

        public void AddAccount(string filePath, string filePath1)
        {
            if (File.Exists(filePath))
            {
                string[] existingLogin = File.ReadAllLines(filePath);
                bool log = true;
                bool pass = true;

                while(log)
                {
                    Console.Clear();
                    Console.Write("Create your login please:");
                    Login = Console.ReadLine();
                    bool loginExists = false;
                    foreach (string line in existingLogin)
                    {
                        string[] parts = line.Split(';');
                        if (parts.Length == 2 && Login == parts[0])
                        {
                            Console.WriteLine("Sorry, but user with this nick already exist");
                            Console.ReadKey();
                            loginExists = true;
                            break;
                        }
                    }

                    if (!loginExists)
                    {
                        log = false;
                    }
                }
                Console.Write("Create your password please:");
                Password = Console.ReadLine();

                while (pass)
                {
                    Console.Write("Repeat your password please:");
                    TestP = Console.ReadLine();
                    if (TestP == Password)
                    {
                        Console.WriteLine("Your account successfully created");
                        pass = false;
                    }
                }
                try
                {
                    BankAccount bankAccount = new BankAccount();
                    string existingContent = File.ReadAllText(filePath);

                    string modifiedContent = existingContent + "\n" + Login + ";" + Password;

                    File.WriteAllText(filePath, modifiedContent);
                    bankAccount.createBankAccount(filePath1, Login);

                }
                catch (IOException ex)
                {
                    Console.WriteLine("Sorry something went wrong please try again later");
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Sorry something went wrong please try again later");
            }

            Console.WriteLine("Press the key to continue");
            Console.ReadKey();
        }


        public void LoginAccount(string filePath)
        {
            string AdminName = "Admin";
            string AdminPassword = "12345";
            AdminMode();
        }

        private void AdminMode()
        {

        }

        public void RemoveAccount(BankAccount account)
        {

        }

        public string GetCustomerInfo()
        {
            // Логіка отримання інформації про клієнта
            return $"Customer Name: {Login}, Address: {Address}, Contact Info: {ContactInfo}";
        }
    }
}
