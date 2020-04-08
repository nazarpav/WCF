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
        ConvertedUnits LinearMeasure(double meters);
        ConvertedUnits CelsiusToFahrenheit(double c);
        ConvertedUnits FahrenheitToCelsius(double f);
    }
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
