using UnityEngine;

/// <summary>
/// Representa um item no jogo.
/// </summary>
[CreateAssetMenu(fileName = "NewItem", menuName = "Game/Item")]
public class Item : ScriptableObject
{
    [SerializeField] private string itemName; // Nome do item
    [SerializeField] private Sprite icon; // Ícone do item para exibição
    [SerializeField] private GameObject itemPrefab; // Prefab do item para instanciar no jogo
    [SerializeField] private int damage; // Dano base do item
    [SerializeField] private int damageOne; // Primeiro tipo de dano adicional
    [SerializeField] private int damageTwo; // Segundo tipo de dano adicional

    /// <summary>
    /// Retorna ou define o prefab associado a este item.
    /// </summary>
    public GameObject ItemPrefab
    {
        get { return itemPrefab; }
        set { itemPrefab = value; }
    }

    /// <summary>
    /// Retorna ou define o nome deste item.
    /// </summary>
    public string ItemName
    {
        get { return itemName; }
        set { itemName = value; }
    }

    /// <summary>
    /// Retorna ou define o ícone deste item.
    /// </summary>
    public Sprite Icon
    {
        get { return icon; }
        set { icon = value; }
    }

    /// <summary>
    /// Retorna ou define o dano base deste item.
    /// </summary>
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    /// <summary>
    /// Retorna ou define o primeiro tipo de dano adicional deste item.
    /// </summary>
    public int DamageOne
    {
        get { return damageOne; }
        set { damageOne = value; }
    }

    /// <summary>
    /// Retorna ou define o segundo tipo de dano adicional deste item.
    /// </summary>
    public int DamageTwo
    {
        get { return damageTwo; }
        set { damageTwo = value; }
    }
}
