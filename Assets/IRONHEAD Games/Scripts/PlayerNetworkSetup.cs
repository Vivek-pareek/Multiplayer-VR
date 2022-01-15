using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerNetworkSetup : MonoBehaviourPunCallbacks
{

    public GameObject localXRRigGameObject;

    public GameObject avatarHeadGameObject;

    public GameObject avatarBodyGameObject;

    #region unity methods
    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            //Local player
            localXRRigGameObject.SetActive(true);

            SetLayerRecursively(avatarHeadGameObject, 6);

            SetLayerRecursively(avatarBodyGameObject, 7);

            TeleportationArea[] teleportationAreas = GameObject.FindObjectsOfType<TeleportationArea>();

            Debug.Log("Assigning teleportation provider");

            if (teleportationAreas.Length > 0)
            {
                Debug.Log("There are object of teleportation area type!");
                foreach (var teleportationArea in teleportationAreas)
                {
                    teleportationArea.teleportationProvider = localXRRigGameObject.GetComponent<TeleportationProvider>();
                }
            }
        }
        else {
            //Remote Player
            localXRRigGameObject.SetActive(false);

            SetLayerRecursively(avatarHeadGameObject, 0);

            SetLayerRecursively(avatarBodyGameObject, 0);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion

    #region class methods
    
    void SetLayerRecursively(GameObject go, int layerNumber)
    {
        if (go == null) return;
        foreach (Transform trans in go.GetComponentsInChildren<Transform>(true))
        {
            trans.gameObject.layer = layerNumber;
        }
    }

    #endregion
}
