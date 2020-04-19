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
    public delegate void CallbackDelegateNewPalyers(string[] msg);
    public delegate void CallbackDelegateGetPlayingField(bool?[][] msg);
    public delegate void CallbackGameSuccessfulEnded(bool msg);
    public delegate void CallbackGameStarted(bool msg);
    class CallbackHandler : IGameCallback
    {
        static public event CallbackDelegateNewPalyers OnGetNewPalyers;
        static public event CallbackGameSuccessfulEnded OnGameSuccessfulEnded;
        static public event CallbackDelegateGetPlayingField OnServerPlayingField;
        static public event CallbackGameStarted OnGameStarted;

        public void CallbackGameStarted(bool msg)
        {
            OnGameStarted(msg);
        }

        public void CallbackGameSuccessfulEnded(bool isWin)
        {
            OnGameSuccessfulEnded(isWin);
        }
        public void CallbackGetNewPalyers(string[] msg)
        {
            OnGetNewPalyers(msg);
        }
        public void CallbackGetServerPlayingField(bool?[][] msg)
        {
            OnServerPlayingField(msg);
        }
    }

    public partial class MainWindow : Window
    {
        static InstanceContext instance = new InstanceContext(new CallbackHandler());
        GameClient proxy = new GameClient(instance);
        bool? isMove = null;
        bool isButtonPressed = false;
        public MainWindow()
        {
            InitializeComponent();
            CallbackHandler.OnGetNewPalyers += RefreshPlayers;
            CallbackHandler.OnServerPlayingField += UpdatePlayingField;
            CallbackHandler.OnGameSuccessfulEnded += GameSuccessfulEnded;
            CallbackHandler.OnGameStarted += GameStarted;
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            if (isMove == null)
            {
                MessageBox.Show("Connect to game!!!");
                return;
            }
            if (isMove.Value)
            {
                return;
            }
            isButtonPressed = true;
            isMove = true;
            if (isMove.Value)
            {
                isMove_.Content = "Opponent move";
            }
            else
            {
                isMove_.Content = "Your move";
            }
            switch (((Button)sender).Name)
            {
                case "_00":
                    proxy.PlayerMove(YourName.Text, 0, 0);
                    break;
                case "_01":
                    proxy.PlayerMove(YourName.Text, 0, 1);
                    break;
                case "_02":
                    proxy.PlayerMove(YourName.Text, 0, 2);
                    break;
                ///////////////
                case "_10":
                    proxy.PlayerMove(YourName.Text, 1, 0);
                    break;
                case "_11":
                    proxy.PlayerMove(YourName.Text, 1, 1);
                    break;
                case "_12":
                    proxy.PlayerMove(YourName.Text, 1, 2);
                    break;
                ///////////////
                case "_20":
                    proxy.PlayerMove(YourName.Text, 2, 0);
                    break;
                case "_21":
                    proxy.PlayerMove(YourName.Text, 2, 1);
                    break;
                case "_22":
                    proxy.PlayerMove(YourName.Text, 2, 2);
                    break;
                default:
                    MessageBox.Show("Error Invalid press button☺");
                    break;
            }
        }
        private void RefreshPlayers(string[] msg)
        {
            Players.Items.Clear();
            foreach (var item in msg)
            {
                if (item != YourName.Text)
                {
                    Players.Items.Add(item);
                }
            }
        }
        private void GameStarted(bool msg)
        {
            Console.WriteLine((char)7);
            MessageBox.Show("Game Started!!");
            isMove = msg;
            if (isMove.Value)
            {
                isMove_.Content = "Opponent move";
            }
            else
            {
                isMove_.Content = "Your move";
            }
        }
        private void GameSuccessfulEnded(bool isWin)
        {
            if (isWin)
            {
                MessageBox.Show("You Win");
                Win.Content = int.Parse(Win.Content.ToString()) + 1;
            }
            else
            {
                MessageBox.Show("You Lose");
                Lose.Content = int.Parse(Lose.Content.ToString()) + 1;
            }
        }
        private void UpdatePlayingField(bool?[][] field)
        {
            if (!isButtonPressed)
            {
                isMove = false;
            }
            isButtonPressed = false;
            if (isMove.Value)
            {
                isMove_.Content = "Opponent move";
            }
            else
            {
                isMove_.Content = "Your move";
            }
            //true X
            //false O
            if (field[0][0] == true)
            {
                _00.Content = "X";
            }
            else if (field[0][0] == false)
            {
                _00.Content = "O";
            }
            else
            {
                _00.Content = "";
            }
            if (field[0][1] == true)
            {
                _01.Content = "X";
            }
            else if (field[0][1] == false)
            {
                _01.Content = "O";
            }
            else
            {
                _01.Content = "";
            }
            if (field[0][2] == true)
            {
                _02.Content = "X";
            }
            else if (field[0][2] == false)
            {
                _02.Content = "O";
            }
            else
            {
                _02.Content = "";
            }

            if (field[1][0] == true)
            {
                _10.Content = "X";
            }
            else if (field[1][0] == false)
            {
                _10.Content = "O";
            }
            else
            {
                _10.Content = "";
            }
            if (field[1][1] == true)
            {
                _11.Content = "X";
            }
            else if (field[1][1] == false)
            {
                _11.Content = "O";
            }
            else
            {
                _11.Content = "";
            }
            if (field[1][2] == true)
            {
                _12.Content = "X";
            }
            else if (field[1][2] == false)
            {
                _12.Content = "O";
            }
            else
            {
                _12.Content = "";
            }

            if (field[2][0] == true)
            {
                _20.Content = "X";
            }
            else if (field[2][0] == false)
            {
                _20.Content = "O";
            }
            else
            {
                _20.Content = "";
            }
            if (field[2][1] == true)
            {
                _21.Content = "X";
            }
            else if (field[2][1] == false)
            {
                _21.Content = "O";
            }
            else
            {
                _21.Content = "";
            }
            if (field[2][2] == true)
            {
                _22.Content = "X";
            }
            else if (field[2][2] == false)
            {
                _22.Content = "O";
            }
            else
            {
                _22.Content = "";
            }
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //EndGame
            EndGame();
        }
        private void EndGame()
        {
            try
            {
                Task.Run(() => proxy.EndGame(Dispatcher.Invoke(() => YourName.Text)));
                isMove = null;
                isButtonPressed = false;
                isMove_.Content = "_";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                proxy.Logout(YourName.Text);
                YourName.IsEnabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            //connect to server
            var res = proxy.Login(YourName.Text);
            if (res == null)
            {
                MessageBox.Show("User is already exist.");
                return;
            }
            RefreshPlayers(res);
            YourName.IsEnabled = false;
        }
        private void StartGame()
        {
            try
            {
                Task.Run(() => proxy.StartGame(Dispatcher.Invoke(() => YourName.Text), Dispatcher.Invoke(() => Players.SelectedItem.ToString())));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            //connect to game
            StartGame();
        }
    }
}
