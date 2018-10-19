using System.Collections;

using UnityEngine;
using UnityEngine.UI;
using ExitGames.Client.Photon.Chat;
using ExitGames.Client.Photon;

public class ChatManager : Photon.MonoBehaviour , IChatClientListener{

    public ChatClient chatClient;
    public Text playerName;
    public Text connectionState;
    string worldChat;
    public Text msgInput;
    public Text msgArea;

    AuthenticationValues av;

    void Start()
    {
        Application.runInBackground = true;
        chatClient.Connect("88e2a12a-0fad-4e01-a259-270df830ab7d","0.1",null);
    }



    private void Update()
    {

        this.chatClient.Service();
        

    }


    

    public void DebugReturn(DebugLevel level, string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnDisconnected()
    {
        throw new System.NotImplementedException();
    }

    public void OnConnected()
    {
        throw new System.NotImplementedException();
    }

    public void OnChatStateChange(ChatState state)
    {
        throw new System.NotImplementedException();
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        throw new System.NotImplementedException();
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        throw new System.NotImplementedException();
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnsubscribed(string[] channels)
    {
        throw new System.NotImplementedException();
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        throw new System.NotImplementedException();
    }

    void IChatClientListener.DebugReturn(DebugLevel level, string message)
    {
        throw new System.NotImplementedException();
    }

    void IChatClientListener.OnDisconnected()
    {
        throw new System.NotImplementedException();
    }

    void IChatClientListener.OnConnected()
    {
        throw new System.NotImplementedException();
    }

    void IChatClientListener.OnChatStateChange(ChatState state)
    {
        throw new System.NotImplementedException();
    }

    void IChatClientListener.OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        throw new System.NotImplementedException();
    }

    void IChatClientListener.OnPrivateMessage(string sender, object message, string channelName)
    {
        throw new System.NotImplementedException();
    }

    void IChatClientListener.OnSubscribed(string[] channels, bool[] results)
    {
        throw new System.NotImplementedException();
    }

    void IChatClientListener.OnUnsubscribed(string[] channels)
    {
        throw new System.NotImplementedException();
    }

    void IChatClientListener.OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        throw new System.NotImplementedException();
    }
}
