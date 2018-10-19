using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetachMannequin : MonoBehaviour {

    PlayerRole pr;
    GameObject player;
	void Start () {
        
	}
	
	
	void Update () {
        
            
          
	}

    [PunRPC]
    void Ghost()
    {
        transform.parent = null;
    }
}
