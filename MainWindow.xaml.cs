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
            UpdateGUI();
            Application.Current.MainWindow = this;
        }
        private void UpdateGUI()
        {
            FillListBox();
        }
        private void TestData()
        {
            // Planes created for testing
            Airplane airplane1 = new Airplane("Boeing 747", "BA123", "London", 2.5);
            Airplane airplane2 = new Airplane("Airbus A380", "AF456", "Paris", 3.5);
            Airplane airplane3 = new Airplane("Boeing 737", "SK789", "Stockholm", 1.5);
            Airplane airplane4 = new Airplane("Boeing 747", "BA123", "London", 2.5);
            Airplane airplane5 = new Airplane("Airbus A380", "AF456", "Paris", 3.5);
            Airplane airplane6 = new Airplane("Boeing 737", "SK789", "Stockholm", 1.5);
            controlTower.AddAirplane(airplane1);
            controlTower.AddAirplane(airplane2);
            controlTower.AddAirplane(airplane3);
            controlTower.AddAirplane(airplane4);
            controlTower.AddAirplane(airplane5);
            controlTower.AddAirplane(airplane6);
        }
        private void FillListBox()
        {
            lstPlanes.Items.Clear();
            foreach (Airplane airplane in controlTower.Airplanes)
            {
                lstPlanes.Items.Add(airplane);
            }
        }
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
        private void DisplayInfo(object sender, AirplaneEventArgs e)
        {
            lstMessages.Items.Add(e.Flight + e.Message);
        }   

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


        private void btnChangeAltitude_Click(object sender, RoutedEventArgs e)
        {
            if (lstPlanes.SelectedItem != null)
            {
                OpenAltitudeWindow();
            }
            else
            {
                MessageBox.Show("Please select an airplane");
            }
        }

        private void OpenAltitudeWindow()
        {
            Airplane airplane = (Airplane)lstPlanes.SelectedItem;
            Window altitudeWindow = new Window
            {
                Title = "Change Altitude",
                Width = 250,
                Height = 150,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };

            StackPanel stackPanel = new StackPanel();
                
            Label altLbl = new Label { Content = "Enter the new altitude", Margin = new Thickness(10) };
            TextBox txtAltitude = new TextBox { Name = "txtAltitude", Margin = new Thickness(10), HorizontalAlignment = HorizontalAlignment.Center, Width = 75 };
    
            stackPanel.Children.Add(altLbl);
            stackPanel.Children.Add(txtAltitude);
            StackPanel buttonPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,

                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(10)
            };

            Button btnOk = new Button
            {
                Content = "OK",
                Margin = new Thickness(5)
            };
            btnOk.Click += (sender, e) =>
            {
                // Hämta den nya höjden från textboxen och ändra höjden
                if (double.TryParse(txtAltitude.Text, out double newAltitude))
                {
                    airplane.Altitude = newAltitude;
                }
                altitudeWindow.Close();
            };
            buttonPanel.Children.Add(btnOk);

            Button btnCancel = new Button
            {
                Content = "Cancel",
                Margin = new Thickness(5)
            };
            btnCancel.Click += (sender, e) =>
            {
                altitudeWindow.Close();
            };
            buttonPanel.Children.Add(btnCancel);

            stackPanel.Children.Add(buttonPanel);

            // Lägg till innehåll till fönstret
            altitudeWindow.Content = stackPanel;

            // Visa fönstret
            altitudeWindow.ShowDialog();

        }

        private Airplane? ReadAndSavePlaneInfo()
        {
            if (
                string.IsNullOrEmpty(txtName.Text) ||
                string.IsNullOrEmpty(txtFlightID.Text) ||
                string.IsNullOrEmpty(txtDestination.Text)||
                !double.TryParse(txtFlightTime.Text, out double result) || result <= 0
                )
            {
                MessageBox.Show("Please fill in all the fields");
                return null;
            } 
            else
            {
                Airplane airplane = new Airplane(txtName.Text, txtFlightID.Text, txtDestination.Text, double.Parse(txtFlightTime.Text));
                return airplane;
            }
        }
    }
}