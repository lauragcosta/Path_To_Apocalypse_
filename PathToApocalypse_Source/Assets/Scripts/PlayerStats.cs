using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Classe respons�vel por gerenciar as estat�sticas e a��es do jogador.
/// </summary>
public class PlayerStats : MonoBehaviour
{
    private Inventory playerInventory; // Refer�ncia para o invent�rio do jogador
    [SerializeField] private CombatData combatData; // Dados de combate do jogador
    [SerializeField] private PlayerHealth playerHealth; // Sa�de, fome e sede do jogador
    public Slider healthBar; // Barra de sa�de do jogador
    public Slider thirstBar; // Barra de sede do jogador
    public Slider hungerBar; // Barra de fome do jogador
    [SerializeField] private TextMeshProUGUI objectivesText; // Texto dos objetivos do jogador
    private List<GameObject> listOfNpcs = new List<GameObject>(); // Lista de NPCs encontrados
    [SerializeField] private Transform npcBox; // Caixa que cont�m os NPCs

    /// <summary>
    /// Inicializa��es iniciais ao iniciar a cena.
    /// </summary>
    private void Start()
    {
        // Encontra objetos NPC e atualiza objetivos se npcBox n�o for nulo
        if (npcBox != null)
        {
            FindAllNpcObjects(npcBox, listOfNpcs);
            UpdateObjectives();
        }

        // Atualiza as barras de estat�sticas do jogador
        UpdateHealthBar();
        UpdateThirstBar();
        UpdateHungerBar();

        // Verifica a cena anterior e realiza a��es correspondentes
        switch (SceneController.Instance.GetPreviousScene())
        {
            case "level1ApartmentFight":
                HandleApartmentFight();
                break;
            case "BarScene":
                HandleBarScene();
                break;
            case "SupermarketScene":
                HandleSupermarketScene();
                break;
            case "PrisionScene":
                HandlePrisonScene();
                break;
            case "HospitalScene":
                HandleHospitalScene();
                break;
        }
    }

    /// <summary>
    /// Lida com as a��es ap�s vencer a luta no apartamento.
    /// </summary>
    private void HandleApartmentFight()
    {
        if (combatData.IsWonCombat)
        {
            switch (combatData.Difficulty.Value)
            {
                case Difficulty.Easy:
                    EatFood(5);
                    DrinkWater(3);
                    DestroyNpc();
                    break;
                case Difficulty.Medium:
                    EatFood(2);
                    DrinkWater(2);
                    playerInventory.AddItemToInventory(combatData.RewardWeapon);
                    DestroyNpc();
                    break;
                case Difficulty.Hard:
                    EatFood(4);
                    DrinkWater(4);
                    playerInventory.AddItemToInventory(combatData.RewardWeapon);
                    DestroyNpc();
                    break;
            }
        }
        else if (!combatData.IsWonCombat)
        {
            TakeDamage(20);
            NotEatFood(3);
            NotDrinkWater(3);
        }
    }

    /// <summary>
    /// Lida com as a��es ap�s vencer no Bar.
    /// </summary>
    private void HandleBarScene()
    {
        if (combatData.IsWonCombat)
        {
            DrinkWater(5);
        }
        else
        {
            TakeDamage(10);
            NotEatFood(1);
            NotDrinkWater(1);
        }
    }

    /// <summary>
    /// Lida com as a��es ap�s vencer no Supermercado.
    /// </summary>
    private void HandleSupermarketScene()
    {
        if (combatData.IsWonCombat)
        {
            EatFood(5);
        }
        else
        {
            TakeDamage(10);
            NotEatFood(1);
            NotDrinkWater(1);
        }
    }

    /// <summary>
    /// Lida com as a��es ap�s vencer na Pris�o.
    /// </summary>
    private void HandlePrisonScene()
    {
        if (combatData.IsWonCombat)
        {
            playerInventory.AddItemToInventory(combatData.RewardWeapon);
            EatFood(5);
            DrinkWater(5);
        }
        else
        {
            TakeDamage(50);
            NotEatFood(3);
            NotDrinkWater(3);
        }
    }

    /// <summary>
    /// Lida com as a��es ap�s vencer no Hospital.
    /// </summary>
    private void HandleHospitalScene()
    {
        if (combatData.IsWonCombat)
        {
            GainHealth(50);
        }
        else
        {
            TakeDamage(50);
            NotEatFood(3);
            NotDrinkWater(3);
        }
    }

