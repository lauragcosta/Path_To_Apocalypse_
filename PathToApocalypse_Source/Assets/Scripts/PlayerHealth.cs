using UnityEngine;

/// <summary>
/// Representa os atributos de sa�de do jogador.
/// </summary>
[CreateAssetMenu(fileName = "NewPlayerHealth", menuName = "Game/PlayerHealth")]
public class PlayerHealth : ScriptableObject
{
    [SerializeField] private float hunger; // N�vel de fome do jogador
    [SerializeField] private float thirst; // N�vel de sede do jogador
    [SerializeField] private int health; // Pontos de vida do jogador

    /// <summary>
    /// Retorna ou define o n�vel de fome do jogador.
    /// </summary>
    public float Hunger
    {
        get { return hunger; }
        set { hunger = value; }
    }

    /// <summary>
    /// Retorna ou define o n�vel de sede do jogador.
    /// </summary>
    public float Thirst
    {
        get { return thirst; }
        set { thirst = value; }
    }

    /// <summary>
    /// Retorna ou define os pontos de vida do jogador.
    /// </summary>
    public int Health
    {
        get { return health; }
        set { health = value; }
    }
}
