using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class VirtualJoystickTest : MonoBehaviourPunCallbacks
{
    [SerializeField] private VirtualJoystick inputSource;
    private Rigidbody rigid;
    private PhotonView pV;
    private Animator anim;
    private bool mastClient;
    private GameObject Primary;

    public bool dead;

    [Header("Stats")]
    public float moveSpeed;
    public float jumpForce;
    public int curHp;
    public int maxHp;

    public static VirtualJoystickTest instance;

    void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        inputSource = FindObjectOfType<VirtualJoystick>();
        pV = GetComponent<PhotonView>();
        anim = GetComponent<Animator>();
        mastClient = PhotonNetwork.IsMasterClient;
        GameUI.instance.Initialize(this);
    }

    private void Update()
    {
        if (!pV.IsMine)
            return;
      
        rigid.velocity = new Vector3(inputSource.Direction.x, 0, 0);

        if (inputSource.Direction.z > 0.5)
            TryJump();

        AnimateMovement();

    }

    void AnimateMovement()
    {
        if (mastClient)
        {
            if (inputSource.Direction.x > 0)
            {
                anim.SetBool("RunningForward", true);
                anim.SetBool("RunningBackward", false);
            }
            if (inputSource.Direction.x < 0)
            {
                anim.SetBool("RunningBackward", true);
                anim.SetBool("RunningForward", false);
            }
            if (inputSource.Direction.x == 0)
            {
                anim.SetBool("RunningBackward", false);
                anim.SetBool("RunningForward", false);
            }
        }

        if (!mastClient)
        {
            if (inputSource.Direction.x < 0)
            {
                anim.SetBool("RunningForward", true);
                anim.SetBool("RunningBackward", false);
            }
            if (inputSource.Direction.x > 0)
            {
                anim.SetBool("RunningBackward", true);
                anim.SetBool("RunningForward", false);
            }
            if (inputSource.Direction.x == 0)
            {
                anim.SetBool("RunningBackward", false);
                anim.SetBool("RunningForward", false);
            }
        }
    }
    void TryJump()
    {
        Ray ray = new Ray(transform.position, Vector3.down);

        //shoot the ray cast
        if (Physics.Raycast(ray, 1.5f))
        {
            rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            anim.SetTrigger("Jump");
        }

    }

    [PunRPC]
    public void TakeDamage(int damage)
    {
        if (dead)
            return;
        curHp = curHp - damage;

        Debug.Log("Damage received");
     
        //update health UI
        GameUI.instance.UpdateHealthBar();

        //die if no health left
        if (curHp <= 0)
            photonView.RPC("Die", RpcTarget.All);
    }

    [PunRPC]
    void Die()
    {
        dead = true;
        
        if (photonView.IsMine)
        {
            anim.SetBool("Die", true);
        }

        if(!pV.IsMine)
        {
            anim.SetBool("Victory", true);
        }
    }

}
