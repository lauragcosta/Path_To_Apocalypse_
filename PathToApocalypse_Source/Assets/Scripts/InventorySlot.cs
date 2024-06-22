using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Representa um slot de inventário que pode conter um item.
/// </summary>
public class InventorySlot : MonoBehaviour
{
    private Item[] allItems; // Array de todos os itens disponíveis no inventário
    private bool isSelected; // Indica se este slot está selecionado
    private bool isEmpty = true; // Indica se este slot está vazio
    private Item itemInSlot; // Item atualmente no slot
    [SerializeField] private CombatData combatData; // Dados de combate associados ao slot
    private Image slotImage; // Componente de imagem para exibir o ícone do item

    void Start()
    {
        // Obtém o componente de imagem neste GameObject ou em seus filhos
        slotImage = gameObject.GetComponentInChildren<Image>();

        // Carrega todos os itens do tipo Item da pasta Resources/Items
        allItems = Resources.LoadAll<Item>("Items");
    }

    /// <summary>
    /// Retorna se este slot está selecionado.
    /// </summary>
    /// <returns>True se o slot estiver selecionado, caso contrário, false.</returns>
    public bool IsSelected()
    {
        return isSelected;
    }

    /// <summary>
    /// Adiciona um item ao slot com base no nome do item.
    /// </summary>
    /// <param name="name">O nome do item a ser adicionado.</param>
    public void AddItemToSlot(string name)
    {
        // Verifica se o slot está vazio
        if (isEmpty)
        {
            isEmpty = true;

            // Procura o item pelo nome na lista de todos os itens carregados
            foreach (Item i in allItems)
            {
                if (i.ItemName.Equals(name))
                {
                    slotImage.sprite = i.Icon; // Define o sprite do item no componente de imagem
                    itemInSlot = i; // Atribui o item ao slot
                    isEmpty = false;
                    break;
                }
            }
        }
        else
        {
            isEmpty = false;
            Debug.LogWarning("Slot is not empty."); // Aviso de que o slot não está vazio
        }
    }

    /// <summary>
    /// Retorna o item atualmente no slot.
    /// </summary>
    /// <returns>O item no slot.</returns>
    public Item GetItemInSlot()
    {
        return itemInSlot;
    }

    /// <summary>
    /// Verifica se o slot está vazio.
    /// </summary>
    /// <returns>True se o slot estiver vazio, caso contrário, false.</returns>
    public bool IsEmpty()
    {
        return isEmpty;
    }

    /// <summary>
    /// Evento chamado quando o botão associado ao slot é clicado.
    /// </summary>
    public void OnButtonClick()
    {
        combatData.WeaponInHand = null; // Limpa a arma na mão do jogador
        isSelected = true; // Define o slot como selecionado
        Debug.Log("Selected: " + (itemInSlot != null ? itemInSlot.ItemName : "No item selected")); // Debuga o nome do item selecionado
        combatData.WeaponInHand = itemInSlot != null ? itemInSlot.ItemPrefab : null; // Atribui o prefab da arma à mão do jogador, se houver
    }
}
