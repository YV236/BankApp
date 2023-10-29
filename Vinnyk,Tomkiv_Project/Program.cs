using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Vinnyk_Tomkiv_Project;

namespace Vinnyk_Tomkiv_Project
{
    class Program
    {        
        static void Main(string[] args)
        {
            string filePath = @"BankAccountData.txt";
            string filePath1 = @"BankQuantityData.txt";
            Menu(filePath, filePath1);
        }

        static void Menu(string filePath, string filePath1)
        {
            Customer customer = new Customer();
            int choice;
            bool exit = true;

            while (exit)
            {
                Console.Clear();
                Console.WriteLine("Welcome to polyBank Application\nPlease enter your choice");
                Console.WriteLine("1.Create account");
                Console.WriteLine("2.Login to the account");
                Console.WriteLine("3.Remove account");
                Console.WriteLine("5.Exit Program");
                choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        customer.AddAccount(filePath, filePath1);
                        break;

                    case 2:
                        break;

                    case 3:
                        break;

                    case 4:
                        break;

                    case 5:
                        exit = false;
                        break;

                    default:
                        Console.WriteLine("Error try another option 1-5.");
                        break;

                }


            }
        }
    }


    // Клас для рахунку зберігання
    public class SavingsAccount : BankAccount
    {
        // Реалізація методів IAccountOperations для рахунку зберігання
        public override void Deposit(double amount)
        {
            // Логіка внесення грошей на рахунок зберігання
        }

        public override void Withdraw(double amount)
        {
            // Логіка зняття грошей з рахунку зберігання
        }

        public override void Transfer(BankAccount targetAccount, double amount)
        {
            // Логіка переказу з рахунку зберігання на інший рахунок
        }
    }

}