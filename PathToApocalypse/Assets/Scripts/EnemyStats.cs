using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Classe responsável por atualizar a barra de vida de um inimigo na interface.
/// </summary>
public class EnemyStats : MonoBehaviour
{
    public Slider healthBar; // Referência ao controle deslizante da barra de vida

    /// <summary>
    /// Método chamado no início do jogo para inicializar a barra de vida.
    /// </summary>
    void Start()
    {
        UpdateHealthBar();
    }

    /// <summary>
    /// Método chamado a cada quadro para atualizar a barra de vida.
    /// </summary>
    void Update()
    {
        UpdateHealthBar();
    }

    /// <summary>
    /// Atualiza a barra de vida com base nos dados do inimigo.
    /// </summary>
    private void UpdateHealthBar()
    {
        // Obtém o script EnemyMovement associado a este GameObject
        EnemyMovement enemyScript = gameObject.GetComponent<EnemyMovement>();

        // Define o valor da barra de vida com base na vida atual do inimigo
        healthBar.value = enemyScript.GetLife();
    }
}
