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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Client.Converter;

namespace Client
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() => Convert());
        }
        private void Convert()
        {
            using (ConvertedClient convertedClient = new ConvertedClient())
            {
                ConvertedUnits convertedUnits = Dispatcher.Invoke(() =>  convertedClient.LinearMeasure(double.Parse(LinearMeasure_before.Text)));
                Dispatcher.Invoke(()=>LinearMeasure_after.Text = "Yard: " + convertedUnits.yard + " | Foot: " + convertedUnits.foot + " | Inch: " + convertedUnits.inch);
                Dispatcher.Invoke(() => FahrenheitToCelsius_after.Text = convertedClient.FahrenheitToCelsius(double.Parse(FahrenheitToCelsius_before.Text)).Celsius.ToString());
                Dispatcher.Invoke(() => CelsiusToFahrenheit_after.Text = convertedClient.CelsiusToFahrenheit(double.Parse(CelsiusToFahrenheit_before.Text)).Fahrenheit.ToString());
            }
        }
    }
}
