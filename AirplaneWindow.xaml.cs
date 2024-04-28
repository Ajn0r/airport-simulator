using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AirportSimulator
{
    /// <summary>
    /// Interaction logic for AirplaneWindow.xaml
    /// </summary>
    public partial class AirplaneWindow : Window
    {
        Airplane airplane;

        public AirplaneWindow(Airplane airplane)
        {
            InitializeComponent();
            this.airplane = airplane;
            this.Title = airplane.ToString();
            DataContext = airplane;
        }

        // Event handlers for the take off and land buttons clicked, the airplane class is listening to these events
        public event EventHandler TakeOffButtonClicked;
        public event EventHandler LandButtonClicked;

        /// <summary>
        /// Method to handle the event when the land button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LandButton_Click(object sender, RoutedEventArgs e)
        {
            // Invoke the event when the land button is clicked
            LandButtonClicked?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Method to handle the event when the take off button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TakeOffButton_Click(object sender, RoutedEventArgs e)
        {
            // Invoke the event when the take off button is clicked
            TakeOffButtonClicked?.Invoke(this, EventArgs.Empty);

        }

        /// <summary>
        /// Method to update the context of the airplane window with the airplane object that is passed in, an quick fix to make it work
        /// </summary>
        /// <param name="airplane"></param>
        public void UpdateContext(Airplane airplane)
        {
            txtDest.Text = airplane.Destination;
            txtStatus.Text = airplane.Status;
            txtAlt.Text = airplane.Altitude.ToString();

        }
    }
}
