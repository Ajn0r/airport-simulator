using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AirportSimulator
{
    public class ControlTower
    {
        private List<Airplane> airplanes = new List<Airplane>();
        private ListBox listBox;

        public ControlTower(ListBox listBox)
        {
            this.listBox = listBox;
        }
        public List<Airplane> Airplanes { get => airplanes; set => airplanes = value; }

        public void AddAirplane(Airplane airplane)
        {
            airplanes?.Add(airplane);
        }

        public void OrderLanding(int index)
        {
            throw new NotImplementedException();
        }

        public void OrderTakeOff(int index)
        {
            throw new NotImplementedException();
        }

        public void OrderChangeAltitude(int index, int altitude)
        {
            throw new NotImplementedException();
        }

        public void OnDisplayInfo(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
