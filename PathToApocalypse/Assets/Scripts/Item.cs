using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
	public string itemName;
	public Sprite icon;
	private GameObject itemPrefab;
	private int damage;
}
