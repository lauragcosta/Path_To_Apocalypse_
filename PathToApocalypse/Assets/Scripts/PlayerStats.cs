using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    public Slider healthBar;
    public Slider thirstBar;
    public Slider hungerBar;

    private void Start()
    {
        UpdateHealthBar();
        UpdateThirstBar();
        UpdateHungerBar();
    }

    private void Update()
    {
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
}