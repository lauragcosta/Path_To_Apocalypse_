using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Classe respons�vel por atualizar a barra de vida de um inimigo na interface.
/// </summary>
public class EnemyStats : MonoBehaviour
{
    public Slider healthBar; // Refer�ncia ao controle deslizante da barra de vida

    /// <summary>
    /// M�todo chamado no in�cio do jogo para inicializar a barra de vida.
    /// </summary>
    void Start()
    {
        UpdateHealthBar();
    }

    /// <summary>
    /// M�todo chamado a cada quadro para atualizar a barra de vida.
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
        // Obt�m o script EnemyMovement associado a este GameObject
        EnemyMovement enemyScript = gameObject.GetComponent<EnemyMovement>();

        // Define o valor da barra de vida com base na vida atual do inimigo
        healthBar.value = enemyScript.GetLife();
    }
}
