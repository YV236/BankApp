using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            OddNums(numbers);
        }

        static void OddNums(int[] numbers)
        {
            Console.WriteLine("Odd Numbers");

            IEnumerable<int> OddNumbers = from number in numbers where number % 2 != 0 select number;
            Console.WriteLine(OddNumbers);

            foreach (int number in OddNumbers)
            {
                Console.WriteLine(number);
            }
        }
    }
}
