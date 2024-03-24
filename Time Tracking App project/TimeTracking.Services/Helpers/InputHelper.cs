using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracking.Services.Helpers
{
    public class InputHelper
    {
        public static int InputNumber()
        {
            string input = Console.ReadLine();

            if (!int.TryParse(input, out int number))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                throw new Exception("Wrong input/selection!");
                Console.ResetColor();
            }
            return number;
        }
    }
}

