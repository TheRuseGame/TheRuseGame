using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : Photon.MonoBehaviour {
    // 175 / -175
    
    public bool day = true;
    public bool night;
    
    public Material sun;
    
    GameObject GameCont;
    GameController gc;
    void Start() {
        
        
        
        GameCont = GameObject.FindGameObjectWithTag("GameController");
        gc = GameCont.GetComponent<GameController>();
        
    }


    void Update() {
        

        if (transform.eulerAngles.x > 90f)
        {
            night = true;
            day = false;
            StartCoroutine(ResDay());
            
        }
        else
        {
            day = true;
            night = false;
            StartCoroutine(ResNight());
        }

        
        
    }

    IEnumerator ResDay()
    {
       
        yield return new WaitForSeconds(30f);
        transform.rotation = Quaternion.Euler(new Vector3(90f, -30f, 0f));
    }

    IEnumerator ResNight()
    {
        
        yield return new WaitForSeconds(30f);
        transform.rotation = Quaternion.Euler(new Vector3(210f, -30f, 0f));
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        //else
        //{
            //transform.position = (Vector3)stream.ReceiveNext();
            //transform.rotation = (Quaternion)stream.ReceiveNext();
        //}
    }

}
