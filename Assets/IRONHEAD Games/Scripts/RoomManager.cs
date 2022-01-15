using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
public class RoomManager : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI playerCountText;

    #region unity methods
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;

        if (PhotonNetwork.IsConnectedAndReady) {
            PhotonNetwork.JoinLobby();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #endregion 

    #region UI mapped methods

    public void JoinRandomRoom() {
        PhotonNetwork.JoinRandomRoom();
    }

    #endregion

    #region Photon Callback methods
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join room. Error code : " + returnCode + "Message : " + message);
        CreateAndJoinRoom();
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Room created with name : " + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Player joined room : " + PhotonNetwork.CurrentRoom.Name + "Player Count = :" + PhotonNetwork.CurrentRoom.PlayerCount);

        if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey(MultiplayerConstants.MAP_TYPE_KEY)) {
            object mapType;
            if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(MultiplayerConstants.MAP_TYPE_KEY, out mapType)) {
                Debug.Log("Joined room with the map: " + (string)mapType);
                //Here, we can add new checks as per new maps addition
                if ((string)mapType == MultiplayerConstants.MAP_TYPE_SCHOOL) {
                    PhotonNetwork.LoadLevel("World_School");
                }
            }
        }

    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("Player count: " + PhotonNetwork.CurrentRoom.PlayerCount);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("Room list updated");
        if (roomList.Count == 0)
        {
            playerCountText.text = 0 + "/" + 20;
        }

        foreach (RoomInfo room in roomList) {
            playerCountText.text = room.PlayerCount + "/" + 20;
        }

    }

    public override void OnJoinedLobby()
    {
        Debug.Log("joined the lobby");
    }

    #endregion

    #region private methods
    private void CreateAndJoinRoom()
    {
        string randomRoomName = "Room_" + Random.Range(1, 100);
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 20;

        ExitGames.Client.Photon.Hashtable customPropertiesForRoom = new ExitGames.Client.Photon.Hashtable() { {MultiplayerConstants.MAP_TYPE_KEY, MultiplayerConstants.MAP_TYPE_SCHOOL } };

        string[] roomProperties = {MultiplayerConstants.MAP_TYPE_KEY};
        roomOptions.CustomRoomPropertiesForLobby = roomProperties;
        roomOptions.CustomRoomProperties = customPropertiesForRoom;

        Debug.Log("Creating a new room with name : " + randomRoomName);
        PhotonNetwork.CreateRoom(randomRoomName, roomOptions);
    }
    #endregion
}
