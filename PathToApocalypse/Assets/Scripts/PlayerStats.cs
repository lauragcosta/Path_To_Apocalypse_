using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
	public Slider healthBar;
	public Slider thirstBar;
	public Slider hungerBar;

	private float maxHealth = 100f;
	private float currentHealth;
	private float maxThirst = 100f;
	private float currentThirst;
	private float maxHunger = 100f;
	private float currentHunger;

	void Start()
	{
		currentHealth = maxHealth;
		currentThirst = maxThirst;
		currentHunger = maxHunger;

		UpdateHealthBar();
		UpdateThirstBar();
		UpdateHungerBar();
	}

	void Update()
	{
		currentHunger -= Time.deltaTime;
		currentThirst -= Time.deltaTime;

		UpdateHealthBar();
		UpdateThirstBar();
		UpdateHungerBar();
	}

	public void TakeDamage(float amount)
	{
		currentHealth -= amount;
		if (currentHealth < 0) currentHealth = 0;
		UpdateHealthBar();
	}

	public void EatFood(float amount)
	{
		currentHunger += amount;
		if (currentHunger > maxHunger) currentHunger = maxHunger;
		UpdateHungerBar();
	}

	public void DrinkWater(float amount)
	{
		currentThirst += amount;
		if (currentThirst > maxThirst) currentThirst = maxThirst;
		UpdateThirstBar();
	}

	void UpdateHealthBar()
	{
		healthBar.value = currentHealth / maxHealth;
	}

	void UpdateThirstBar()
	{
		thirstBar.value = currentThirst / maxThirst;
	}

	void UpdateHungerBar()
	{
		hungerBar.value = currentHunger / maxHunger;
	}
}
