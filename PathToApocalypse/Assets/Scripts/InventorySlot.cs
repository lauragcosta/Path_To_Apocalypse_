using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    private Item[] allItems;
    private bool isSelected;
    private bool isEmpty = true;
    private Item itemInSlot;
    [SerializeField] private CombatData combatData;
    private Image slotImage;

    void Start()
    {
        slotImage = gameObject.GetComponentInChildren<Image>();
        allItems = Resources.LoadAll<Item>("Items");
    }

    public void AddItemToSlot(string name)
    { 
        // Check if the slot is empty
        if (isEmpty)
        {
            isEmpty = true;
            foreach (Item i in allItems)
            {
                if (i.ItemName.Equals(name))
                {
                    slotImage.sprite = i.Icon; // Assign the sprite to the Image component
                    itemInSlot = i;
                    isEmpty = false;
                    break;
                }
            }
        }
        else
        {
            isEmpty = false;
            Debug.LogWarning("Slot is not empty.");
        }
    }


    public Item GetItemInSlot()
    {
        return itemInSlot;
    }

    public bool IsEmpty()
    {
        return isEmpty;
    }

    public void OnButtonClick()
    {
        combatData.WeaponInHand = null;
        isSelected = true;
        Debug.Log("Selected: " + (itemInSlot != null ? itemInSlot.ItemName : "No item selected"));
        combatData.WeaponInHand = itemInSlot != null ? itemInSlot.ItemPrefab : null;
    }

}
