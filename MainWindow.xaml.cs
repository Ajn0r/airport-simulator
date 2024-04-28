using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AirportSimulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ControlTower controlTower;

        public MainWindow()
        {
            InitializeComponent();
            controlTower = new ControlTower(lstMessages);
            TestData();
        }
        private void UpdateGUI()
        {
            FillListBox();
        }

        /// <summary>
        /// Test data to fill the listbox with airplanes
        /// </summary>
        private void TestData()
        {
            // Planes created for testing
            Airplane airplane1 = new Airplane("Boeing 747", "BA123", "London", 2.5);
            Airplane airplane2 = new Airplane("Airbus A380", "AF456", "Paris", 3.5);
            Airplane airplane3 = new Airplane("Boeing 737", "SK789", "Stockholm", 1.5);
            controlTower.AddAirplane(airplane1);
            controlTower.AddAirplane(airplane2);
            controlTower.AddAirplane(airplane3);

            UpdateGUI();
        }

        /// <summary>
        /// Method to fill the listbox with the airplanes in the control tower
        /// </summary>
        private void FillListBox()
        {
            lstPlanes.Items.Clear();
            foreach (Airplane airplane in controlTower.Airplanes)
            {
                lstPlanes.Items.Add(airplane);
            }
        }

        /// <summary>
        /// Method to clear the textboxes
        /// </summary>
        private void ClearTextBoxes()
        {
            txtName.Text = "";
            txtFlightID.Text = "";
            txtDestination.Text = "";
            txtFlightTime.Text = "";
        }

        /// <summary>
        /// Method to handle when the controltower orders a plane to take off
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTakeOff_Click(object sender, RoutedEventArgs e)
        {
            // check that an airplane is selected
            if (lstPlanes.SelectedItem != null)
            {
                // controlTower.SendToRunway += DisplayInfo;
                controlTower.OrderTakeOff(lstPlanes.SelectedIndex);
            }
            else // if not, display a message
            {
                MessageBox.Show("Please select an airplane");
            }
        }
  
        /// <summary>
        /// Method to handle the add button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Airplane? airplane = ReadAndSavePlaneInfo();
            if (airplane != null)
            {
                controlTower.AddAirplane(airplane);
                ClearTextBoxes();
            }
            UpdateGUI();
        }

        /// <summary>
        /// Method to handle the change altitude button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChangeAltitude_Click(object sender, RoutedEventArgs e)
        {
            if (lstPlanes.SelectedItem != null)
            {
                int index = lstPlanes.SelectedIndex;
                
                controlTower.OrderChangeAltitude(index);
            }
            else
            {
                MessageBox.Show("Please select an airplane");
            }
        }
        
        /// <summary>
        /// Method to read the information from the textboxes and save it in an airplane object
        /// </summary>
        /// <returns></returns>
        private Airplane? ReadAndSavePlaneInfo()
        {
            // Validate the input
            if (
                string.IsNullOrEmpty(txtName.Text) ||
                string.IsNullOrEmpty(txtFlightID.Text) ||
                string.IsNullOrEmpty(txtDestination.Text))
            {
                MessageBox.Show("Please fill in all the fields");
                return null;
            } else if (!double.TryParse(txtFlightTime.Text, out double result) || result <= 0)
            {
                MessageBox.Show("Please enter a valid flight time");
                return null;
            }
            else
            {
                // Create an airplane object with the information from the textboxes
                Airplane airplane = new Airplane(txtName.Text, txtFlightID.Text, txtDestination.Text, double.Parse(txtFlightTime.Text));
                return airplane;
            }
        }
    }
}