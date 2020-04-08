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

    public class DiskInfo : ITask1, ITask2
    {
        public string[] GetDiskInfo(string Path_)
        {
            List<string> ress = new List<string>();
            try
            {
                ress.Add(@"Content for path: " + Path_);
                foreach (var item in Directory.GetDirectories(Path_))
                {
                    ress.Add(Path.GetDirectoryName(item) + " - Directory");
                }
                foreach (var item in Directory.GetFiles(Path_))
                {
                    ress.Add(Path.GetFileName(item) + " - File");
                }
            }
            catch
            {
                return null;
            }
            return ress.ToArray();
        }
        public string FreeSpace(string Path)
        {
            try
            {
                DriveInfo di = new DriveInfo(Path[0].ToString());
                return "AvailableFreeSpace(byte): " + di.TotalFreeSpace + "";
            }
            catch
            {
                return "Wrong disk!";
            }
        }
        public string TotalSpace(string Path)
        {
            try
            {
                DriveInfo di = new DriveInfo(Path[0].ToString());
                return "TotalFreeSpace(byte): " + di.TotalSize + "";
            }
            catch
            {
                return "Wrong disk!";
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost sh2 = new ServiceHost(typeof(DiskInfo));
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
