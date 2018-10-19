using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
public class PlayerRole : Photon.MonoBehaviour {
    DayNightCycle dnc;
    GameObject DirectionLight;
    public Text Info;
    public bool dead = false;
    public bool detachMann = false;
    public float range = 2f;
    public Camera cam;
    bool Tag_player;
    public string Class ;
    public string role ;
    PlayerControllerAnim pcanim;
    public PlayerRole pr2;
    public GameObject mannequin;
    public GameObject PlayerName;

    void Start () {
        pcanim = GetComponent<PlayerControllerAnim>();
        DirectionLight = GameObject.FindGameObjectWithTag("DNC");
        dnc = DirectionLight.GetComponent<DayNightCycle>();
    }
	
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }


        
	}

    [PunRPC]
    private void Interact()
    {
        if (dnc.night)
        {
            RaycastHit hit;
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
            {
                if (hit.collider.tag == "Player")
                {
                    if (role == "Sherrif")
                    {
                        PlayerRole pr = hit.transform.GetComponent<PlayerRole>();
                        if (pr)
                        {
                            Info.text = "Your Target is a member of " + pr.Class;
                        }
                        StartCoroutine(deleteText());
                    }
                    if (role == "Mafioso")
                    {
                        PlayerRole pr = hit.transform.GetComponent<PlayerRole>();

                        if (pr)
                        {
                            if(pr.role != "Serial Killer")
                            {
                                pr2 = pr;
                                StartCoroutine(Die());
                                pr.Info.text = "You were killed";
                                Info.text = "Shot Him";
                                StartCoroutine(deleteText());
                            }
                        }  
                    }
                    if (role == "Serial Killer")
                    {
                        PlayerRole pr = hit.transform.GetComponent<PlayerRole>();

                        if (pr)
                        {
                            pr2 = pr;
                            StartCoroutine(Die());
                        }


                        Info.text = "Stabbed Him";
                        StartCoroutine(deleteText());
                    }
                    
                }


            }
        }
        
    }

    IEnumerator deleteText()
    {
        yield return new WaitForSeconds(5f);
        Info.text = "";
    }

    IEnumerator deleteText2()
    {
        yield return new WaitForSeconds(5f);
        Info.text = "";
    }

    IEnumerator Die()
    {
        yield return new WaitUntil(() => dnc.day == true);
        yield return new WaitForSeconds(2f);
        pr2.GetComponent<PhotonView>().RPC("DeathInfo",PhotonTargets.All);
        pr2.GetComponent<PhotonView>().RPC("DieFunc", PhotonTargets.All);
        pr2.GetComponent<PhotonView>().RPC("Ghost",PhotonTargets.All);
        pr2.GetComponent<PhotonView>().RPC("DisableCollider",PhotonTargets.All);
        pr2.Info.text = "You were killed";
    }

    [PunRPC]
    void DeathInfo()
    {
        //Info.text = "You were killed!!";
        //StartCoroutine(deleteText2());
    }

    [PunRPC]
    void Ghost()
    {
        PlayerName.SetActive(false);
        mannequin.transform.parent = null;
        
    }

    [PunRPC]
    void DisableCollider()
    {
        GetComponent<Collider>().tag = "Untagged";
    }


}
