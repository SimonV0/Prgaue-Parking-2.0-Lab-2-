using Prague_Parking;
using Prague_Parking.Core;

namespace Prague_Parking_WinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void add_vehicle_button_Click(object sender, EventArgs e)
        {

            ParkingSpot.AddVehicle();
        }
                
    }
}