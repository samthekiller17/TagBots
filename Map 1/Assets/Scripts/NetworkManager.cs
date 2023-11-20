using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    private GameObject spawnerPlayerPrefrab;
    public string sceneNameToChange;
    public GameObject WaitingforPlayers;
    public GameObject playerlist;

    void Start()
    {
        ConnectedToServer();
    }

    void ConnectedToServer()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Trying connect to server...");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to server.");
        base.OnConnectedToMaster();
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        roomOptions.IsVisible = true;
        roomOptions.IsOpen = true;
        PhotonNetwork.JoinOrCreateRoom("Sala1", roomOptions, TypedLobby.Default);

    }
    public override void OnJoinedRoom()
    {
        Debug.Log("Joined on room.");
        base.OnJoinedRoom();
        switch (SceneManager.GetActiveScene().name)
        {
            case "Game":
                GameObject[] restpawns = GameObject.FindGameObjectsWithTag("Respawn");
                int rant=UnityEngine.Random.Range(0, restpawns.Length);
               
                Vector3 posToSet = restpawns[rant].transform.position;
                spawnerPlayerPrefrab = PhotonNetwork.Instantiate("Player", posToSet, transform.rotation);
                break;
        }
    }
    
        
    
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        sceneNameToChange = "Lobby";
        ChangeRoom();
    }

    public void ChangeRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
    public override void OnLeftRoom()
    {
        Debug.Log("Left room");
        base.OnLeftRoom();
        PhotonNetwork.Destroy(spawnerPlayerPrefrab);
        PhotonNetwork.Disconnect();
        ChangeScene();
    }
    void ChangeScene()
    {
        PhotonNetwork.LoadLevel(sceneNameToChange);
    }
}
