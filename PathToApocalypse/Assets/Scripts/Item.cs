using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Game/Item")]
public class Item : ScriptableObject
{
    [SerializeField] private string itemName;
    [SerializeField] private Sprite icon;
    [SerializeField] private GameObject itemPrefab;
	[SerializeField] private int damage;
    [SerializeField] private int damageOne;
    [SerializeField] private int damageTwo;

    public GameObject ItemPrefab
    {
        get { return itemPrefab; }
        set { itemPrefab = value; }
    }

    public string ItemName
    {
        get { return itemName; }
        set { itemName = value; }
    }
    public Sprite Icon
    {
        get { return icon; }
        set { icon = value; }
    }

    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    public int DamageOne
    {
        get { return damageOne; }
        set { damageOne = value; }
    }

    public int DamageTwo
    {
        get { return damageTwo; }
        set { damageTwo = value; }
    }
}
