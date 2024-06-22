using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Classe respons�vel por atualizar as barras de sa�de, sede e fome do jogador durante o turno do jogador.
/// </summary>
public class PlayerTurnStats : MonoBehaviour
{
    public Slider healthBar; // Barra de sa�de do jogador
    public Slider thirstBar; // Barra de sede do jogador
    public Slider hungerBar; // Barra de fome do jogador

    /// <summary>
    /// Inicializa as barras de sa�de, sede e fome do jogador ao iniciar a cena.
    /// </summary>
    void Start()
    {
        UpdateHealthBar();
        UpdateThirstBar();
        UpdateHungerBar();
    }

    /// <summary>
    /// Atualiza as barras de sa�de, sede e fome do jogador a cada quadro.
    /// </summary>
    void Update()
    {
        UpdateHealthBar();
        UpdateThirstBar();
        UpdateHungerBar();
    }

    /// <summary>
    /// Atualiza a barra de sa�de do jogador.
    /// </summary>
    private void UpdateHealthBar()
    {
        PlayerControllerTurn playerScript = gameObject.GetComponent<PlayerControllerTurn>();
        healthBar.value = playerScript.GetLife();
    }

    /// <summary>
    /// Atualiza a barra de sede do jogador.
    /// </summary>
    private void UpdateThirstBar()
    {
        PlayerControllerTurn playerScript = gameObject.GetComponent<PlayerControllerTurn>();
        thirstBar.value = playerScript.GetThirst();
    }

    /// <summary>
    /// Atualiza a barra de fome do jogador.
    /// </summary>
    private void UpdateHungerBar()
    {
        PlayerControllerTurn playerScript = gameObject.GetComponent<PlayerControllerTurn>();
        hungerBar.value = playerScript.GetHunger();
    }
}
