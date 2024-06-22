using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
	public Image icon;
	public Button button;

	Item item;

	public void AddItem(Item newItem)
	{
		item = newItem;

		icon.sprite = item.icon;
		icon.enabled = true;
		button.interactable = true;

		button.onClick.AddListener(OnSelectItem);
	}

	public void ClearSlot()
	{
		item = null;

		icon.sprite = null;
		icon.enabled = false;
		button.interactable = false;

		button.onClick.RemoveListener(OnSelectItem);
	}

	public void OnSelectItem()
	{
		button.GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f, 1f);
	}

	public void OnDeselectItem()
	{
		button.GetComponent<Image>().color = Color.white;
	}
}
