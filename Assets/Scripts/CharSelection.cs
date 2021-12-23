using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CharSelection : MonoBehaviourPunCallbacks
{

    public GameObject charSelectScreen;
    public string selection;

    public static CharSelection instance;
    public GameObject readyButton;
    

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        charSelectScreen.SetActive(true);
    }

    public void Ready()
    {
        GameManager.instance.photonView.RPC("PlayerCharSelectionCount", RpcTarget.All);
        readyButton.SetActive(false);
    }

    public void SelectedMage()
    {
        //Invoking the PlayerCharSelectionCount function on all clients and storing the character selection
        selection = "Mage";
    }

    public void SelectedTank()
    {
        selection = "Tank";
    }

}
