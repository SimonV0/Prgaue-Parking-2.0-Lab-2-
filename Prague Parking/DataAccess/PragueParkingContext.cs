using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Prague_Parking.Core;

namespace Prague_Parking.DataAccess
{
    public class PragueParkingContext : DbContext
    {

            public PragueParkingContext()
            {
            }

            public PragueParkingContext(DbContextOptions<PragueParkingContext> options) : base(options)
            {
            }
            public DbSet<ParkingLot>? ParkingLot { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }



        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{

        //    if (!optionsBuilder.IsConfigured)
        //    {

        //        // Fungerade inte men lämnar kvar koden iallafall.

        //        var builder = new ConfigurationBuilder()
        //                .SetBasePath(Directory.GetCurrentDirectory())
        //                .AddJsonFile($"appsettings.json", true, true)
        //                .Build();
        //        string connectionString =
        //        builder.GetConnectionString("DefaultConnection");
        //        optionsBuilder.UseSqlServer(connectionString);

        //        //optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=PragueParking;Trusted_Connection=True;");

        //    }
        //}


    }
}


