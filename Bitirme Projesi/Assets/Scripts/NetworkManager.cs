using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NetworkManager : MonoBehaviour {
    
    [SerializeField] private Text connectText;
    
    public int playerNum;
    
    
    
    void Start () {
        print("Connecting to server..");
        PhotonNetwork.ConnectUsingSettings("0.1");
       
    }

    public virtual void OnConnectedToMaster()
    {
        print("Connected to master");
        PhotonNetwork.automaticallySyncScene = false;
        PhotonNetwork.playerName = PLNetwork.Instance.PlayerName;
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }



    private void OnJoinedLobby()
    {

        print("Joined lobby");
        if (!PhotonNetwork.inRoom)
        {
            MainCanvasManager.Instance.LobbyCanvas.transform.SetAsLastSibling();
        }
        
        
        //PhotonNetwork.JoinOrCreateRoom("Room",null,null);
    }
    /*public virtual void OnJoinedRoom()
    {
        joined = true;
        if (PhotonNetwork.countOfPlayers == 1)
        {
            PhotonNetwork.Instantiate(player.name, spawnPoint1.position, Quaternion.identity, 0);
        }
        if (PhotonNetwork.countOfPlayers == 2)
        {
            Debug.Log(PhotonNetwork.countOfPlayers);
            PhotonNetwork.Instantiate(player.name, spawnPoint2.position, Quaternion.identity, 0);
            
        }
        lobbyCamera.SetActive(false);
    print("Joined Room");
    }*/
    void Update () {
        connectText.text = PhotonNetwork.connectionStateDetailed.ToString();
        
        
    }


    
}
