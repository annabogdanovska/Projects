using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracking.Services.Helpers
{
    public static class ValidationHelper
    {

        public static bool ValidUserName(string username)
        {
            if (string.IsNullOrEmpty(username) || username.Length < 5)
            {
                return false;
            }
            return true;
        }
        public static bool ValidPassword(string password)
        {
            if (password.Length < 6)
            {
                return false;
            }

            bool containsNumber = false;

            foreach (char c in password)
            {
                if (int.TryParse(c.ToString(), out int number))
                {
                    containsNumber = true;
                    break;
                }
            }
            if (!containsNumber)
            {
                return false;
            }

            bool containsCapitalLetter = false;
            foreach (char c in password)
            {
                if (Char.IsUpper(c))
                {
                    containsCapitalLetter = true;
                    break;
                }
            }
            if (!containsCapitalLetter)
            {
                return false;
            }
            return true;
        }
        public static bool ValidFirstLastName(string name)
        {
            if (string.IsNullOrEmpty(name) || name.Length < 2)
            {
                return false;
            }

            bool containNumbers = false;
            foreach (char c in name)
            {
                if (int.TryParse(c.ToString(), out int number))
                {
                    return false;
                }
            }
            if (containNumbers)
            {
                return false;
            }
            return true;
        }
        public static bool ValidAge(int age)
        {
            if (age < 18 || age > 120)
            {
                return false;
            }
            return true;
        }
    }
}
