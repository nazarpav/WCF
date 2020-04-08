using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace _07_04_2020
{
    [ServiceContract]
    public interface IGetFolders
    {
        [OperationContract]
        string[] Add(string Path);
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
            ChannelFactory<IGetFolders> factory2 = new ChannelFactory<IGetFolders>(
                new WSHttpBinding(),
                new EndpointAddress("http://localhost/GetFolders"));
            ChannelFactory<ITask2> factory = new ChannelFactory<ITask2>(
               new WSHttpBinding(),
               new EndpointAddress("http://localhost/Task2/t1"));

            ITask2 channel = factory.CreateChannel();
            IGetFolders channel2 = factory2.CreateChannel();

            string result = channel.FreeSpace("ee");
            string result2 = channel.TotalSpace("ee");
            Console.WriteLine("Free => "+result+"\nTotal => "+result2);
            var res = channel2.Add(@"E:\WCF\ClassWork");
            foreach (var item in res)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Press <ENTER> to exit...\n");
            Console.ReadLine();

            factory.Close();
        }
    }
}
