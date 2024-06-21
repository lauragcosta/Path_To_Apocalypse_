using UnityEngine;

[CreateAssetMenu(fileName = "NewCombatData", menuName = "Combat Data", order = 51)]
public class CombatData : ScriptableObject
{
    public int difficulty;
    public RewardType rewardType;
    public Need rewardNeed;
    public GameObject rewardPrefab;  // Optional parameter for a prefab
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
