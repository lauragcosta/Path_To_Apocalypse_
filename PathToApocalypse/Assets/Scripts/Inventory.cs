using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	public List<Item> items = new List<Item>();

	public delegate void OnItemChanged();
	public OnItemChanged onItemChangedCallback;

	public void Add(Item item)
	{
		items.Add(item);
		if (onItemChangedCallback != null)
			onItemChangedCallback.Invoke();
	}

	public void Remove(Item item)
	{
		items.Remove(item);
		if (onItemChangedCallback != null)
			onItemChangedCallback.Invoke();
	}
}
