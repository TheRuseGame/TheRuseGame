using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
public class PlayerControllerAnim : Photon.MonoBehaviour {
    public bool walking;
    public bool c;
    Animator anim;
    RigidbodyFirstPersonController rbfpc;
    public GameObject cam;
    PlayerRole playerRole;
    GameObject DirectionLight;
    DayNightCycle dnc;
    PlayerNetwork pln;
    public bool detach;
    
    private PhotonView PhotonView;


    private void Awake()
    {
        PhotonView = GetComponent<PhotonView>();


    }
    void Start () {
        anim = GetComponent<Animator>();
        rbfpc = GetComponent<RigidbodyFirstPersonController>();
        playerRole = GetComponent<PlayerRole>();
        DirectionLight = GameObject.FindGameObjectWithTag("DNC");
        dnc = DirectionLight.GetComponent<DayNightCycle>();
        pln = GetComponent<PlayerNetwork>();
        
        GetComponent<RigidbodyFirstPersonController>().enabled = true;
        GetComponent<PlayerNetwork>().enabled = true;

    }
	
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.SetBool("walk", true);
            if (anim.GetBool("crouch"))
            {
                
                anim.SetBool("crouchWalk", true);
            }
            else
            {
                anim.SetBool("walk", true);

            }
            
            //anim.SetBool("idle", false);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            anim.SetBool("idle",true);
            anim.SetBool("run", false);
            anim.SetBool("walk", false);
            anim.SetBool("crouchWalk",false);
            
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            anim.SetBool("run",true);
            
            
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            anim.SetBool("run", false);
        }
        /*if (Input.GetKeyDown(KeyCode.S))
        {
            anim.SetBool("walkBack",true);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            
        }
        */
        
        
        
                
           

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            c = true;
            anim.SetBool("crouch", true);
            rbfpc.movementSettings.ForwardSpeed = 0.80f;
            //cam.transform.position = new Vector3(transform.position.x, transform.position.y - 0.001f, transform.position.z);
        }
        
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            c = false;
            //cam.transform.position = new Vector3(transform.position.x, transform.position.y + 0.4f, transform.position.z);
            rbfpc.movementSettings.ForwardSpeed = 1.5f;
            anim.SetBool("crouch", false);
            anim.SetBool("crouchWalk", false);
        }
    }

    [PunRPC]
    public void DieFunc()
    {
        anim.SetBool("die", true);
        
        StartCoroutine(UntilDay());

        //GetComponent<RigidbodyFirstPersonController>().movementSettings.ForwardSpeed = 0;
        
    }


    IEnumerator UntilDay()
    {
        yield return new WaitUntil(() => dnc.day == true);
        //GetComponent<PlayerNetwork>().enabled = false;
        
    }

    IEnumerator deleteText()
    {
        yield return new WaitForSeconds(5f);
        playerRole.Info.text = "";
    }

    
   
}
