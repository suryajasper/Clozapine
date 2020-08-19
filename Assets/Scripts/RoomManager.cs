using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public string roomName = "Room01";
    public List<Transform> spawnPoint;
    public GameObject playerPref;
    public bool isConnected = false;
    public GameObject camera;

    private System.Random random;

    private void Start()
    {
        random = new System.Random();
        print("Connecting to server...");

        PhotonNetwork.GameVersion = "0.1";
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinOrCreateRoom(roomName, null, null);
        print("Connected to server!");
    }

    public override void OnJoinedRoom()
    {
        isConnected = true;
        SpawnPlayer();

        print("Joined room!");
    }

    public void SpawnPlayer()
    {
        Transform randomSpawnPoint = spawnPoint[random.Next(0, spawnPoint.Count - 1)];
        GameObject pl = PhotonNetwork.Instantiate(playerPref.name, randomSpawnPoint.position, randomSpawnPoint.rotation, 0) as GameObject;
        pl.GetComponent<PlayerController3D>().enabled = true;
        camera.SetActive(false);
        //pl.GetComponent<PlayerController3D>().multiplayerGraphics.SetActive(false);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        print("Disconnected from server! " + cause.ToString());
    }
}