using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
	public List<Item> items = new List<Item>();
	public Transform inventoryPanel;

	void Start()
	{
		UpdateInventoryUI();
	}

	public void AddItem(Item item)
	{
		items.Add(item);
		UpdateInventoryUI();
	}

	public void RemoveItem(Item item)
	{
		items.Remove(item);
		UpdateInventoryUI();
	}

	void UpdateInventoryUI()
	{
		foreach (Transform slot in inventoryPanel)
		{
			Image icon = slot.GetComponent<Image>();
			int index = slot.GetSiblingIndex();

			if (index < items.Count)
			{
				icon.sprite = items[index].itemIcon;
				icon.enabled = true;
			}
			else
			{
				icon.enabled = false;
			}
		}
	}
}
