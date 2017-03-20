using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroMQ;
using System.Web.Script.Serialization;

namespace ZeroMQTester
{
    class Program
    {
        static void Main(string[] args)
        {
            ZeroMQLCient(null);
        }

        public static void ZeroMQLCient(string[] args)
        {
            //
            // Hello World client
            //
            // Author: metadings
            //

            if (args == null || args.Length < 1)
            {
                Console.WriteLine();
                Console.WriteLine("Usage: ./{0} HWClient [Endpoint]", AppDomain.CurrentDomain.FriendlyName);
                Console.WriteLine();
                Console.WriteLine("    Endpoint  Where HWClient should connect to.");
                Console.WriteLine("              Default is tcp://127.0.0.1:5555");
                Console.WriteLine();
                args = new string[] { "tcp://127.0.0.1:5555" };
            }

            string endpoint = args[0];
           // string endpoint = "ipc:///tmp/zeromq";

            // Create
            using (var context = new ZContext())
            using (var requester = new ZSocket(context, ZSocketType.REQ))
            {
                // Connect
                requester.Connect(endpoint);

                for (int n = 0; n < 10; ++n)
                {
                    string methodName = "AddValueToAsymptoticAverage";

                    double[] data = {0.3, 0.2};
                    //string strData = data.ToString();               
                    
                    RequestData requestData = new RequestData();
                    requestData.Method = methodName;
                    requestData.Data = data;
                    var serializer = new JavaScriptSerializer();
                    string requestJson = serializer.Serialize(requestData);

                    Console.Write("Sending To Method: {0}… data: {1} $$$ ", methodName, data);

                    // Send Data
                    requester.Send(new ZFrame(requestJson));

                    // Receive Result
                    using (ZFrame reply = requester.ReceiveFrame())
                    {
                        string strResult = reply.ReadString();
                        Double Mresult = serializer.Deserialize<Double>(strResult);
                        double doubleResult = Double.Parse(strResult);
                        Console.WriteLine(" Received, Method: {0}, sendData: {1}, resultData: {2}!", methodName, data.ToString(), doubleResult);
                    }
                }
            }
            Console.ReadLine();
        }
    }
}
