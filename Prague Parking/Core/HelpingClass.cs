using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prague_Parking.Core
{
    internal class HelpingClass
    {
        public static int RandomNumber(int minValue, int maxValue)
        {
            Random rnd = new Random();
            int randomNumber = rnd.Next(minValue, maxValue + 1);
            return randomNumber;
        }
    }
}
