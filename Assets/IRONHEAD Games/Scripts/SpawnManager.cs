using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    public GameObject genericVRPlayerPrefab;

    public Vector3 spawnPosition;

    #region unity methods
    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsConnectedAndReady) {
            PhotonNetwork.Instantiate(genericVRPlayerPrefab.name, spawnPosition, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion
}
