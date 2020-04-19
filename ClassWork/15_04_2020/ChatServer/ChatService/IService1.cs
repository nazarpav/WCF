using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ChatService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract(CallbackContract = typeof(ICallback))]
    public interface IChat
    {
        [OperationContract]
        string[] Login(string name);

        [OperationContract(IsOneWay = true)]
        void SendMessasge(string userName, string msg);

        [OperationContract(IsOneWay = true)]
        void SendPrivate(string NameFrom, string msg, string NameTo);

        [OperationContract]
        void Logout(string name);
    }

    public interface ICallback
    {
        [OperationContract(IsOneWay = true)]
        void RecieveMessage(string msg);
    }
}
