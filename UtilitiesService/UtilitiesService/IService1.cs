using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace UtilitiesService
{
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        [WebInvoke(Method="POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        double CalcAsymptoticAverage(double[] values);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        double AddValueToAsymptoticAverage(double value, double average);

    }

}
