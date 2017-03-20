using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using AsymptoticAverage;

namespace UtilitiesService
{
    public class Service1 : IService1
    {
        public double CalcAsymptoticAverage(double[] values)
        {
            AsymptoticAverageClass avg = new AsymptoticAverageClass();
            return avg.calcAverage(values.ToList());
        }

        public double AddValueToAsymptoticAverage(double value, double average)
        {
            AsymptoticAverageClass avg = new AsymptoticAverageClass();
            return avg.addValueToAverage(value, average);
        }
    }
}
