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
            controlTower = new ControlTower(lstPlanes);
        }
        private void UpdateGUI()
        {
            FillListBox();
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
        private void btnTakeOff_Click(object sender, RoutedEventArgs e)
        {

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