using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public string roomName = "Room01";
    public Transform spawnPoint;
    public GameObject playerPref;
    public bool isConnected = false;
    public GameObject camera;

    private void Start()
    {
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
        GameObject pl = PhotonNetwork.Instantiate(playerPref.name, spawnPoint.position, spawnPoint.rotation, 0) as GameObject;
        pl.GetComponent<PlayerController3D>().enabled = true;
        camera.SetActive(false);
        pl.GetComponent<PlayerController3D>().multiplayerGraphics.SetActive(false);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        print("Disconnected from server! " + cause.ToString());
    }
}


