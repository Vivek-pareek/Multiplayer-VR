using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class VirtualWorldManager : MonoBehaviourPunCallbacks
{
    #region Photon callback methods

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("New player joined. Player count : " + PhotonNetwork.CurrentRoom.PlayerCount);
    }

    #endregion
}
