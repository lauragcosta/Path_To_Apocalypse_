using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    public Slider healthBar;
    public Slider thirstBar;
    public Slider hungerBar;
    [SerializeField] private TextMeshProUGUI objectivesText;
    private List<GameObject> listOfNpcs = new List<GameObject>();
    [SerializeField] private Transform npcBox;

    private void Start()
    {
        if (npcBox != null)
        {
            FindAllNpcObjects(npcBox, listOfNpcs);
            UpdateObjectives();
        }
        UpdateHealthBar();
        UpdateThirstBar();
        UpdateHungerBar();
    }

    private void Update()
    {

        // You may want to call this less frequently
        UpdateHealthBar();
        UpdateThirstBar();
        UpdateHungerBar();
    }

    public void TakeDamage(int damage)
    {
        playerHealth.Health -= damage;
        if (playerHealth.Health < 0) playerHealth.Health = 0;
        UpdateHealthBar();
    }

    public void EatFood(int amount)
    {
        playerHealth.Hunger += amount;
        if (playerHealth.Hunger > 10) playerHealth.Hunger = 10;
        UpdateHungerBar();
    }

    public void DrinkWater(int amount)
    {
        playerHealth.Thirst += amount;
        if (playerHealth.Thirst > 10) playerHealth.Thirst = 10;
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
