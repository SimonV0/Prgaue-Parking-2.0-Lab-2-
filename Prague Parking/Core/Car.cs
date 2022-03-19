using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prague_Parking.Core
{
    public class Car : Vehicle
    {
        public Car()
        {
            VehicleType = "Car";
            VehicleSize = ConfigFile.CarSize;
            VehicleCost = ConfigFile.CarPrice;
        }
    }
}
