using System;
using System.Collections.Generic;
using System.Text;

namespace TakePromoCodes
{
    public static class ListExtensions
    {
        public static void ToUppercase(this List<String> promoCodes)
        {
            List<String> uppercasePromoCodes = new List<String>();

            foreach (var code in promoCodes)
            {
                uppercasePromoCodes.Add(code.ToUpper());
            }

            promoCodes = uppercasePromoCodes;
        }
    }
}
