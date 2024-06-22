using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    private Inventory playerInventory;
    [SerializeField] private CombatData combatData;
    [SerializeField] private PlayerHealth playerHealth;
    public Slider healthBar;
    public Slider thirstBar;
    public Slider hungerBar;
    [SerializeField] private TextMeshProUGUI objectivesText;
    private List<GameObject> listOfNpcs = new();
    [SerializeField] private Transform npcBox;

    private void Start()
    {
        // Find NPC objects and update objectives if npcBox is not null
        if (npcBox != null)
        {
            FindAllNpcObjects(npcBox, listOfNpcs);
            UpdateObjectives();
        }

        // Update player stats bars
        UpdateHealthBar();
        UpdateThirstBar();
        UpdateHungerBar();

        // Check the previous scene and perform actions accordingly
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

    private void DestroyNpc()
    {
        if (combatData.Npc != null)
        {
            Destroy(combatData.Npc.gameObject);
        }
    }


    private void Update()
    {

        // You may want to call this less frequently
        if (CurrentScene("Map1"))
        {
            UpdateHealthBar();
            UpdateThirstBar();
            UpdateHungerBar();
        }

    }

    private bool CurrentScene(string sceneName)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        return currentScene.name == sceneName;
    }

    public void TakeDamage(int damage)
    {
        playerHealth.Health -= damage;
        if (playerHealth.Health < 0) playerHealth.Health = 0;
        UpdateHealthBar();
    }

    public void GainHealth(int health)
    {
        playerHealth.Health += health;
        if (playerHealth.Health > 100) playerHealth.Health = 100;
        UpdateHealthBar();
    }

    public void EatFood(int amount)
    {
        playerHealth.Hunger += amount;
        if (playerHealth.Hunger > 10) playerHealth.Hunger = 10;
        UpdateHungerBar();
    }

    public void NotEatFood(int amount)
    {
        playerHealth.Hunger -= amount;
        if (playerHealth.Hunger <= 0) Destroy(gameObject);
        UpdateHungerBar();
    }

    public void DrinkWater(int amount)
    {
        playerHealth.Thirst += amount;
        if (playerHealth.Thirst > 10) playerHealth.Thirst = 10;
        UpdateThirstBar();
    }

    public void NotDrinkWater(int amount)
    {
        playerHealth.Thirst += amount;
        if (playerHealth.Thirst < 10) Destroy(gameObject);
        UpdateThirstBar();
    }

    private void UpdateHealthBar()
    {
        healthBar.value = playerHealth.Health;
    }

    private void UpdateThirstBar()
    {
        thirstBar.value = playerHealth.Thirst;
    }

    private void UpdateHungerBar()
    {
        hungerBar.value = playerHealth.Hunger;
    }

    // Recursive method to find all "NPC" objects
    void FindAllNpcObjects(Transform parent, List<GameObject> list)
    {
        foreach (Transform child in parent)
        {
            if (child.CompareTag("NPC"))
            {
                list.Add(child.gameObject);
            }
            FindAllNpcObjects(child, list); // Recursive call
        }
    }

    void UpdateObjectives()
    {
        objectivesText.text = $"Objectives\r\nHelp people around the map: {listOfNpcs.Count}";
    }
}
