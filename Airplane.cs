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
            // get the value, only used to display the status of the airplane in the airplane window
            get => canLand ? "In the air" : "On the ground";
        }
        public bool IsTakeOffSubscribed { get; set; }
        public bool IsAltitudeChangeSubscribed { get; set; }
        public bool OpenWindow { get; set; }
        public AirplaneWindow airplaneWindow { get; set; }

        public Airplane(string name, string flightId, string destination, double flightTime)
        {
            Name = name;
            FlightID = flightId;
            Destination = destination;
            FlightTime = flightTime;
        }

        public event EventHandler<AirplaneEventArgs> TakeOff;
        public event EventHandler<AirplaneEventArgs> Landing;
        public event EventHandler<AirplaneEventArgs> AltitudeChanged;

        /// <summary>
        /// Method that opens the airplane window for the airplane and subscribes to the Takeoff and Landig button events
        /// </summary>
        private void OpenAirplaneWindow()
        {
            // Create a new instance of the AirplaneWindow
            airplaneWindow = new AirplaneWindow(this);
            // Subscribe to the TakeOffButtonClicked and LandButtoncli´cked event in the airplane window class
            airplaneWindow.TakeOffButtonClicked += TakingOff;
            airplaneWindow.LandButtonClicked += Land;
            // Subscribe to the closing event of the window
            airplaneWindow.Closing += ClosingWindow;
            airplaneWindow.Show(); // Show the window
            OpenWindow = true; // Set the open window property to true to indicate that the window is open and only allow it to be opened once
            airplaneWindow.DataContext = this;
        }

        /// <summary>
        /// Method to handle when the user tries to close the a
        /// irplane window by clicking the X in the top right corner, making sure the user is sure they want to close the window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClosingWindow(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (canLand) // Check if the airplane is in the air, then inform the user to land the airplane before closing the window
            {
                MessageBox.Show(this.ToString() + " is in the air, please land first", "Cannot close window");
                e.Cancel = true;
                return;
            }
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
            // Check if the airplane is already in the air, and if so, display a message and return 
            if (canLand)
            {
                MessageBox.Show(Name + " is already in the air, please wait until it has landed or land now");
                return;
            }
            
            canLand = true;
            Altitude = 11500;
            // Check if the timer is not set up, and set it in that case
            if (timer == null)
            {
                // Get the current time and convert it to a TimeOnly object to set the local time
                DateTime currentTime = DateTime.Now;
                LocalTime = TimeOnly.FromDateTime(currentTime);
                SetUpTimer();
            } else // else if the timer has been started before, start it again
            {
                timer.Start();
            }

            // Invoke the event to print the message that the airplane is taking off
            AirplaneEventArgs args = new AirplaneEventArgs(flightinfo, " taking off to destination: " + Destination + ", " + LocalTime);
            TakeOff?.Invoke(this, args);

            airplaneWindow.UpdateContext(this);
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

        /// <summary>
        /// Method that is called when the airplane is landing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Land(object sender, EventArgs e)
        {
            /// Check if the airplane is able to land, if not, display a message and return
            if (!canLand)
            {
                MessageBox.Show(Name + " is already on the ground");
                return;
            }
            // call the on landing method that sets the airplane canLand, altitude and stop the timer
            OnLanding();
            // Create a new instance of the AirplaneEventArgs and invoke the event that the airplane has landed
            AirplaneEventArgs args = new AirplaneEventArgs(this.ToString(), " has landed at " + Destination + " at: " + LocalTime);
            Landing?.Invoke(this, args);
            // set the destination to home
            Destination = "Home";
            // update the context of the airplane window
            airplaneWindow.UpdateContext(this);

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
        /// Method to subscribe to the altitude change event in the control tower.
        /// Passing in the control tower as a parameter to be able to subscribe to the event
        /// if the plane has already subscribed to the event, just return, else subscribe to the event
        /// </summary>
        /// <param name="controlTower"></param>
        public void SubscribeToAltitudeChange(ControlTower controlTower)
        {
            // Check if the event is already subscribed, if it is, return
            if (IsAltitudeChangeSubscribed)
            {
                return;
            }
            // Subscribe to the event
            controlTower.ChangeAltitudeRequested += ChangeAltitude;
        }
       

        /// <summary>
        /// Method to change the altitude of the airplane
        /// </summary>
        /// <param name="altitude"></param>
        public void ChangeAltitude(double altitude)
        {
            string msg;
            if (!canLand) // Check if the airplane is on the ground
            {
                msg = " is on the ground, it cannot change altitude";
            }
            else if (altitude < 0) // Check if the altitude is negative
            {
                msg = ": Altitude cannot be negative";
            } else if (altitude > 14000) // Check if the altitude is higher than 14000
            {
                msg = ": Altitude cannot be higher than 14000";
            } else if (canLand && altitude <= 500) // Check if the airplane is in the air and the altitude is lower than 500
            {
                msg = " is in the air, the requested altitude is to low.";
            } else // else, it is ok to change the altitude
            {
                Altitude = altitude;
                msg = " has changed altitude to: " + Altitude;
            }
            // Create a new instance of the AirplaneEventArgs and invoke the event that the altitude has changed. The control tower is listening to this event to display the message there
            AirplaneEventArgs args = new AirplaneEventArgs(this.ToString(), msg);
            // Invoke the event
            AltitudeChanged?.Invoke(this, args);
            // update the context of the airplane window
            airplaneWindow?.UpdateContext(this);
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

        /// <summary>
        /// Metod to print the airplane object as name and flight id
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name + ", " + FlightID;
        }
    }

}