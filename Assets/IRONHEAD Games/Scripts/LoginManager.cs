using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LoginManager : MonoBehaviourPunCallbacks
{
    #region unity methods
    void Start()
    {
        Debug.Log("Attempting to connect to server");
        PhotonNetwork.ConnectUsingSettings();
    }
    #endregion

    #region Callback methods
    public override void OnConnected()
    {
        Debug.Log("On Connected called, server available!");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Successfully connected to server");
        PhotonNetwork.LoadLevel("HomeScene");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log(cause.ToString());
    }

    #endregion

}
