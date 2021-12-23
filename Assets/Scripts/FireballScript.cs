using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballScript : MonoBehaviour
{
    private int damage;
    private bool isMine;

    public Rigidbody rig;

    public void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }

    public void Start()
    {
        rig.velocity = Vector3.right * 5;
    }
    public void Initialize(int damage, bool isMine)
    {
        this.damage = damage;
        this.isMine = isMine;
        
        Destroy(gameObject, 5.0f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            VirtualJoystickTest.instance.photonView.RPC("TakeDamage",Photon.Pun.RpcTarget.Others,damage);
            Debug.Log("damage received");
        }
        Destroy(gameObject);
    }
}
