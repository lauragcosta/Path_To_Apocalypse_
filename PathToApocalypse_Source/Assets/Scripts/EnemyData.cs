using UnityEngine;

/// <summary>
/// ScriptableObject que armazena dados de um inimigo no jogo.
/// </summary>
[CreateAssetMenu(fileName = "NewEnemyData", menuName = "Game/EnemyData")]
public class EnemyData : ScriptableObject
{
    [SerializeField] private int life;        // Vida do inimigo
    [SerializeField] private int damage;      // Dano causado pelo inimigo
    [SerializeField] private float maxDistance;  // Distância máxima de ataque do inimigo
    [SerializeField] private float minDistance;  // Distância mínima de ataque do inimigo

    /// <summary>
    /// Obtém ou define a vida do inimigo.
    /// </summary>
    public int Life
    {
        get { return life; }
        set { life = value; }
    }

    /// <summary>
    /// Obtém ou define o dano causado pelo inimigo.
    /// </summary>
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    /// <summary>
    /// Obtém ou define a distância máxima de ataque do inimigo.
    /// </summary>
    public float MaxDistance
    {
        get { return maxDistance; }
        set { maxDistance = value; }
    }

    /// <summary>
    /// Obtém ou define a distância mínima de ataque do inimigo.
    /// </summary>
    public float MinDistance
    {
        get { return minDistance; }
        set { minDistance = value; }
    }
}
