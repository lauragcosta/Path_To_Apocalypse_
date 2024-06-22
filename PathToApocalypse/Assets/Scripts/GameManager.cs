using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Gerenciador principal do jogo respons�vel por inicializar o jogador e a interface do usu�rio (UI).
/// </summary>
public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab; // Prefab do jogador para instanciar
    public GameObject canvasPrefab; // Prefab do canvas da UI para instanciar

    private void Start()
    {
        // Instancia o jogador e o canvas da UI
        GameObject playerInstance = Instantiate(playerPrefab);
        GameObject canvasInstance = Instantiate(canvasPrefab);

        // Obt�m a refer�ncia para o componente PlayerStats do jogador instanciado
        PlayerStats playerStats = playerInstance.GetComponent<PlayerStats>();

        // Obt�m as refer�ncias para as barras de sa�de, sede e fome do canvas da UI
        Slider healthBar = canvasInstance.transform.Find("HealthBar").GetComponent<Slider>();
        Slider thirstBar = canvasInstance.transform.Find("ThirstBar").GetComponent<Slider>();
        Slider hungerBar = canvasInstance.transform.Find("HungerBar").GetComponent<Slider>();

        // Atribui as barras ao jogador para atualiza��o de status
        playerStats.healthBar = healthBar;
        playerStats.thirstBar = thirstBar;
        playerStats.hungerBar = hungerBar;
    }
}
