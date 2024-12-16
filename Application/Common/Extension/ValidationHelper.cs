using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Extension
{
    public static class ValidationHelper
    {
        public static bool IsNationalCode(string input)
        {
            if (input.Length != 10 || !input.All(char.IsDigit))
                return false;

            int[] coefficients = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int checksum = int.Parse(input[9].ToString());
            int sum = 0;

            for (int i = 0; i < 9; i++)
            {
                sum += int.Parse(input[i].ToString()) * coefficients[i];
            }

            int remainder = sum % 11;

            return (remainder < 2 && remainder == checksum) || (remainder >= 2 && checksum == (11 - remainder));
        }

        public static bool IsPhoneNumber(string input)
        {
            if (input.Length == 11 && input.StartsWith("09") && input.All(char.IsDigit))
                return true;

            if (input.Length == 11 && input.StartsWith("0") && input.All(char.IsDigit))
                return true;

            return false;
        }
    }
}
