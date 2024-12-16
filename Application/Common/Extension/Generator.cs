using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Extension
{
    public static class Generator
    {
        public static string GetOtpCode()
        {
            Random random = new Random();
            return random.Next(100000, 1000000).ToString();
        }
    }
}
