using UnityEngine;

/// <summary>
/// ScriptableObject que armazena dados relacionados ao combate.
/// </summary>
[CreateAssetMenu(fileName = "NewCombatData", menuName = "Game/CombatData")]
public class CombatData : ScriptableObject
{
    [SerializeField] private GameObject npc; // O NPC envolvido no combate (opcional)
    [SerializeField] private bool isWonCombat; // Indica se o combate foi vencido
    [SerializeField] private Difficulty? difficulty; // Dificuldade do combate (opcional)
    [SerializeField] private RewardType? rewardType; // Tipo de recompensa (arma ou necessidade)
    [SerializeField] private Need? rewardNeed; // Necessidade como recompensa (opcional)
    [SerializeField] private string rewardWeapon; // Nome da arma como recompensa (opcional)
    [SerializeField] private GameObject weaponInHand; // Arma atualmente equipada

    /// <summary>
    /// Reseta todos os valores para seus estados iniciais.
    /// </summary>
    public void ResetValues()
    {
        npc = null;
        isWonCombat = false;
        difficulty = null;
        rewardType = null;
        rewardNeed = null;
        rewardWeapon = null;
    }

    /// <summary>
    /// Retorna ou define a arma atualmente em mãos do jogador.
    /// </summary>
    public GameObject WeaponInHand
    {
        get { return weaponInHand; }
        set { weaponInHand = value; }
    }

    /// <summary>
    /// Retorna ou define o NPC envolvido no combate.
    /// </summary>
    public GameObject Npc
    {
        get { return npc; }
        set { npc = value; }
    }

    /// <summary>
    /// Retorna ou define se o combate foi vencido.
    /// </summary>
    public bool IsWonCombat
    {
        get { return isWonCombat; }
        set { isWonCombat = value; }
    }

    /// <summary>
    /// Retorna ou define a dificuldade do combate.
    /// </summary>
    public Difficulty? Difficulty
    {
        get { return difficulty; }
        set { difficulty = value; }
    }

    /// <summary>
    /// Retorna ou define o tipo de recompensa recebida pelo combate.
    /// </summary>
    public RewardType? RewardType
    {
        get { return rewardType; }
        set { rewardType = value; }
    }

    /// <summary>
    /// Retorna ou define a necessidade como recompensa do combate.
    /// </summary>
    public Need? RewardNeed
    {
        get { return rewardNeed; }
        set { rewardNeed = value; }
    }

    /// <summary>
    /// Retorna ou define o nome da arma recebida como recompensa do combate.
    /// </summary>
    public string RewardWeapon
    {
        get { return rewardWeapon; }
        set { rewardWeapon = value; }
    }
}


/// <summary>
/// Tipos de recompensa que podem ser obtidos após um combate.
/// </summary>
public enum RewardType
{
    Weapon, // Recompensa é uma arma
    Need    // Recompensa é uma necessidade
}

/// <summary>
/// Necessidades que podem ser recebidas como recompensa após um combate.
/// </summary>
public enum Need
{
    Hunger, // Fome
    Thirst, // Sede
    Health  // Saúde
}

/// <summary>
/// Dificuldades possíveis para um combate.
/// </summary>
public enum Difficulty
{
    Easy,   // Fácil
    Medium, // Médio
    Hard    // Difícil
}
