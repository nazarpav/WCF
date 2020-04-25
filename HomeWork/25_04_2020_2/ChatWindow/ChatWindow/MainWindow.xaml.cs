using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ChatWindow.ServiceReference1;

namespace ChatWindow
{
    public delegate void CallbackRecieve(string msg);
    public delegate void CallbackUsersRefresh(string[] msg);
    class CallbackHandler : IChatCallback
    {
        static public event CallbackRecieve OnRecieve;
        static public event CallbackUsersRefresh OnUsersRefresh;
        public void RecieveMessage(string msg)
        {
            OnRecieve(msg);
        }

        public void UsersRefresh(string[] msg)
        {
            OnUsersRefresh(msg);
        }
    }

    public partial class MainWindow : Window
    {
        static InstanceContext instance = new InstanceContext(new CallbackHandler());
        ChatClient proxy = new ChatClient(instance);
        bool isLogin = false;
        public MainWindow()
        {
            InitializeComponent();
            CallbackHandler.OnRecieve += AddMessageToList;
            CallbackHandler.OnUsersRefresh += UsersRefresh;
        }

        private void UsersRefresh(string[] users)
        {
            Users.Items.Clear();
            foreach (var item in users)
            {
                Users.Items.Add(item);
            }
        }
        // Login
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            var res = proxy.Login(loginTb.Text);
            if (res == null)
                MessageBox.Show("User is already exist.");
            foreach (var item in res)
            {
                Users.Items.Add(item);
            }
            isLogin = true;
        }

        // Send message
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(!isLogin)
            {
                MessageBox.Show("Login!!");
                return;
            }
            proxy.SendMessasge(loginTb.Text, msgTb.Text);

            TextBlock tb = new TextBlock()
            {
                HorizontalAlignment = HorizontalAlignment.Right,
                FontWeight = FontWeights.Bold,
                Text = msgTb.Text
            };
            msgList.Items.Add(tb);
        }

        private void AddMessageToList(string msg)
        {
            msgList.Items.Add(msg);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            proxy.Logout(loginTb.Text);
            isLogin = false;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            proxy.Logout(loginTb.Text);
        }
    }
}
