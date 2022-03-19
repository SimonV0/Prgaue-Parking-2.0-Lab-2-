using Prague_Parking.DataAccess;
using Spectre.Console;
using Prague_Parking.Core;


namespace Prague_Parking.UI
{
    public class Menu
    {
        // TODO: Fixa en översiktlig view på hela garaget.
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
                    Console.WriteLine(ParkingSpot.AddVehicle());
                    Overlay();
                    MainMenu();
                    break;
                case "Show all parked vehicles":
                    ParkingGarage.DisplayAllVehicles();
                    Overlay();
                    MainMenu();
                    break;
                case "Move a vehicle":
                    MoveVehicleDisplay(); 
                    Overlay();
                    MainMenu();
                    break;
                case "Search for a vehicle":
                    DisplaySearchedVehicle();
                    Overlay();
                    MainMenu();
                    break;
                case "Remove a vehicle":
                    RemoveVehicleDisplay();
                    Overlay();
                    MainMenu();
                    break;
                case "End program":
                    Console.WriteLine("Exiting Prague Parking");
                    break;
                default:
                    break;


            }
        }

        private static void RemoveVehicleDisplay()
        {
            Console.Write("Enter the number plate of the vehicle you want to remove from the parking lot: ");
            var numberPlate = Console.ReadLine();
            ParkingSpot.RemoveVehicle(numberPlate!);
        }

        private static string MoveVehicleDisplay()
        {
            Console.Write("Enter the vehicles registration number that you want to move: ");
            string numberPlate = Console.ReadLine()!;
            if (ParkingGarage.SearchVehicle(numberPlate!))
            {
                Console.Write("Enter the parking spot that you want to move the vehicle to: ");
                int newSpot;
                while (!int.TryParse(Console.ReadLine(), out newSpot))
                    Console.WriteLine("Invalid input, enter a valid parking spot.");
                if (newSpot > ConfigFile.ParkingGarageSize)
                    Console.WriteLine($"There's only {ConfigFile.ParkingGarageSize} parkingspots.");
                else
                    ParkingSpot.MoveVehicle(numberPlate!, newSpot);
                return numberPlate!;
            }
            else
            {
                return String.Format($"There's no vehicle with the numberplate ({numberPlate}) parked here.");
            }
        }

        private static void DisplaySearchedVehicle()
        {
            Console.Write("Enter the number plate of the vehicle you want to search for: ");
            var plate = Console.ReadLine();
            if (!ParkingGarage.SearchVehicle(plate!))
            {
                Console.WriteLine("Vehicle was not found in the parking lot.");
            }
            else
            {
                using (var context = new PragueParkingContext())
                {
                    var vehicle = context.ParkingLot!.Where(n => n.NumberPlate == plate).FirstOrDefault();
                    Console.WriteLine($"{vehicle!.VehicleType}{vehicle.NumberPlate} - {vehicle.ParkedAtTime}");
                    ParkingGarage.Receipt(plate);
                }
            }
        }

        public static void Overlay()
        {
            
            using (var context = new PragueParkingContext())
            {
                Table table = new Table();
                table.AddColumns("[green]EMPTY SPOT[/]", "[yellow]HAlF FULL[/]", "[red]FULL SPOT[/]");
                AnsiConsole.Write(table);

                var colour = String.Empty;
                var display = String.Empty;

                for (int i = 0; i <= ConfigFile.ParkingGarageSize; i++)
                {
                    var parkingSpots = context.ParkingLot!.Where(s => s.ParkingSpot == i).FirstOrDefault();
                    if (parkingSpots != null)
                    {
                        if (parkingSpots.ParkingSize == 2)
                            colour = "yellow";
                        else
                            colour = "red";

                        display += ($"[{colour}] {i}[/] ");
                    }
                    else
                    {
                        colour = "green";
                        display += ($"[{colour}] {i}[/] ");
                    }
                }

                Table table2 = new Table();
                table2.AddColumn(new TableColumn(display));
                AnsiConsole.Write(table2);
            }

        }
    }
}