    /// <summary>
    /// Destroi o NPC ap�s a intera��o.
    /// </summary>
    private void DestroyNpc()
    {
        if (combatData.Npc != null)
        {
            Destroy(combatData.Npc.gameObject);
        }
    }

    /// <summary>
    /// Atualiza as barras de sa�de, sede e fome do jogador.
    /// </summary>
    private void Update()
    {
        UpdateHealthBar();
        UpdateThirstBar();
        UpdateHungerBar();
    }

    /// <summary>
    /// Verifica se a cena atual � a cena especificada.
    /// </summary>
    /// <param name="sceneName">Nome da cena a ser verificada.</param>
    /// <returns>True se a cena atual for a cena especificada, caso contr�rio, false.</returns>
    private bool CurrentScene(string sceneName)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        return currentScene.name == sceneName;
    }

    /// <summary>
    /// Causa dano ao jogador e atualiza a barra de sa�de.
    /// </summary>
    /// <param name="damage">Quantidade de dano a ser causado.</param>
    public void TakeDamage(int damage)
    {
        playerHealth.Health -= damage;
        if (playerHealth.Health < 0) playerHealth.Health = 0;
        UpdateHealthBar();
    }

    /// <summary>
    /// Ganha sa�de para o jogador e atualiza a barra de sa�de.
    /// </summary>
    /// <param name="health">Quantidade de sa�de a ser ganha.</param>
    public void GainHealth(int health)
    {
        playerHealth.Health += health;
        if (playerHealth.Health > 100) playerHealth.Health = 100;
        UpdateHealthBar();
    }

    /// <summary>
    /// Aumenta a fome do jogador e atualiza a barra de fome.
    /// </summary>
    /// <param name="amount">Quantidade de aumento da fome.</param>
    public void EatFood(int amount)
    {
        playerHealth.Hunger += amount;
        if (playerHealth.Hunger > 10) playerHealth.Hunger = 10;
        UpdateHungerBar();
    }

    /// <summary>
    /// Reduz a fome do jogador e atualiza a barra de fome.
    /// </summary>
    /// <param name="amount">Quantidade de redu��o da fome.</param>
    public void NotEatFood(int amount)
    {
        playerHealth.Hunger -= amount;
        if (playerHealth.Hunger <= 0) Destroy(gameObject);
        UpdateHungerBar();
    }

    /// <summary>
    /// Aumenta a sede do jogador e atualiza a barra de sede.
    /// </summary>
    /// <param name="amount">Quantidade de aumento da sede.</param>
    public void DrinkWater(int amount)
    {
        playerHealth.Thirst += amount;
        if (playerHealth.Thirst > 10) playerHealth.Thirst = 10;
        UpdateThirstBar();
    }

    /// <summary>
    /// Reduz a sede do jogador e atualiza a barra de sede.
    /// </summary>
    /// <param name="amount">Quantidade de redu��o da sede.</param>
    public void NotDrinkWater(int amount)
    {
        playerHealth.Thirst += amount;
        if (playerHealth.Thirst < 10) Destroy(gameObject);
        UpdateThirstBar();
    }

    /// <summary>
    /// Atualiza a barra de sa�de do jogador.
    /// </summary>
    private void UpdateHealthBar()
    {
        healthBar.value = playerHealth.Health;
    }

    /// <summary>
    /// Atualiza a barra de sede do jogador.
    /// </summary>
    private void UpdateThirstBar()
    {
        thirstBar.value = playerHealth.Thirst;
    }

    /// <summary>
    /// Atualiza a barra de fome do jogador.
    /// </summary>
    private void UpdateHungerBar()
    {
        hungerBar.value = playerHealth.Hunger;
    }

    /// <summary>
    /// M�todo recursivo para encontrar todos os objetos "NPC".
    /// </summary>
    /// <param name="parent">Transform pai a ser pesquisado.</param>
    /// <param name="list">Lista para armazenar os objetos "NPC".</param>
    void FindAllNpcObjects(Transform parent, List<GameObject> list)
    {
        foreach (Transform child in parent)
        {
            if (child.CompareTag("NPC"))
            {
                list.Add(child.gameObject);
            }
            FindAllNpcObjects(child, list); // Chamada recursiva
        }
    }

    /// <summary>
    /// Atualiza o texto dos objetivos do jogador com base na lista de NPCs encontrados.
    /// </summary>
    void UpdateObjectives()
    {
        objectivesText.text = $"Objectives\r\nHelp people around the map: {listOfNpcs.Count}";
    }
}
