using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkedGrabbing : MonoBehaviourPunCallbacks, IPunOwnershipCallbacks
{
    PhotonView photonViewComponent;

    Rigidbody rb;

    bool isBeingHeld = false;

    private void Awake()
    {
        photonViewComponent = GetComponent<PhotonView>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isBeingHeld)
        {
            rb.isKinematic = true;
            gameObject.layer = 10;
        }
        else {
            rb.isKinematic = false;
            gameObject.layer = 9;
        }
    }

    public void OnSelectEntered() {
        Debug.Log("Object grabbed");
        photonViewComponent.RPC("StartNetworkGrabbing", RpcTarget.AllBuffered);

        if (photonViewComponent.Owner == PhotonNetwork.LocalPlayer)
        {
            Debug.Log("Object is owned");
        }
        else
        {
            TransferOwnership();
        }
    }

    public void OnSelectExited() {
        Debug.Log("Object released");
        photonViewComponent.RPC("StopNetworkGrabbing", RpcTarget.AllBuffered); 
    }

    private void TransferOwnership() {
        photonViewComponent.RequestOwnership();
    }

    public void OnOwnershipRequest(PhotonView targetView, Player requestingPlayer)
    {
        if (targetView != photonViewComponent) {
            return;
        }
        photonViewComponent.TransferOwnership(requestingPlayer);
    }

    public void OnOwnershipTransfered(PhotonView targetView, Player previousOwner)
    {
        Debug.Log("Ownership transferred");
    }

    public void OnOwnershipTransferFailed(PhotonView targetView, Player senderOfFailedRequest)
    {
        Debug.Log("Ownership transfer failed");
    }

    [PunRPC]
    public void StartNetworkGrabbing() {
        isBeingHeld = true;
    }

    [PunRPC]
    public void StopNetworkGrabbing() {
        isBeingHeld = false;
    }

}
