using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PLNetwork : MonoBehaviour {
    public static PLNetwork Instance;
    public string PlayerName { get; private set; }
    private int PlayersInGame = 0;
    private PhotonView PhotonView;
    // Use this for initialization
    GameController gc;
    GameObject gcObj;
    GameObject playerNet;
    PlayerNetwork plnet;
    public int playerNumber;
    public Text text;
    
    public GameObject EnterNameField;
    public InputField ipf;
	void Awake () {


        PhotonNetwork.playerName = "Player#" + Random.Range(1000 , 9999);
        SceneManager.sceneLoaded += OnSceneFinishedLoading;
        PhotonView = GetComponent<PhotonView>();
        PhotonNetwork.sendRate = 30;
        PhotonNetwork.sendRateOnSerialize = 20;
        ipf.GetComponent<InputField>();
    }
    
    // Update is called once per frame
    void Update () {
        Instance = this;
        //PlayerName = "Player#" + Random.Range(1000,9999);
        

    }

    private void OnJoinedRoom()
    {

        text.text = "Joined room";
        playerNumber = PhotonNetwork.playerList.Length;
        
    }
    private void OnLeftRoom()
    {
        playerNumber = 0;
    }
    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 1)
        {
            if (PhotonNetwork.isMasterClient)
            {
                MasterLoadedGame();
            }
            else
            {
                NonMasterLoadedGame();
            }
        }
    }

    [PunRPC]
    private void MasterLoadedGame()
    {
        PlayersInGame = 1;
        PhotonView.RPC("RPC_LoadGameScene", PhotonTargets.MasterClient);
        PhotonView.RPC("RPC_LoadGameOthers",PhotonTargets.Others);
    }

    [PunRPC]
    private void NonMasterLoadedGame()
    {
        PhotonView.RPC("RPC_LoadedGameScene",PhotonTargets.MasterClient);
    }

    [PunRPC]
    private void RPC_LoadGameOthers()
    {
        PhotonNetwork.LoadLevel(1);
    }

    [PunRPC]
    private void RPC_LoadedGameScene()
    {
        PlayersInGame++;
        if (PlayersInGame == PhotonNetwork.playerList.Length)
        {
            print("All players are in the game scene");
            PhotonView.RPC("RPC_CreatePlayer",PhotonTargets.All);
        }
    }

    [PunRPC]
    private void RPC_CreatePlayer()
    {

        gcObj = GameObject.FindGameObjectWithTag("GameController");
        gc = gcObj.GetComponent<GameController>();
        if (playerNumber == 1)
        {
            PhotonNetwork.Instantiate(gc.player.name, gc.townSpawn1.transform.position, Quaternion.identity, 0);
            
        }
        if (playerNumber == 2)
        {
            PhotonNetwork.Instantiate(gc.player2.name, gc.townSpawn2.transform.position, Quaternion.identity, 0);
            
        }
        if (playerNumber == 3)
        {
            PhotonNetwork.Instantiate(gc.player3.name, gc.townSpawn3.transform.position, Quaternion.identity, 0);
            
        }
    }


    public void EnterName()
    {
        EnterNameField.SetActive(false);
        PhotonNetwork.playerName = ipf.text;
    }
}
