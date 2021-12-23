using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class mageAttacks: MonoBehaviourPunCallbacks
{
    [Header("Stats")]
    public int primaryDamage;
    public float magePrimarySpeed;
    public float primaryRate;

    public float secondaryRate;

    public float mana;
    public float primaryManaCost;
    public float secondaryManaCost;
    public float ultiManaCost;

    private float lastPrimaryTime;
    private Animator anim;

    public bool primaryCalled;

    public GameObject fireballPrefab;
    public Transform primarySpawnPos;

    private VirtualJoystickTest player;

    public static mageAttacks instance;

    void Awake()
    {
        //get component
        player = GetComponent<VirtualJoystickTest>();
        anim = GetComponent<Animator>();
        instance = this;
    }


    public void TryPrimary()
    {
        //can we primary attack
        if (mana <= 0 || Time.time - lastPrimaryTime < primaryRate)
            return;

        lastPrimaryTime = Time.time;
        mana = mana - primaryManaCost;

        //update mana 
        //   GameUI.instance.UpdateAmmoText();

        //spawn primary
        player.photonView.RPC("Primary", RpcTarget.All, primarySpawnPos.position, fireballPrefab.transform.forward);
    }

    [PunRPC]
    void Primary(Vector3 pos, Vector3 dir)
    {
        primaryCalled = true;

        //spawn and orient it
        anim.SetTrigger("Primary");
        Primary();

        GameObject primary = PhotonNetwork.Instantiate("Fireball", pos, Quaternion.identity);
        primary.transform.forward = dir;

        
        Debug.Log("spawned Fireball");

        //get the fireball script
         FireballScript FireballScript = fireballPrefab.GetComponent<FireballScript>();

        //initialize it 
         FireballScript.Initialize(primaryDamage, player.photonView.IsMine);
    }

    IEnumerator Primary()
    {
        yield return new WaitForSeconds(1.2f);
        primaryCalled = false;      
    }
}