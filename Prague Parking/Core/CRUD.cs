using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prague_Parking.DataAccess;
using Prague_Parking.Core;


namespace Prague_Parking
{
    public class CRUD
    {

         public static string AddVehicle()
        {
            using (var context = new PragueParkingContext())
            {
                var vehicle = Vehicle.GetRandomVehicle();
                var addVehicle = new ParkingLot
                {

                    VehicleType = vehicle.VehicleType,
                    ParkedAtTime = DateTime.Now,
                    NumberPlate = vehicle.NumberPlate,

                };
                context.Add(addVehicle);
                context.SaveChanges();

                return String.Format($"Vehicle {addVehicle.VehicleType}#{addVehicle.NumberPlate} added to database");
            }
        }


        public static void RemoveVehicle(string numberPlate)
        {
            if (!SearchVehicle(numberPlate))
            {
                Console.WriteLine("Vehicle you searched for was not found in the parking lot.");
            }
            else
            {
                DateTime currentTime = DateTime.Now;
                using (var context = new PragueParkingContext())
                {
                    var vehicle = context.ParkingLot.Where(n=>n.NumberPlate == numberPlate).FirstOrDefault();
                    var difference = currentTime - vehicle.ParkedAtTime;
                    //double price = vehicle.VehicleType == "Car#" && vehicle.ParkedAtTime.Minute < 10 ? 20 : 10;
                    //Console.WriteLine($"{vehicle.VehicleType}{vehicle.NumberPlate} was parked for {timeSpan.ToString(@"hh\:mm\:ss")}");

                    //if (vehicle.VehicleType == "Car#")
                    //{
                    //    if (difference.TotalMinutes < 10)
                    //        price = 
                    //    else
                    //    {
                    //        difference -= TimeSpan.FromMinutes(10);
                    //        price = difference.TotalHours * 20;
                    //    }

                    //}
                    //else
                    //{

                    //}

                    //Console.WriteLine($"{vehicle.VehicleType}{vehicle.NumberPlate} was parked for {difference:%d} days, {difference:%h} hours and {difference:%m} minutes" +
                    //    $" Total cost: {price:F2} CZK")


                }
            }


            //using (var context = new PragueParkingContext())
            //{
            //    var vehicle = context.ParkingLot.Where(n => n.NumberPlate == numberPlate).FirstOrDefault();
            //    if (vehicle == null)
            //        return false;
            //    else
            //    {
            //        context.Remove(vehicle);
            //        context.SaveChanges();
            //        return true;
            //        //return String.Format($"Vehicle {vehicle.VehicleType}#{vehicle.NumberPlate} has been removed from the database");
            //    }

            //}
        }

        public static bool SearchVehicle(string numberPlate)
        {
            using (var context = new PragueParkingContext())
            {
                var vehicle = context.ParkingLot.Where(n => n.NumberPlate == numberPlate).FirstOrDefault();
                if (vehicle == null)
                    return false;
                
                return true;
            }
        }

        //public static void MoveVehicle

    }
}
