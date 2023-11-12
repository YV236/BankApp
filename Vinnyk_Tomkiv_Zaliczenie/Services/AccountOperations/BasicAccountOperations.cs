using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vinnyk_Tomkiv_Zaliczenie;
using Vinnyk_Tomkiv_Zaliczenie.Models;
using Vinnyk_Tomkiv_Zaliczenie.Services.BankAccManagement;

namespace Vinnyk_Tomkiv_Zaliczenie
{
    // Клас для рахунку зберігання
    public class BasicAccountOperations : BankAccountManagement
    {
        public void OperationsMenu(string login, int index)
        {
            int choice;

            Console.Clear();

            Console.WriteLine("1.Deposit");
            Console.WriteLine("2.Withdraw");
            Console.WriteLine("3.Transfer to another user");
            Console.WriteLine("4.Exit to menu");

            choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Deposit(login, index, 0);
                    break;

                case 2:
                    Console.WriteLine("Withdraw");
                    break;

                case 3:
                    Console.WriteLine("Transfer");
                    break;

                case 4:
                    break;
            }
        }

        // Логіка внесення грошей на рахунок зберігання
        public override void Deposit(string login, int index, double amount)
        {
            BankAccountManagement bankAccountManagement = new BankAccountManagement();


            while (true)
            {
                Console.Clear();

                Console.Write("How much do you want deposit to your account: ");

                if (double.TryParse(Console.ReadLine(), out amount) && amount > 0)
                {
                    bankAccountManagement.Deposit(login, index, amount);
                    break;
                }
                else
                {
                    Console.WriteLine("Please write how much do you want deposit to your account");
                    Console.ReadKey();
                }
            }
            Console.ReadKey();

        }

        // Логіка зняття грошей з рахунку зберігання
        public override void Withdraw(double amount)
        {


        }

        // Логіка переказу з рахунку зберігання на інший рахунок
        public override void Transfer(BankAccount targetAccount, double amount)
        {


        }
    }

}
