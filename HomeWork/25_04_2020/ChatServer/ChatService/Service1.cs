using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ChatService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class Chat : IChat
    {
        private Dictionary<string, ICallback> clients = new Dictionary<string, ICallback>();
        public void SendMessasge(string userName, string msg)
        {
            foreach (var item in clients)
            {
                if (item.Key == userName)
                    continue;
                item.Value.RecieveMessage(userName + ": " + msg);
            }
        }

        public void Logout(string name)
        {
            clients.Remove(name);
        }

        public void SendPrivate(string NameFrom, string msg, string NameTo)
        {
            foreach (var item in clients)
            {
                if (item.Key == NameFrom || item.Key != NameTo)
                    continue;
                item.Value.RecieveMessage(NameFrom + ": " + msg + "  (Private)");
            }
        }

        string[] IChat.Login(string name)
        {
            if (clients.ContainsKey(name))
            {
                return null;
            }
            clients.Add(name, OperationContext.Current.GetCallbackChannel<ICallback>());
            foreach (var item in clients)
            {
                if (item.Key != name)
                {
                    item.Value.UsersRefresh(clients.Keys.ToArray());
                }
            }
            return clients.Keys.ToArray();
        }
    }
}
