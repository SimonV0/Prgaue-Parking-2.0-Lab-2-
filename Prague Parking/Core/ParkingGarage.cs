using Prague_Parking.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prague_Parking.Core
{
    public class ParkingGarage
    {
        public int ParkingGarageSize { get; set; } = ConfigFile.ParkingGarageSize;
        public int ParkingSlotSize { get; set; } = ConfigFile.ParkingSlotSize;

        public static bool SearchVehicle(string numberPlate)
        {
            using (var context = new PragueParkingContext())
            {
                var vehicle = context.ParkingLot!.Where(n => n.NumberPlate == numberPlate).FirstOrDefault();
                if (vehicle == null)
                    return false;

                return true;
            }
        }

        public static void Receipt(string numberPlate)
        {
            var currentTime = DateTime.Now;
            TimeSpan difference = TimeSpan.Zero;
            using (var context = new PragueParkingContext())
            {
                var vehicle = context.ParkingLot!.Where(n => n.NumberPlate == numberPlate).FirstOrDefault();
                if ((vehicle!.ParkedAtTime - currentTime) < TimeSpan.FromMinutes(ConfigFile.FreeParkingMin))
                    difference = (currentTime - vehicle!.ParkedAtTime) - TimeSpan.FromMinutes(ConfigFile.FreeParkingMin);
                else
                    difference = currentTime - vehicle!.ParkedAtTime;

                double price = 0;
                if (difference.TotalMinutes > ConfigFile.FreeParkingMin)
                {
                    if (vehicle.VehicleType == "Car")
                    {
                        price = (difference.TotalMinutes / 60) * ConfigFile.CarPrice;
                    }
                    else if (vehicle.VehicleType == "MC")
                    {
                        price = (difference.TotalMinutes / 60) * ConfigFile.McPrice;
                    }
                }

                Console.WriteLine($"{vehicle.VehicleType}{vehicle.NumberPlate} has been parked for {difference:%d} days, {difference:%h} hours and {difference:%m} minutes" +
                    $" Total cost: {price:F2} CZK");
            }
        }
        public static bool EmptyGarage()
        {
            using (var context = new PragueParkingContext())
            {
                var emptyDatase = context.ParkingLot!.ToList();
                if (emptyDatase.Count <= 0)
                    return true;
                else
                    return false;
            }
        }

        public static void DisplayAllVehicles()
        {
            var vehicles = new List<ParkingLot>();
            using (var context = new PragueParkingContext())
            {
                vehicles = context.ParkingLot!.OrderBy(s => s.ParkingSpot).ToList();
            }

            if (vehicles.Count == 0)
            {
                Console.WriteLine("The parking garage is empty.");
            }
            else
            {
                foreach (var vehicle in vehicles)
                {
                    Console.WriteLine($"{vehicle.ParkingSpot}. {vehicle.VehicleType}#{vehicle.NumberPlate}");
                }
            }

        }

    }



}
