using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
public class PlayerNetwork : Photon.MonoBehaviour {
    //Vector3 realPos = Vector3.zero;
    //Quaternion realRot = Quaternion.identity;
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private MonoBehaviour[] playerControlScripts;
    private PhotonView PhotonView;
    
    
    GameObject DirectionLight;
    DayNightCycle dnc;

    private Vector3 TargetPosition;
    private Quaternion TargetRotation;

    bool posSet = false;

    private GameObject playerTownSpawn, playerHomeSpawn;

    public bool home;
    public bool town = true;
    GameObject plnet;
    PLNetwork pl;
    GameController gc;
    GameObject gcobj;
    public int num;
    private void Awake()
    {
        PhotonView = GetComponent<PhotonView>();
        
        
    }
    void Start()
    {

        gcobj = GameObject.FindGameObjectWithTag("GameController");
        gc = gcobj.GetComponent<GameController>();
        plnet = GameObject.FindGameObjectWithTag("PLNetwork");
        pl = plnet.GetComponent<PLNetwork>();

        DirectionLight = GameObject.FindGameObjectWithTag("DNC");
        dnc = DirectionLight.GetComponent<DayNightCycle>();
        //PhotonView = GetComponent<PhotonView>();
        num = pl.playerNumber;
        StartCoroutine(SetPositions());


        Initialize();

    }

    
    public void Update()
    {

        if (posSet)
        {
            if (dnc.day)
            {
                if (town)
                {
                    transform.position = playerTownSpawn.transform.position;
                    GetComponent<RigidbodyFirstPersonController>().movementSettings.ForwardSpeed = 0;
                    town = false;
                    home = true;
                }

            }

            if (dnc.night)
            {
                if (home)
                {
                    transform.position = playerHomeSpawn.transform.position;
                    GetComponent<RigidbodyFirstPersonController>().movementSettings.ForwardSpeed = 1.5f;
                    town = true;
                    home = false;
                }
            }
        }
        



    }
    
    private void Initialize()
    {
        if (PhotonView.isMine)
        {

        }
        else
        {
            playerCamera.SetActive(false);

            foreach(MonoBehaviour m in playerControlScripts)
            {
                m.enabled = false;
            }
        }
    }



    public void OnPhotonSerializeView(PhotonStream stream , PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
           
        }
        else
        {
            transform.position = (Vector3)stream.ReceiveNext();
            transform.rotation = (Quaternion)stream.ReceiveNext();
            
        }
    }


    IEnumerator SetPositions()
    {
        yield return new WaitForSeconds(1f);

        if (pl.playerNumber == 1)
        {
            playerTownSpawn = gc.townSpawn1;
            playerHomeSpawn = gc.homeSpawn1;
        }

        if (pl.playerNumber == 2)
        {
            playerTownSpawn = gc.townSpawn2;
            playerHomeSpawn = gc.homeSpawn2;
        }
        if (pl.playerNumber == 3)
        {
            playerTownSpawn = gc.townSpawn3;
            playerHomeSpawn = gc.homeSpawn3;
        }
        posSet = true;

    }
    
}
