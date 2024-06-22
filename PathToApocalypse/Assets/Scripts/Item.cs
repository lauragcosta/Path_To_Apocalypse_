using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
	public string itemName;
	public Sprite icon;
	[SerializeField] private int damage;
    [SerializeField] private int damageOne;
    [SerializeField] private int damageTwo;
    private GameObject itemPrefab;


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
