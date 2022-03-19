using Prague_Parking.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prague_Parking.Core
{
    public class ParkingSpot
    {
        public int Parkingspot { get; set; }


        public static int RandomParkingSpot(Vehicle vehicle)
        {

            var randomNumber = HelpingClass.RandomNumber(ConfigFile.ParkingGarageSize);
            var count = 0;
            using (var context = new PragueParkingContext())
            {
                var emptySlot = context.ParkingLot!.Where(s => s.ParkingSpot == randomNumber).FirstOrDefault();
                
                if (emptySlot != null)
                {
                    for (int i = 1; i < ConfigFile.ParkingGarageSize; i++)
                    {
                        emptySlot = context.ParkingLot!.Where(s => s.ParkingSpot == i).FirstOrDefault();
                        if (emptySlot != null && vehicle.VehicleType == "MC" && emptySlot.ParkingSize >= 2)
                        {
                            randomNumber = i;
                            return i;
                            
                        }
                        else if (emptySlot == null)
                        {
                            randomNumber = i;
                            return i;
                        }
                        count++;
                    }
                }
                else if (emptySlot == null)
                {
                    return randomNumber;
                }
            }
            return -1;
        }

        public static string AddVehicle()
        {
            var vehicle = Vehicle.GetRandomVehicle();
            var spot = ParkingSpot.RandomParkingSpot(vehicle);
            if (spot == -1 && !ParkingGarage.EmptyGarage())
            {
                return String.Format("Parking garage is full.");
            }
            else
            {
                using (var context = new PragueParkingContext())
                {
                    var currentParkingSize = context.ParkingLot!.Where(s => s.ParkingSpot == spot).FirstOrDefault();
                    var addVehicle = new ParkingLot
                    {
                        VehicleType = vehicle.VehicleType,
                        ParkedAtTime = DateTime.Now,
                        NumberPlate = vehicle.NumberPlate,
                        ParkingSpot = spot,
                        ParkingSize = currentParkingSize == null ? ConfigFile.ParkingSlotSize - vehicle.VehicleSize : currentParkingSize.ParkingSize- vehicle.VehicleSize
                    };

                    if (currentParkingSize != null)
                        currentParkingSize.ParkingSize -= vehicle.VehicleSize;
                    
                    context.Add(addVehicle);
                    context.SaveChanges();

                    return String.Format($"Vehicle {addVehicle.VehicleType}#{addVehicle.NumberPlate} parked at slot number: {addVehicle.ParkingSpot}");
                }
            }

        }

        // TODO: Fixa så att man för ökad plats när fordonet tas bort. 

        public static void RemoveVehicle(string numberPlate)
        {
            if (!ParkingGarage.SearchVehicle(numberPlate))
            {
                Console.WriteLine("Vehicle you searched for was not found in the parking lot.");
            }
            else
            {
                ParkingGarage.Receipt(numberPlate);
                using (var context = new PragueParkingContext())
                {
                    var vehicle = context.ParkingLot!.Where(v => v.NumberPlate == numberPlate).FirstOrDefault();
                    var parkingSpot = vehicle.ParkingSpot;
                    context.Remove(vehicle!);
                    var doubleParked = context.ParkingLot.Where(s => s.ParkingSpot == parkingSpot).FirstOrDefault();
                    if (doubleParked != null)
                    {
                        doubleParked.ParkingSize += 2;
                    }
                    context.SaveChanges();
                }
            }
        }

        // TODO: Lägg till en check så att man kan parkera två fordon tillsammans. 
        public static void MoveVehicle(string numberPlate, int newParkingSpot)
        {

            if (!ParkingGarage.SearchVehicle(numberPlate))
            {
                Console.WriteLine($"There's no vehicle with the numberplate ({numberPlate}) parked here.");
            }
            else
            {
                using (var context = new PragueParkingContext())
                {
                    var newSpot = context.ParkingLot!.Where(s => s.ParkingSpot == newParkingSpot).FirstOrDefault(); // Parking spot
                    var mainVehicle = context.ParkingLot!.Where(s => s.NumberPlate == numberPlate).FirstOrDefault(); // Vehicle numberplate 
                    if (newSpot != null && newSpot.NumberPlate != numberPlate && newSpot.ParkingSize == 0)
                    {
                        var spotCount = context.ParkingLot!.Where(s => s.ParkingSpot == newParkingSpot).Count();
                        if (spotCount == 2)
                            Console.WriteLine($"There's already two motorbikes parked on parkingspot number: {newParkingSpot}");
                        else
                            Console.WriteLine($"Another vehicle ({newSpot.VehicleType}#{newSpot.NumberPlate}) is already parked on spot number: {newParkingSpot}");
                    }
                    else
                    {
                        
                        if (mainVehicle!.ParkingSpot == newParkingSpot)
                        {
                            Console.WriteLine($"{mainVehicle.VehicleType}#{mainVehicle.NumberPlate}, is already parked on the same spot as you're trying to move it to: {newParkingSpot}");
                        }
                        else if (mainVehicle.VehicleType == "Car")
                        {
                            if (newSpot != null && newSpot.ParkingSize < ConfigFile.CarSize)
                            {
                                Console.WriteLine($"There's no space to park another car. {newSpot.VehicleType}#{newSpot.NumberPlate} is already parked here");
                            }
                            else
                            {
                                mainVehicle.ParkingSpot = newParkingSpot;
                                Console.WriteLine($"{mainVehicle.VehicleType}#{mainVehicle.NumberPlate}, has been moved to parking spot number: {newParkingSpot}");
                            }
                        }
                        else if (mainVehicle.VehicleType == "MC")
                        {
                            var currentSlotVehicle = context.ParkingLot.Where(s => s.ParkingSpot == mainVehicle.ParkingSpot && s.NumberPlate != numberPlate).FirstOrDefault();
                            if (newSpot == null)
                            {
                                mainVehicle.ParkingSize = ConfigFile.ParkingSlotSize;
                                if (currentSlotVehicle != null)
                                    currentSlotVehicle.ParkingSize += ConfigFile.McSize;
                                mainVehicle.ParkingSpot = newParkingSpot;
                                mainVehicle.ParkingSize -= ConfigFile.McSize;

                            }
                            else if (newSpot != null) 
                            {

                                if (newSpot.ParkingSize != 0)
                                    newSpot.ParkingSize -= ConfigFile.McSize;

                                if (currentSlotVehicle != null)
                                    currentSlotVehicle.ParkingSize += ConfigFile.McSize;
                                mainVehicle.ParkingSpot = newParkingSpot;
                                if(mainVehicle.ParkingSize >= 2)
                                    mainVehicle.ParkingSize -= ConfigFile.McSize;
                                Console.WriteLine($"{mainVehicle.VehicleType}#{mainVehicle.NumberPlate}, has been moved to parking spot number: {newParkingSpot}");
                            }
                        }
                       
                        context.SaveChanges();
                    }
                    }
                }
            }


        }
    }

