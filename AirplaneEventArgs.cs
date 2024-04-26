using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSimulator
{
    public class AirplaneEventArgs : EventArgs
    {
        public string Message { get; set; }
        public string Flight { get; set; }


        public AirplaneEventArgs(string flight, string message)
        {
            Flight = flight;
            Message = message;
        }
    }
}
