using Microsoft.EntityFrameworkCore;
using Prague_Parking.Core;

namespace Prague_Parking.DataAccess
{
    public class ParkingLot
    {
        public int Id { get; set; }
        public DateTime ParkedAtTime { get; set; }

        public string NumberPlate { get; set; }
        public string VehicleType { get; set; } // Ändra till något annat?

    }

    

}