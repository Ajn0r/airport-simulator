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

        public event EventHandler TakeOffButtonClicked;
        public event EventHandler LandButtonClicked;

        private void LandButton_Click(object sender, RoutedEventArgs e)
        {
            LandButtonClicked?.Invoke(this, EventArgs.Empty);
        }

        private void TakeOffButton_Click(object sender, RoutedEventArgs e)
        {
            TakeOffButtonClicked?.Invoke(this, EventArgs.Empty);
    
           
        }
    }
}
