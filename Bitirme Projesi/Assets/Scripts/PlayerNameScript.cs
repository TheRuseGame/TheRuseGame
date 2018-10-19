using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNameScript : Photon.MonoBehaviour {
     
	// Use this for initialization
	void Start () {
        
            GetComponent<TextMesh>().text = photonView.owner.NickName;
            
        

        
    }


    

    

}
