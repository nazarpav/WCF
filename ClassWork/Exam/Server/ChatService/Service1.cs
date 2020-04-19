using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace ChatService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class Chat : IGame
    {
        private Dictionary<string, ICallback> ConnectedPalyers = new Dictionary<string, ICallback>();
        private List<Players> playersInGame = new List<Players>();
        private List<PairPlayer> queuePlayers = new List<PairPlayer>();
        public void PlayerMove(string name, int x, int y)
        {
            foreach (var item in playersInGame)
            {
                if (item.Player1 == name)
                {
                    item.PlayerField[x][y] = true;
                    item.NotifyPlayersNewField();
                }
                else if (item.Player2 == name)
                {
                    item.PlayerField[x][y] = false;
                    item.NotifyPlayersNewField();
                }
                if (CheckWin(item.PlayerField))
                {
                    bool res = item.Player1 == name;
                    item.NotifyPlayer1IsWin(res);
                    item.NotifyPlayer2IsWin(!res);
                    ResetPlayerField(item);
                }
            }
        }
        private bool CheckWin(bool?[][] Field)
        {
            return Field[0][0] != null && Field[0][0] == Field[0][1] && Field[0][0] == Field[0][2] ||
                Field[1][0] != null && Field[1][0] == Field[1][1] && Field[1][0] == Field[1][2] ||
                Field[2][0] != null && Field[2][0] == Field[2][1] && Field[2][0] == Field[2][2] ||
                ///////////////////////
                Field[0][0] != null && Field[0][0] == Field[1][0] && Field[0][0] == Field[2][0] ||
                Field[0][1] != null && Field[0][1] == Field[1][1] && Field[0][1] == Field[2][1] ||
                Field[0][2] != null && Field[0][2] == Field[1][2] && Field[0][2] == Field[2][2] ||
                ///////////////////////
                Field[0][0] != null && Field[0][0] == Field[1][1] && Field[0][0] == Field[2][2] ||
                Field[0][2] != null && Field[0][2] == Field[1][1] && Field[0][2] == Field[2][0];
        }
        private void ResetPlayerField(Players players, bool? value = null)
        {
            if (players == null)
            {
                return;
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    players.PlayerField[i][j] = value;
                }
            }
            players.NotifyPlayersNewField();
        }
        public void Logout(string name)
        {
            if (ConnectedPalyers.ContainsKey(name))
            {
                ConnectedPalyers.Remove(name);
            }
            else
            {
                Players playerToDelete = null;
                foreach (var item in playersInGame)
                {
                    if (item.Player1 == name || item.Player2 == name)
                    {
                        playerToDelete = item;
                        if (item.Player1 != name)
                        {
                            ConnectedPalyers.Add(item.Player1, item.Callback1);
                        }
                        else
                        {
                            ConnectedPalyers.Add(item.Player2, item.Callback2);
                        }
                    }
                }
                playersInGame.Remove(playerToDelete);
                foreach (var item in playersInGame)
                {
                    item.Callback1.CallbackGetNewPalyers(ConnectedPalyers.Keys.ToArray());
                    item.Callback2.CallbackGetNewPalyers(ConnectedPalyers.Keys.ToArray());
                }
            }
            NotifyPlayers();
        }

        string[] IGame.Login(string name)
        {
            if (ConnectedPalyers.ContainsKey(name))
            {
                return null;
            }
            ConnectedPalyers.Add(name, OperationContext.Current.GetCallbackChannel<ICallback>());
            NotifyPlayers(name);
            return ConnectedPalyers.Keys.ToArray();
        }

        public void EndGame(string name)
        {
            Players playersToDelete = null;
            foreach (var item in playersInGame)
            {
                if (item.Player1 == name || item.Player2 == name)
                {
                    playersToDelete = item;
                    ConnectedPalyers.Add(item.Player1, item.Callback1);
                    ConnectedPalyers.Add(item.Player2, item.Callback2);
                    break;
                }
            }
            ResetPlayerField(playersToDelete);
            playersInGame.Remove(playersToDelete);
            NotifyPlayers();
        }

        void IGame.StartGame(string PlayerName, string nameToGame)
        {
            foreach (var item in queuePlayers)
            {
                if (item.Player1 == nameToGame && item.Player2 == PlayerName)
                {
                    var p = new Players(nameToGame, PlayerName, ConnectedPalyers.Where(q => q.Key == nameToGame).FirstOrDefault().Value, ConnectedPalyers.Where(q => q.Key == PlayerName).FirstOrDefault().Value);
                    playersInGame.Add(p);
                    bool rand = new Random().Next(0, 3) == 1;
                    p.NotifyPlayer1GameStarted(rand);
                    p.NotifyPlayer2GameStarted(!rand);
                    queuePlayers.Remove(queuePlayers.Where(q => q.Player1 == nameToGame || q.Player1 == PlayerName).FirstOrDefault());
                    ConnectedPalyers.Remove(ConnectedPalyers.Where(q => q.Key == PlayerName).FirstOrDefault().Key);
                    ConnectedPalyers.Remove(ConnectedPalyers.Where(q => q.Key == nameToGame).FirstOrDefault().Key);
                    NotifyPlayers();
                    return;
                }
            }
            queuePlayers.Add(new PairPlayer(PlayerName, nameToGame));
        }
        private void NotifyPlayers(string SkipName = null)
        {
            foreach (var item in ConnectedPalyers)
            {
                if (item.Key == SkipName)
                {
                    continue;
                }
                try
                {
                    item.Value.CallbackGetNewPalyers(ConnectedPalyers.Keys.ToArray());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
    public class PairPlayer
    {
        public PairPlayer(string Player1, string Player2)
        {
            this.Player1 = Player1;
            this.Player2 = Player2;
        }
        public string Player1 { get; set; }
        public string Player2 { get; set; }
    }
    public class Players
    {
        public string Player1 { get; set; }
        public string Player2 { get; set; }
        public ICallback Callback1 { get; set; }
        public ICallback Callback2 { get; set; }
        public bool?[][] PlayerField { get; set; }
        public Players(string Player1, string Player2, ICallback Callback1, ICallback Callback2)
        {
            this.Player1 = Player1;
            this.Player2 = Player2;

            this.Callback1 = Callback1;
            this.Callback2 = Callback2;

            PlayerField = new bool?[3][];
            for (int i = 0; i < 3; i++)
            {
                PlayerField[i] = new bool?[3];
                PlayerField[i][0] = null;
                PlayerField[i][1] = null;
                PlayerField[i][2] = null;
            }
        }
        public void NotifyPlayersPlayerAdd(string[] ConnectedPalyers)
        {
            Callback1.CallbackGetNewPalyers(ConnectedPalyers);
            Callback2.CallbackGetNewPalyers(ConnectedPalyers);
        }
        public void NotifyPlayersNewField()
        {
            Callback1.CallbackGetServerPlayingField(PlayerField);
            Callback2.CallbackGetServerPlayingField(PlayerField);
        }
        public void NotifyPlayer1IsWin(bool isWin)
        {
            Callback1.CallbackGameSuccessfulEnded(isWin);
        }
        public void NotifyPlayer2IsWin(bool isWin)
        {
            Callback2.CallbackGameSuccessfulEnded(isWin);
        }
        public void NotifyPlayer1GameStarted(bool msg)
        {
            Callback1.CallbackGameStarted(msg);
        }
        public void NotifyPlayer2GameStarted(bool msg)
        {
            Callback2.CallbackGameStarted(msg);
        }
    }
}
