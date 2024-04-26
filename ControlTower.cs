using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

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

        // public event EventHandler<AirplaneEventArgs> SendToRunway;

        public void AddAirplane(Airplane airplane)
        {
            airplanes?.Add(airplane); 
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

        public void OrderChangeAltitude(int index, int altitude)
        {
            throw new NotImplementedException();
        }

        public void OnDisplayInfo(object sender, AirplaneEventArgs e)
        {
            // Display the information in the listbox
            listMsg.Items.Add(e.Flight + e.Message);

        }
    }
}
