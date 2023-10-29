using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vinnyk_Tomkiv_Project;

namespace Vinnyk_Tomkiv_Project
{
    // Базовий клас банківського рахунку

    public class BankAccount : IAccountOperations
    {
        public int AccountNumber { get; set; }
        public double Balance { get; set; }

        public void createBankAccount(string filePath, string Login)
        {
            Random random = new Random();
            AccountNumber = 0;
            Balance = 0;
            bool exit = true;

            if (File.Exists(filePath))
            {
                try
                {
                    string[] existingContent = File.ReadAllLines(filePath);

                    while (exit)
                    {
                        AccountNumber = random.Next(100000000, 1000000001);
                        bool accountNumExists = false;

                        foreach (string line in existingContent)
                        {
                            string[] parts = line.Split(';');
                            int.TryParse(parts[1], out int num);
                            if (parts.Length == 3 && AccountNumber == num)
                            {
                                accountNumExists = true;
                                break;
                            }
                        }

                        if (!accountNumExists)
                        {
                            //string newAccountLine = "\n" + Login + ";" + accountNum + ";" + amount;
                            string newAccountLine = $"\n{Login};{AccountNumber};{Balance}";
                            File.AppendAllText(filePath, newAccountLine);
                            exit = false;
                        }
                    }
                }
                catch (IOException ex)
                {
                    Console.WriteLine("Sorry, something went wrong. Please try again later.");
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Sorry, something went wrong. Please try again later.");
            }
        }



        //public static void createBankAccount(string filePath, string Login)
        //{
        //    Random random = new Random();
        //    int accountNum = 0;
        //    bool exit = true;
        //    if (File.Exists(filePath))
        //    {
        //        try
        //        {
        //            string[] existingContent = File.ReadAllLines(filePath);

        //            while (exit)
        //            {
        //                accountNum = random.Next(1000000, 100000001);
        //                foreach (string line in existingContent)
        //                {
        //                    string[] parts = line.Split(';');
        //                    if (parts.Length == 3 && int.TryParse(parts[1], out int number))
        //                    {
        //                        if (accountNum != number)
        //                        {
        //                            string modifiedContent = existingContent + "\n" + Login + accountNum;

        //                            File.WriteAllText(filePath, modifiedContent);
        //                            exit = false;
        //                        }
        //                    }
        //                }
        //            }


        //        }
        //        catch (IOException ex)
        //        {
        //            Console.WriteLine("Sorry something went wrong please try again later");
        //            Console.WriteLine(ex.Message);
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("Sorry something went wrong please try again later");
        //    }
        //}

        public virtual void Deposit(double amount)
        {

        }
        public virtual void Withdraw(double amount)
        {

        }
        public virtual void Transfer(BankAccount targetAccount, double amount)
        {

        }
    }
}