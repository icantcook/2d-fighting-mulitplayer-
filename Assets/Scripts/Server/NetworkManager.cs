using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager instance;

    private string currRoom;

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        //connects to Photon using the server settings. A requisite step for photon.
        PhotonNetwork.ConnectUsingSettings();
    }

    //I am implementing the game for just one room. Undoubtedly, we can have multiple rooms at the same time.
    //Matchmaking can be implemented based on stats too.
    public void Host()
    {
        //creates room based on room options. Here, since we wish to have a duel, max players are set as 2.
        PhotonNetwork.CreateRoom("currRoom", new RoomOptions {MaxPlayers =2});
        Menu.instance.waitScreen.SetActive(true);
    }

    public void Connect()
    {
        PhotonNetwork.JoinRoom("currRoom");
    }

    public void OnBack()
    {
        PhotonNetwork.LeaveRoom();
        Menu.instance.waitScreen.SetActive(false);
        Menu.instance.currPlayerCount = 0;
    }

    [PunRPC]
    public void ChangeScene(string sceneName)
    {
        PhotonNetwork.LoadLevel(sceneName);
    }
}
