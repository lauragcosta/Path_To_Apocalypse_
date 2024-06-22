using UnityEngine;

[CreateAssetMenu(fileName = "NewCombatData", menuName = "Game/CombatData")]
public class CombatData : ScriptableObject
{
    [SerializeField] private GameObject npc; // Optional parameter for a npc if it's from one
    [SerializeField] private bool isWonCombat;
    [SerializeField] private Difficulty? difficulty;
    [SerializeField] private RewardType? rewardType;
    [SerializeField] private Need? rewardNeed; // Optional parameter for a need
    [SerializeField] private GameObject rewardWeapon;  // Optional parameter for a prefab
    [SerializeField] private GameObject weaponInHand;

    public void ResetValues()
    {
        npc = null;
        isWonCombat = false;
        difficulty = null; 
        rewardType = null; 
        rewardNeed = null;
        rewardWeapon = null; 
    }

    public GameObject WeaponInHand
    {
        get { return weaponInHand; }
        set { weaponInHand = value; }
    }

    public GameObject Npc { 
        get { return npc; }
        set { npc = value; }
    }
    public bool IsWonCombat
    {
        get { return isWonCombat; }
        set { isWonCombat = value; }
    }

    public Difficulty? Difficulty
    {
        get { return difficulty; }
        set { difficulty = value; }
    }

    public RewardType? RewardType
    {
        get { return rewardType; }
        set { rewardType = value; }
    }

    public Need? RewardNeed
    {
        get { return rewardNeed; }
        set { rewardNeed = value; }
    }

    public GameObject RewardWeapon
    {
        get { return rewardWeapon; }
        set { rewardWeapon = value; }
    }
}


public enum RewardType
{
    Weapon,
    Need
}

public enum Need
{
    Hunger,
    Thirst,
    Health,
}

public enum Difficulty
{
    Easy,
    Medium,
    Hard,
}
