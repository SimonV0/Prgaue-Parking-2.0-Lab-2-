using Prague_Parking.DataAccess;
using Spectre.Console;
using Prague_Parking.Core;


namespace Prague_Parking.UI
{
    public class Menu
    {

        public static void MainMenu()
        {
            Console.WriteLine();
            var menu = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
               .Title("[green]Prague Parking 2.0[/]")
               .PageSize(10)
               .AddChoices(new[] {
                "Add a new vehicle", "Show all parked vehicles", "Move a vehicle",
            "Search for a vehicle", "Remove a vehicle", "End program",
               }));
            Switch(menu);
        }


        private static void Switch(string menu)
        {
            switch (menu)
            {
                case "Add a new vehicle":
                    Console.WriteLine(CRUD.AddVehicle());
                    
                    MainMenu();
                    break;
                case "Show all parked vehicles":
                    ConfigFile.ReadJSONFile();
             
                    break;
                case "Move a vehicle":
                    break;
                case "Search for a vehicle":

                    DisplaySearchedVehicle();

                    break;
                case "Remove a vehicle":
                    Console.Write("Enter the number plate of the vehicle you want to remove from the parking lot: ");
                    var numberPlate = Console.ReadLine();
                    CRUD.RemoveVehicle(numberPlate!);
                    MainMenu();
                    break;
                case "End program":
                    Console.WriteLine("Exiting Prague Parking");
                    break;
                default:
                    break;


            }
        }

        private static void DisplaySearchedVehicle()
        {
            Console.Write("Enter the number plate of the vehicle you want to search for: ");
            var plate = Console.ReadLine();
            if (!CRUD.SearchVehicle(plate))
            {
                Console.WriteLine("Vehicle was not found in the parking lot.");
            }
            else
            {
                using (var context = new PragueParkingContext())
                {
                    var vehicle = context.ParkingLot.Where(n => n.NumberPlate == plate).FirstOrDefault();
                    Console.WriteLine($"{vehicle.VehicleType}{vehicle.NumberPlate} - {vehicle.ParkedAtTime}");
                }
            }
        }
    }
}