using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviourPunCallbacks
{    
    //using a string instead of gameObject to instantiate players. Because Photon uses path to the stored prefab instead.
    public string prefabString;
    public Transform[] spawnPoints;
    public VirtualJoystickTest[] players;
    public GameObject magePortrait;
    public GameObject tankPortrait;

    public byte charSel =0;

    public static GameManager instance;

     void Awake()
    {
        instance = this;
    }

    void Start()
    {
        players = new VirtualJoystickTest[PhotonNetwork.PlayerList.Length];

    }

    [PunRPC]
    void PlayerCharSelectionCount()
    {
        charSel++;
        if(charSel==2)
        {
            PhotonNetwork.Instantiate("PhotonNetworkPlayer", spawnPoints[0].position, spawnPoints[0].rotation);
            PhotonNetwork.Instantiate("PhotonNetworkPlayer", spawnPoints[1].position, spawnPoints[1].rotation);
            CharSelection.instance.charSelectScreen.SetActive(false);

            if (CharSelection.instance.selection == "Mage")
            {
                magePortrait.SetActive(true);
            }
            else
                tankPortrait.SetActive(true);
        }
        
    }

}
