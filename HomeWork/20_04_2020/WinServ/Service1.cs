using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using ConverterSL;

namespace WinServ
{
    public partial class Service1 : ServiceBase
    {
        static ServiceHost service = null;
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            if (service != null)
            {
                service.Close();
            }
            service = new ServiceHost(typeof(Converter_));
            service.Open();
        }

        protected override void OnStop()
        {
            if (service != null)
            {
                service.Close();
                service = null;
            }
        }
    }
}
