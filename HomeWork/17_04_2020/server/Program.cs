using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace server
{
    [ServiceContract]
    public interface IGetFolders
    {
        [OperationContract]
        string[] Add(string Path);
    }
    public interface ITask2
    {
        [OperationContract]
        string TotalSpace(string Path);
        [OperationContract]
        string FreeSpace(string Path);
    }

    //[ServiceContract(Name = "MyServiceName")]
    public class GetFolders : IGetFolders
    {
        //[OperationContract(Name = "MyOperationName")]
        public string[] Add(string Path)
        {
            string[] res=null;
            try
            {
                res = Directory.GetDirectories(Path);
                res =res.Concat(Directory.GetFiles(Path)).ToArray();
            }
            catch
            {
                return null;
            }
            return res;
        }
    }
    public class Task2 : ITask2
    {
        public string FreeSpace(string Path)
        {
            DriveInfo di = new DriveInfo(Path[0].ToString());
            return di.AvailableFreeSpace+"";
        }

        public string TotalSpace(string Path)
        {
            DriveInfo di = new DriveInfo(Path[0].ToString());
            return di.TotalFreeSpace + "";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost sh2 = new ServiceHost(typeof(GetFolders));
            //ServiceHost sh2 = new ServiceHost(typeof(Task2));

            //sh.AddServiceEndpoint(
            //  typeof(IMyMath),              // [C]ontract
            //  new WSHttpBinding(),          // [B]inding
            //  "http://localhost/IGetFolders");   // [A]ddress

            //[protocol]://[ipAddress, domainName]/[URI]

            sh2.Open();
            Console.WriteLine("Press <ENTER> to exit...\n");
            Console.ReadLine();
            sh2.Close(); // timeout (10)
        }
    }
}
