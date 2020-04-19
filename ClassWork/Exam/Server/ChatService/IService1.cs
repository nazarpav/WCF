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
    public interface IGame
    {
        [OperationContract]
        string[] Login(string name);

        [OperationContract]
        void StartGame(string PlayerName, string nameToGame);

        [OperationContract(IsOneWay = true)]
        void PlayerMove(string userName, int x,int y);

        [OperationContract]
        void Logout(string name);

        [OperationContract]
        void EndGame(string name);
    }

    public interface ICallback
    {
        [OperationContract(IsOneWay = true)]
        void CallbackGetNewPalyers(string[] msg);
        [OperationContract(IsOneWay = true)]
        void CallbackGameSuccessfulEnded(bool isWin);
        [OperationContract(IsOneWay = true)]
        void CallbackGetServerPlayingField(bool?[][] msg);
        [OperationContract(IsOneWay = true)]
        void CallbackGameStarted(bool msg);
    }
}
