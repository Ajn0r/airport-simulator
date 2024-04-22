using System.Windows.Threading;

namespace AirportSimulator
{
    public class Airplane
    {
        private DispatcherTimer ?timer;
        public string Name { get; set; }
        public TimeOnly localTime { get; set; }
        public double Altitude { get; set; }
        public double FlightTime { get; set; }
        public string FlightID { get; set; }
        public string Destination { get; set; }
        public bool canLand { get; set; }

        public Airplane(string name, string flightId, string destination, double flightTime)
        {
            Name = name;
            FlightID = flightId;
            Destination = destination;
            FlightTime = flightTime;
        }

        public void OnTakeOff()
        {
            
        }

        public void OnLanding()
        {
            if (canLand)
            {
                Altitude = 0;
            }
        }

        public void ChangeAltitude(int altitude)
        {
            Altitude = altitude;
        }


        /// <summary>
        /// Methód to set up the timer
        /// </summary>
        public void SetUpTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = new System.TimeSpan(0, 0, 1);
            timer.Tick += new EventHandler(DispatcherTimer_Tick);
            timer.Start();
        }
        /// <summary>
        /// Method to add one hour to the local time
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DispatcherTimer_Tick(object? sender, EventArgs e)
        {
            // Add one hour to the local time
            localTime = localTime.AddHours(1);
        }
    }
}