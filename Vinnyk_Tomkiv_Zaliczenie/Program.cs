using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vinnyk_Tomkiv_Zaliczenie;

namespace Vinnyk_Tomkiv_Zaliczenie
{
    class Program
    {
        static void Main(string[] args)
        {
            const string filePath = @"BankAccountData.txt";
            const string filePath1 = @"BankQuantityData.txt";
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
                        Console.WriteLine("Acc successfully created");
                        break;

                    case 2:
                        Console.WriteLine("Acc successfully logined");
                        break;

                    case 3:
                        Console.WriteLine("Acc successfully removed");
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

}
