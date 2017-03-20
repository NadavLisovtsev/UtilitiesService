using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZeroMQ;
using System.Web.Script.Serialization;
using AsymptoticAverage;



namespace ConsoleApplication1
{
    static class Program
    {
        static void Main(string[] args)
        {
            ZeroMQServer(null);
        }

        public static void ZeroMQServer(string[] args)
        {
            //
            // Hello World server
            //
            // Author: metadings
            //

            if (args == null || args.Length < 1)
            {
                Console.WriteLine();
                Console.WriteLine("Usage: ./{0} HWServer [Name]", AppDomain.CurrentDomain.FriendlyName);
                Console.WriteLine();
                Console.WriteLine("    Name   Your name. Default: World");
                Console.WriteLine();
                args = new string[] { "World" };
            }

            string name = args[0];
           

            // Create
            using (var context = new ZContext())
            using (var responder = new ZSocket(context, ZSocketType.REP))
            {
                // Bind
                responder.Bind("tcp://*:5555");

                while (true)
                {
                    // Receive
                    using (ZFrame request = responder.ReceiveFrame())
                    {
                        string requestData =  request.ReadString();
                        Console.WriteLine("Received {0}", requestData);

                        Tuple<string, object> methodAndData = extractMethodAndData(requestData);

                        double result = HandleMethod(methodAndData.Item1, methodAndData.Item2);

                        // Send
                        Console.WriteLine("Send {0}", result.ToString());
                        responder.Send(new ZFrame(result.ToString()));
                    }
                }
            }
        }

        private static Tuple<string, object> extractMethodAndData(string strRequestData)
        {
            var serializer = new JavaScriptSerializer();
            RequestData requestData = serializer.Deserialize<RequestData>(strRequestData);
            return new Tuple<string, object>(requestData.Method, requestData.Data);
        }

        private static double HandleMethod(string methodName, object data)
        {
            var method = typeof(Program).GetMethod(methodName, BindingFlags.Static | BindingFlags.NonPublic);
            return (double)method.Invoke(null, new object[]{data} );
        }

        private static double CalcAsymptoticAverage(object objData)
        {
            List<double> data = ObjToDoubleArray(objData);

            AsymptoticAverageClass avg = new AsymptoticAverageClass(); 
            return avg.calcAverage(data);
        }

        private static double AddValueToAsymptoticAverage(object objData)
        {     
            List<double> data = ObjToDoubleArray(objData);
            double val = data[0];
            double average = data[1];

            AsymptoticAverageClass avg = new AsymptoticAverageClass();
            return avg.addValueToAverage(val, average);
        }

        private static List<double> ObjToDoubleArray(object objData)
        {
            List<double> data = new List<double>();
            foreach (object obj in (object[])objData)
            {
                data.Add(Double.Parse(obj.ToString()));
            }
            return data;
        }

    }
}
