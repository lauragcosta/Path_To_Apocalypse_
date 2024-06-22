using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTurnStats : MonoBehaviour
{
    public Slider healthBar;
    public Slider thirstBar;
    public Slider hungerBar;
    void Start()
    {
        UpdateHealthBar();
        UpdateThirstBar();
        UpdateHungerBar();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthBar();
        UpdateThirstBar();
        UpdateHungerBar();
    }

    private void UpdateHealthBar()
    {
        PlayerControllerTurn playerScript = gameObject.GetComponent<PlayerControllerTurn>();
        healthBar.value = playerScript.GetLife();
    }

    private void UpdateThirstBar()
    {
        PlayerControllerTurn playerScript = gameObject.GetComponent<PlayerControllerTurn>();
        thirstBar.value = playerScript.GetThirst();
    }

    private void UpdateHungerBar()
    {
        PlayerControllerTurn playerScript = gameObject.GetComponent<PlayerControllerTurn>();
        hungerBar.value = playerScript .GetHunger();
    }
}
