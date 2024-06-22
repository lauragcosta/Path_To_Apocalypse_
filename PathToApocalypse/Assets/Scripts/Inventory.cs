using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<InventorySlot> inventorySlots = new List<InventorySlot>();


    // Start is called before the first frame update
    void Start()
    {
        
        // Get all child GameObjects with the specified tag and add them to the list
        foreach (Transform child in transform)
        {
            if (child.CompareTag("InventorySlot"))
            {
                InventorySlot slot = child.GetComponent<InventorySlot>();
                if (slot != null)
                {
                    inventorySlots.Add(slot);
                }
            }
        }

        AddItemToInventory("screwdriver");
    }

    public void AddItemToInventory(Item item)
    {
        foreach (InventorySlot slot in inventorySlots)
        {
            if (slot.IsEmpty())
            {
                slot.AddItemToSlot(item.ItemName);
                break; // Item added, break the loop
            }
        }
    }

    public void AddItemToInventory(string name)
    {
        foreach (InventorySlot slot in inventorySlots)
        {
            if (slot.IsEmpty())
            {
                slot.AddItemToSlot(name);
                break; // Item added, break the loop
            }
        }
    }


}
