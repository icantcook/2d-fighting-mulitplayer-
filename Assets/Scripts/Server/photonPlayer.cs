using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class photonPlayer : MonoBehaviourPun
{
    public PhotonView PV;
    public GameObject myAvatar;

    void Start()
    {
        PV = GetComponent<PhotonView>();

        if(PV.IsMine)
        {


            if(PhotonNetwork.IsMasterClient)
            {
                myAvatar = PhotonNetwork.Instantiate(CharSelection.instance.selection, GameManager.instance.spawnPoints[0].position, GameManager.instance.spawnPoints[0].rotation);
                GameObject[] gameObjects;
                gameObjects = GameObject.FindGameObjectsWithTag("Tank");
                PhotonNetwork.Destroy(gameObjects[1]);

                GameObject[] mageObjects;
                mageObjects = GameObject.FindGameObjectsWithTag("Mage");
                PhotonNetwork.Destroy(mageObjects[1]);
            }
                
            else if(!PhotonNetwork.IsMasterClient)
            {
                myAvatar = PhotonNetwork.Instantiate(CharSelection.instance.selection, GameManager.instance.spawnPoints[1].position, GameManager.instance.spawnPoints[1].rotation);
                //myAvatar.GetComponent<PlayerController>().photonView.RPC("Initialize", RpcTarget.All, PhotonNetwork.LocalPlayer);

                GameObject[] gameObjects;
                gameObjects = GameObject.FindGameObjectsWithTag("Tank");
                PhotonNetwork.Destroy(gameObjects[1]);

                GameObject[] mageObjects;
                mageObjects = GameObject.FindGameObjectsWithTag("Mage");
                PhotonNetwork.Destroy(mageObjects[1]);
            }

            
        }
       
    }
}
