using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class PlayerController : MonoBehaviourPunCallbacks
{
   

    [Header("Components")]
    public Rigidbody rig;
    public Player photonPlayer;

    public PhotonView pView;

    void Start()
    {
        pView = GetComponent<PhotonView>();
    }
    void Update()
    {
        if (!pView.IsMine)
            return;

    }

    
}
