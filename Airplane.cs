using System.Windows;
using System.Windows.Threading;

namespace AirportSimulator
{
    public class Airplane
    {
        private DispatcherTimer ?timer;
        public string Name { get; set; }
        public TimeOnly LocalTime { get; set; }
        public double Altitude { get; set; }
        public double FlightTime { get; set; }
        public string FlightID { get; set; }
        public string Destination { get; set; }
        
        public bool canLand { get; set; }
        public string Status {
            // get the value
            get => canLand ? "In the air" : "On the ground";
            set => Status = value;
        }
        public bool IsTakeOffSubscribed { get; set; }
        public bool OpenWindow { get; set; }

        public Airplane(string name, string flightId, string destination, double flightTime)
        {
            Name = name;
            FlightID = flightId;
            Destination = destination;
            FlightTime = flightTime;
        }

        public event EventHandler<AirplaneEventArgs> TakeOff;
        public event EventHandler<AirplaneEventArgs> Landing;

        /// <summary>
        /// Method that opens the airplane window for the airplane and subscribes to the Takeoff and Landig button events
        /// </summary>
        private void OpenAirplaneWindow()
        {
            // Create a new instance of the AirplaneWindow
            AirplaneWindow airplaneWindow = new AirplaneWindow(this);
            // Subscribe to the TakeOffButtonClicked event
            airplaneWindow.TakeOffButtonClicked += TakingOff;
            airplaneWindow.LandButtonClicked += Land;
            airplaneWindow.Closing += ClosingWindow;
            airplaneWindow.Show(); // Show the window
            OpenWindow = true;
        }

        /// <summary>
        /// Method to handle when the user tries to close the a
        /// irplane window by clicking the X in the top right corner, making sure the user is sure they want to close the window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClosingWindow(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Ask the user if they are sure they want to close the window
            MessageBoxResult result = MessageBox.Show("Are you sure you want to close the window?", "Close window", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                // if the user clicks yes, close the window (Cancel = false means don't cancel the closing of window, confusing..)
                e.Cancel = false;
                // reset the airplane to be able to use it again
                ResetAirplane();
            } else // if the user clicks no, do nothing
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Method to reset the airplane to be able to use it again.
        /// </summary>
        private void ResetAirplane()
        {
            OpenWindow = false;
            canLand = false;
            Altitude = 0;
        }
 
        /// <summary>
        /// Method that is called when the airplane is taking off
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TakingOff(object sender, EventArgs e)
        {
            string flightinfo = Name + " with flight Id: " + FlightID;
            if (canLand)
            {
                MessageBox.Show(Name + " is already in the air, please wait until it has landed or land now");
                return;
            }
            AirplaneEventArgs args = new AirplaneEventArgs(flightinfo, " taking off to destination: " + Destination + ", " + LocalTime);
            TakeOff?.Invoke(this, args);
            canLand = true;
            
            Altitude = 11500;
            // Get the current time and convert it to a TimeOnly object to set the local time
            DateTime currentTime = DateTime.Now;
            LocalTime = TimeOnly.FromDateTime(currentTime);
            if (timer == null)
            {
                SetUpTimer();
            }
            else
            {
                timer.Start();
            }
        }

        /// <summary>
        /// Method that is called from the ControlTower when the airplane is ordered to take off
        /// </summary>
        public void OnTakeOff()
        {
            if (OpenWindow)
            {
                // only allow the airplane window to be opened once, if it is already open, do nothing
                return;
            }
            // Calls the method to open the airplane window
            OpenAirplaneWindow();
            
        }

        private void Land(object sender, EventArgs e)
        {
            if (!canLand)
            {
                MessageBox.Show(Name + " is already on the ground");
                return;
            }
            OnLanding();
            string flightinfo = Name + " with flight Id: " + FlightID;
            AirplaneEventArgs args = new AirplaneEventArgs(flightinfo, " has landed at " + Destination + " at: " + LocalTime);
            Landing?.Invoke(this, args);
            Destination = "Home";
        }
        /// <summary>
        /// Method that handles when the airplane is landing
        /// </summary>
        public void OnLanding()
        {
            // Set the airplane to not be able to land, indicating it is able to takeoff
            canLand = false;
            // Set the altitude to 0 when the airplane lands
            Altitude = 0;
            timer.Stop();
        }

        /// <summary>
        /// Method to change the altitude of the airplane
        /// </summary>
        /// <param name="altitude"></param>
        public void ChangeAltitude(int altitude)
        {
            if (altitude < 0) // Check if the altitude is negative
            {
                MessageBox.Show("Altitude cannot be negative");
            } else if (altitude > 14000) // Check if the altitude is higher than 14000
            {
                MessageBox.Show("Altitude cannot be higher than 10000");
            } else if (canLand && altitude <= 500) // Check if the airplane is in the air and the altitude is lower than 500
            {
                MessageBox.Show(Name + " is in the air, the requested altitude is to low.");
            } else // else, it is ok to change the altitude
            {
                Altitude = altitude;
                // call an event to print the new altitude, displayed as a message box for now
                MessageBox.Show(Name + "'s altitude changed to: " + Altitude);
            }
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
            LocalTime = LocalTime.AddHours(1);
        }
        public override string ToString()
        {
            return Name + ", " + FlightID;
        }
    }

}