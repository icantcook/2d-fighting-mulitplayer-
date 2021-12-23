using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class Menu : MonoBehaviourPunCallbacks,ILobbyCallbacks
{
    [Tooltip("Drag UI elements into the respective fields to be accessed by the code")]
    [Header("Buttons")]
    public Button hostButton;
    public Button connectButton;

    public int currPlayerCount = 0; 

    [Header("Screens")]
    public GameObject waitScreen;

    public static Menu instance;


    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //making the player wait until he's connected to the master server. Disabling the buttons
        hostButton.interactable=false;
        connectButton.interactable = false;

        //setting the wait screen to inactive
        waitScreen.SetActive(false);
    }


    public override void OnConnectedToMaster()
    {
        // enabling the buttons after the player is connected to the master client.
        hostButton.interactable = true;
        connectButton.interactable = true;
    }

    public void OnPlayerNameChanged(TMP_InputField nameInput)
    {
        //This value updates when player types a name in the UI
        PhotonNetwork.NickName = nameInput.text;
    }

    public void OnMatchFound()
    {
        //Starts the game for all players after a match is found
        NetworkManager.instance.photonView.RPC("ChangeScene", RpcTarget.All, "Game");
    }

    public override void OnJoinedRoom()
    {
        photonView.RPC("UpdatePlayerCount", RpcTarget.All);
    }

    [PunRPC]
    public void UpdatePlayerCount()
    {
        currPlayerCount++;
        if(currPlayerCount==2)
        {
            OnMatchFound();
        }
    }
}
