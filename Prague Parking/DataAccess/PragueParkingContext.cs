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

       
    }
}


