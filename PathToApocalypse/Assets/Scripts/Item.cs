using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Game/Item")]
public class Item : ScriptableObject
{
	private string itemName;
    private Sprite icon;
	private GameObject itemPrefab;
	[SerializeField] private int damage;

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

}
