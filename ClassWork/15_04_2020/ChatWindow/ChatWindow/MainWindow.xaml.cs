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
    public delegate void CallbackDelegate(string msg);
    class CallbackHandler : IChatCallback
    {
        static public event CallbackDelegate OnRecieve;
        public void RecieveMessage(string msg)
        {
            OnRecieve(msg);
        }
    }

    public partial class MainWindow : Window
    {
        static InstanceContext instance = new InstanceContext(new CallbackHandler());
        ChatClient proxy = new ChatClient(instance);

        public MainWindow()
        {
            InitializeComponent();
            CallbackHandler.OnRecieve += AddMessageToList;
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
        }

        // Send message
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
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
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            proxy.SendPrivate(loginTb.Text, msgTb.Text, private_name.Text);
            TextBlock tb = new TextBlock()
            {
                HorizontalAlignment = HorizontalAlignment.Right,
                FontWeight = FontWeights.Bold,
                Text = msgTb.Text
            };
            msgList.Items.Add(tb);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            proxy.Logout(loginTb.Text);
        }
    }
}
