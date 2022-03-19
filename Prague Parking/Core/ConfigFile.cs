using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Prague_Parking.Core
{
    class ConfigFile
    {
        public static int ParkingGarageSize { get; set; }
        public static int ParkingSlotSize { get; set; }
        public static int CarSize { get; set; }
        public static int McSize { get; set; }
        public static int CarPrice { get; set; }
        public static int McPrice { get; set; }
        public static int FreeParkingMin { get; set; }


        private static void SaveJSONFile()
        {
            var configFile = new ConfigFileDeserialize
            {
                ParkingGarageSize = 100,
                ParkingSlotSize = 4,
                CarSize = 4,
                McSize = 2,
                CarPrice = 20,
                McPrice = 10,
                FreeParkingMin = 10
            };

            using (StreamWriter file = File.CreateText(@"../../../ConfigFile.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, configFile);
            }
        }

        public static void ReadJSONFile()
        {
            try
            {
                using (StreamReader file = File.OpenText(@"../../../ConfigFile.json"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    ConfigFileDeserialize configFile = (ConfigFileDeserialize)serializer.Deserialize(file, typeof(ConfigFileDeserialize))!;
                    ConfigFile.ParkingGarageSize = configFile.ParkingGarageSize;
                    ConfigFile.ParkingSlotSize = configFile.ParkingSlotSize;
                    ConfigFile.CarSize = configFile.CarSize;
                    ConfigFile.McSize = configFile.McSize;
                    ConfigFile.CarPrice = configFile.CarPrice;
                    ConfigFile.McPrice = configFile.McPrice;
                    ConfigFile.FreeParkingMin = configFile.FreeParkingMin;  
                }
            }
            catch (Exception)
            {

                SaveJSONFile();
                Console.WriteLine("No config file was found, default settings applied.");
            }
            
        }
    }

    // Behövs för att kunna använda static properties, kunde inte deserialize static properties
    public class ConfigFileDeserialize
    {
        public int ParkingGarageSize { get; set; }
        public int ParkingSlotSize { get; set; }
        public int CarSize { get; set; }
        public int McSize { get; set; }
        public int CarPrice { get; set; }
        public int McPrice { get; set; }
        public int FreeParkingMin { get; set; }


    }

}
