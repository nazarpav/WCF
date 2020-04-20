using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ConverterSL
{
    public class Service1 : IConverter
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
}
