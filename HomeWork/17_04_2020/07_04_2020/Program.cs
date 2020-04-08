using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace _07_04_2020
{
    [ServiceContract]
    public interface ITask1
    {
        [OperationContract]
        string[] GetDiskInfo(string Path);
    }
    [ServiceContract]
    public interface ITask2
    {
        [OperationContract]
        string TotalSpace(string Path);
        [OperationContract]
        string FreeSpace(string Path);
    }
    class Program
    {
        static void Main(string[] args)
        {
            ChannelFactory<ITask1> factory1 = new ChannelFactory<ITask1>(
                new WSHttpBinding(),
                new EndpointAddress("http://localhost/DiskInfo.ITask1"));
            ChannelFactory<ITask2> factory2 = new ChannelFactory<ITask2>(
                new WSHttpBinding(),
                new EndpointAddress("http://localhost/DiskInfo.ITask2"));

            ITask1 channel1 = factory1.CreateChannel();
            ITask2 channel2 = factory2.CreateChannel();

            string[] result_T1 = channel1.GetDiskInfo(@"E:\WCF\HomeWork\17_04_2020");
            string result_T2_2 = channel2.TotalSpace("ee");
            string result_T2_1 = channel2.FreeSpace("ee");
            Console.WriteLine(result_T2_2+"\n"+result_T2_1);
            Console.WriteLine("\n+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=\n");
            foreach (var item in result_T1)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Press <ENTER> to exit...\n");
            Console.ReadLine();

            factory1.Close();
            factory2.Close();
        }
    }
}
