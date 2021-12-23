using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "GameData/CharacterClass", order = 1)]
public class CharacterClass : ScriptableObject
{
    public CharacterTypeEnum CharacterType;

    [Tooltip("skill1 is usually the character's default attack")]
    public ActionType Skill1;

    [Tooltip("skill2 is usually the character's secondary attack")]
    public ActionType Skill2;

    [Tooltip("skill3 is usually the character's unique or special attack")]
    public ActionType Skill3;

    [Tooltip("Starting HP of this character class")]
    public int BaseHP;

    [Tooltip("Starting Mana of this character class")]
    public int BaseMana;

    [Tooltip("Base movement speed of this character class (in meters/sec)")]
    public float Speed;

}
