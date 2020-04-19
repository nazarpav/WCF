using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Converter
{
    [DataContract]
    public class ConvertedUnits
    {
        [DataMember]
        public double inch;
        [DataMember]
        public double foot;
        [DataMember]
        public double yard;
        [DataMember]
        public double Celsius;
        [DataMember]
        public double Fahrenheit;
    }
    [ServiceContract]
    public interface Converted
    {
        [OperationContract]
        ConvertedUnits LinearMeasure(double meters);
        [OperationContract]
        ConvertedUnits CelsiusToFahrenheit(double c);
        [OperationContract]
        ConvertedUnits FahrenheitToCelsius(double f);
    }
    class Converter_ : Converted
    {
        public ConvertedUnits CelsiusToFahrenheit(double c)
        {
            return new ConvertedUnits() { Fahrenheit = (c * 9 / 5) + 32 };
        }
        public ConvertedUnits FahrenheitToCelsius(double f)
        {
            return new ConvertedUnits() { Celsius = (f - 32) * 5 / 9 };
        }
        public ConvertedUnits LinearMeasure(double meters)
        {
            double yard_ = meters * 1.094;
            double foot_ = (yard_ - Math.Truncate(yard_)) * 3;
            yard_ = Math.Truncate(yard_);
            double inch_ = (foot_ - Math.Truncate(foot_)) * 12;
            foot_ = Math.Truncate(foot_);
            return new ConvertedUnits() { yard = yard_, foot = foot_, inch = inch_ };
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = null;
            try
            {
                host = new ServiceHost(typeof(Converter_));
                host.Open();
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
            }
            finally
            {
                host.Close();
            }
        }
    }
}
