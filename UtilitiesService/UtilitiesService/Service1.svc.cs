using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using UtilitiesService.AssymptoticAgent;

namespace UtilitiesService
{
    public class Service1 : IService1
    {
        public double CalcAsymptoticAverage(double[] values)
        {
            AsymptoticAverage avg = new AsymptoticAverage();
            return avg.calcAverage(values.ToList());
        }

        public double AddValueToAsymptoticAverage(double value, double average)
        {
            AsymptoticAverage avg = new AsymptoticAverage();
            return avg.addValueToAverage(value, average);
        }
    }
}
