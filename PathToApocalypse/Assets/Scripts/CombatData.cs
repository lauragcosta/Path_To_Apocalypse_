using UnityEngine;

[CreateAssetMenu(fileName = "NewCombatData", menuName = "Combat Data")]
public class CombatData : ScriptableObject
{
    [SerializeField] private Difficulty difficulty;
    [SerializeField] private RewardType rewardType;
    [SerializeField] private Need rewardNeed; // Optional parameter for a need
    [SerializeField] private GameObject rewardPrefab;  // Optional parameter for a prefab
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
