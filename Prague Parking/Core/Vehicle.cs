using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prague_Parking.Core
{
    public class Vehicle
    {

        public string VehicleType { get; set; }
        public string NumberPlate { get; set; }
        public int VehicleSize { get; set; }
        public int VehicleCost { get; set; }


        public Vehicle()
        {
            this.NumberPlate = CreateNumberPlate();
        }

        public static Vehicle GetRandomVehicle()
        {
            int type = HelpingClass.RandomNumber(0, 8);
            if (type <= 7)
                return new Car();
            else
                return new MC();
        }

        public static string CreateNumberPlate()
        {

            Random rnd = new Random();

            string allowedCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXY123456790";
            string rareCharacters = "ÅÆÇÉÑÜÝßàáâãåæçèéêëñòóôùúûüÿ";
            
            string numberplate = string.Empty;
            int numberplateLength = rnd.Next(4, 11);

            for (int i = 0; i < numberplateLength; i++)
            {
                numberplate += allowedCharacters[rnd.Next(allowedCharacters.Length)];
            }
            if (numberplate.Length >= 4 && numberplate.Length <= 5)
            {
                for (int i = 0; i < 2; i++)
                {
                    numberplate += rareCharacters[rnd.Next(rareCharacters.Length)];
                }
            }

            return numberplate;
        }



    }

}

 