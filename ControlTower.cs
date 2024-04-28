using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using Microsoft.VisualBasic;

namespace AirportSimulator
{
    public class ControlTower
    {
        private List<Airplane> airplanes = new List<Airplane>();
        private ListView listMsg;

        public ControlTower(ListView listMsg)
        {
            this.listMsg = listMsg;
        }
        public List<Airplane> Airplanes { get => airplanes; set => airplanes = value; }

        // delegate and event for changing the altitude that the airplane will be listening to
        public delegate void ChangeAltitudeDelegate(double altitude);
        public event ChangeAltitudeDelegate ChangeAltitudeRequested;

        /// <summary>
        /// Method to add an airplane to the list of airplanes in the control tower
        /// </summary>
        /// <param name="airplane"></param>
        public void AddAirplane(Airplane airplane)
        {
            airplanes?.Add(airplane);
            
        }

        /// <summary>
        /// Method to open a window to change the altitude of an airplane
        /// </summary>
        /// <param name="airplane"></param>
        public void OpenAltWindow(Airplane airplane)
        {
            // Using a Inputbox to get the new altitude, found insperation here: https://learn.microsoft.com/en-us/dotnet/api/microsoft.visualbasic.interaction.inputbox?view=net-8.0
            string newAltitude = Interaction.InputBox("Enter the new altitude", "Change Altitude", airplane.Altitude.ToString());
            if (double.TryParse(newAltitude, out double altitude))
            {
                // Check if the plane is already subscribed to the event, if not, subscribe to it
                if (!airplane.IsAltitudeChangeSubscribed)
                {
                    airplane.SubscribeToAltitudeChange(this);
                    airplane.IsAltitudeChangeSubscribed = true;
                }
                // Subscribe to the Altitude change event in the airplane class when the window is opened
                airplane.AltitudeChanged += OnDisplayInfo;
                // Invoke the event with the new altitude as a parameter, the airplane class is listening to this event and will change the altitude
                ChangeAltitudeRequested?.Invoke(altitude);
            }
            // Remove the subscription to the event when the window is closed to prevent it from being called multiple times
            airplane.AltitudeChanged -= OnDisplayInfo;
        }


        /// <summary>
        /// Method to order an airplane to take off
        /// </summary>
        /// <param name="index"></param>
        public void OrderTakeOff(int index)
        {
            if (CheckIndex(index))
            {
                Airplane airplane = airplanes[index];
                // Check if the event is already subscribed, if not, subscribe to it
                if (!airplane.IsTakeOffSubscribed)
                {
                    // subscribe to event, had to be put up here in order for it to work
                    airplane.TakeOff += OnDisplayInfo;
                    airplane.Landing += OnDisplayInfo;
                    airplane.IsTakeOffSubscribed = true;
                }
                // Check if the airplane can take off, if it can land, it is in the air and cannot take off
                if (airplane.canLand)
                {
                    listMsg.Items.Add(airplane.ToString() + " is in the air already, please wait until it has landed");
                }
                else if (airplane.OpenWindow) // if the airplane window is open, display a message
                {
                    listMsg.Items.Add(airplane.ToString() + " is already sent to the runway");
                } 
                else // else, it is on the ground and can take off
                {
                    listMsg.Items.Add(airplane.ToString() + ", heading for " + airplanes[index].Destination + " is sent to the runway!");
                    airplane.OnTakeOff();
                }
            } else // if the index is not valid, display a message
            {
                MessageBox.Show("Cannot find selected airplane");
            }
        }


        /// <summary>
        /// Method to check if the index is valid
        /// </summary>
        /// <param name="index"></param>
        /// <exception cref="NotImplementedException"></exception>
        private bool CheckIndex(int index)
        {
            bool ok = false;
            if (index >= 0 && index < airplanes.Count)
            {
                ok = true;
            }
            return ok;
        }

        /// <summary>
        /// Method to order an airplane to change altitude
        /// </summary>
        /// <param name="index"></param>
        public void OrderChangeAltitude(int index)
        {
            if (CheckIndex(index))
            {
                // Get the airplane at the index
                Airplane airplane = airplanes[index];
                // Open the window to change the altitude and pass the airplane object to it
                OpenAltWindow(airplane);
            }
        }

        /// <summary>
        /// Method that is handeling all the printing of the information that is displayed in the listbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnDisplayInfo(object sender, AirplaneEventArgs e)
        {
            // Display the information in the listbox
            listMsg.Items.Add(e.Flight + e.Message);

        }
    }
}
