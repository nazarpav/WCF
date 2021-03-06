﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ConverterSL
{
    [ServiceContract]
    public interface IConverter
    {
        [OperationContract]
        ConvertedUnits LinearMeasure(double meters);
        [OperationContract]
        ConvertedUnits CelsiusToFahrenheit(double c);
        [OperationContract]
        ConvertedUnits FahrenheitToCelsius(double f);
    }
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
}
