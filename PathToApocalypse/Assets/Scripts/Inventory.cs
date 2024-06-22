using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Classe respons�vel por gerenciar o invent�rio do jogador.
/// </summary>
public class Inventory : MonoBehaviour
{
    private List<InventorySlot> inventorySlots = new List<InventorySlot>();

    // Start is called before the first frame update
    void Start()
    {
        // Obt�m todos os GameObjects filhos com a tag "InventorySlot" e adiciona-os � lista de slots do invent�rio.
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

        // Adiciona um item inicial ao invent�rio para teste (exemplo: "screwdriver")
        AddItemToInventory("screwdriver");
    }

    /// <summary>
    /// Adiciona um item ao invent�rio com base no objeto Item.
    /// </summary>
    /// <param name="item">O objeto Item a ser adicionado ao invent�rio.</param>
    public void AddItemToInventory(Item item)
    {
        foreach (InventorySlot slot in inventorySlots)
        {
            if (slot.IsEmpty())
            {
                slot.AddItemToSlot(item.ItemName);
                break; // Item adicionado, interrompe o loop
            }
        }
    }

    /// <summary>
    /// Adiciona um item ao invent�rio com base no nome do item.
    /// </summary>
    /// <param name="name">O nome do item a ser adicionado ao invent�rio.</param>
    public void AddItemToInventory(string name)
    {
        foreach (InventorySlot slot in inventorySlots)
        {
            if (slot.IsEmpty())
            {
                slot.AddItemToSlot(name);
                break; // Item adicionado, interrompe o loop
            }
        }
    }
}
