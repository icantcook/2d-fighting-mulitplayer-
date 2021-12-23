using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Slider healthBar;
    public Slider mana;
    public TextMeshProUGUI playerInfoText;
    public Image winBackground;

    private VirtualJoystickTest player;

    public static GameUI instance;

    void Awake()
    {
        instance = this;
        player = GetComponent<VirtualJoystickTest>();
    }

    public void Initialize(VirtualJoystickTest localPlayer)
    {
        player = localPlayer;
        healthBar.maxValue = player.maxHp;
        healthBar.value = player.curHp;

      
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        healthBar.value = player.curHp;
    }



    public void UpdateAmmoText()
    {
       // ammoText.text = player.weapon.currAmmo + "/" + player.weapon.maxAmmo;
    }

}
