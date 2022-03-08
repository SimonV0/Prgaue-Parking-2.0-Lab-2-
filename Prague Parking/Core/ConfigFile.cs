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
        public int ParkingLotSize { get; set; }
        public int CarSize { get; set; }
        public int McSize { get; set; }
        public int CarPrice { get; set; }
        public int McPrice { get; set; }
        public int FreeParkingMin { get; set; }

        private static void SaveJSONFile()
        {
            var configFile = new ConfigFile
            {
                ParkingLotSize = 100,
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
                    ConfigFile configFile = (ConfigFile)serializer.Deserialize(file, typeof(ConfigFile))!;
                }
            }
            catch (Exception)
            {
             
                SaveJSONFile();
                Console.WriteLine("No config file was found, default settings applied.");
            }
            
        }
    }
}
