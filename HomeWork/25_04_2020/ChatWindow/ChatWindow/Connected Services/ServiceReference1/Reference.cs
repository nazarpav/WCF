﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ChatWindow.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IChat", CallbackContract=typeof(ChatWindow.ServiceReference1.IChatCallback))]
    public interface IChat {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IChat/Login", ReplyAction="http://tempuri.org/IChat/LoginResponse")]
        string[] Login(string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IChat/Login", ReplyAction="http://tempuri.org/IChat/LoginResponse")]
        System.Threading.Tasks.Task<string[]> LoginAsync(string name);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChat/SendMessasge")]
        void SendMessasge(string userName, string msg);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChat/SendMessasge")]
        System.Threading.Tasks.Task SendMessasgeAsync(string userName, string msg);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChat/SendPrivate")]
        void SendPrivate(string NameFrom, string msg, string NameTo);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChat/SendPrivate")]
        System.Threading.Tasks.Task SendPrivateAsync(string NameFrom, string msg, string NameTo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IChat/Logout", ReplyAction="http://tempuri.org/IChat/LogoutResponse")]
        void Logout(string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IChat/Logout", ReplyAction="http://tempuri.org/IChat/LogoutResponse")]
        System.Threading.Tasks.Task LogoutAsync(string name);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IChatCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChat/RecieveMessage")]
        void RecieveMessage(string msg);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IChat/UsersRefresh")]
        void UsersRefresh(string[] msg);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IChatChannel : ChatWindow.ServiceReference1.IChat, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ChatClient : System.ServiceModel.DuplexClientBase<ChatWindow.ServiceReference1.IChat>, ChatWindow.ServiceReference1.IChat {
        
        public ChatClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public ChatClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public ChatClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public ChatClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public ChatClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public string[] Login(string name) {
            return base.Channel.Login(name);
        }
        
        public System.Threading.Tasks.Task<string[]> LoginAsync(string name) {
            return base.Channel.LoginAsync(name);
        }
        
        public void SendMessasge(string userName, string msg) {
            base.Channel.SendMessasge(userName, msg);
        }
        
        public System.Threading.Tasks.Task SendMessasgeAsync(string userName, string msg) {
            return base.Channel.SendMessasgeAsync(userName, msg);
        }
        
        public void SendPrivate(string NameFrom, string msg, string NameTo) {
            base.Channel.SendPrivate(NameFrom, msg, NameTo);
        }
        
        public System.Threading.Tasks.Task SendPrivateAsync(string NameFrom, string msg, string NameTo) {
            return base.Channel.SendPrivateAsync(NameFrom, msg, NameTo);
        }
        
        public void Logout(string name) {
            base.Channel.Logout(name);
        }
        
        public System.Threading.Tasks.Task LogoutAsync(string name) {
            return base.Channel.LogoutAsync(name);
        }
    }
}
