using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonLobby : MonoBehaviourPunCallbacks
{
    public static PhotonLobby Instance { get; private set; }

    public GameObject battleButton;
    public GameObject cancelButton;

    void Awake()
    {
        SingletonInitialize();
    }

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    
    public void OnBattleButtonClick()
    {
        Debug.Log("Battle button was clicked");
        battleButton.SetActive(false);
        cancelButton.SetActive(true);
        PhotonNetwork.JoinRandomRoom();
    }

    public void OnCancelButtonClick()
    {
        Debug.Log("Cancel button was clicked");
        battleButton.SetActive(true);
        cancelButton.SetActive(false);
        PhotonNetwork.LeaveRoom();
    }

    #region Photon Methods
    public override void OnConnectedToMaster()
    {
        Debug.Log("Player has connected to the Photon master");
        PhotonNetwork.AutomaticallySyncScene = true;
        battleButton.SetActive(true);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Tried to join a random room but failed. There must be no open games available");
        CreateRoom();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Tried to create a new room but failed, there must be a room with the same name...");
        CreateRoom();
    }

    private void CreateRoom()
    {
        Debug.Log("creating a room...");
        int randomRoomName = Random.Range(0, 10000);
        RoomOptions roomOps = new RoomOptions()
        {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = 10
        };

        PhotonNetwork.CreateRoom("Room" + randomRoomName, roomOps);
    }
    #endregion

    private void SingletonInitialize()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this.gameObject);
        }
    }
}
