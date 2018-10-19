
using UnityEngine;

public class CurrentRoomCanvas : MonoBehaviour {

    public void OnClickStartGame()
    {
        PhotonNetwork.LoadLevel(1);
    }
	
    public void OnClickStartGameDelayed()
    {
        if (!PhotonNetwork.isMasterClient)
        {
            return;
        }
        PhotonNetwork.room.IsOpen = false;
        PhotonNetwork.room.IsVisible = false;
        PhotonNetwork.LoadLevel(1);
    }
}
