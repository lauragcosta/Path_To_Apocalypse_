using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controla o carregamento de cenas e mantém o controle da cena anterior.
/// </summary>
public class SceneController : MonoBehaviour
{
    public static SceneController Instance { get; private set; } // Instância estática acessível globalmente

    private string previousScene; // Nome da cena anteriormente carregada

    /// <summary>
    /// Método chamado quando o script é inicializado.
    /// Garante que apenas uma instância deste objeto exista durante a execução do jogo.
    /// </summary>
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // Define esta instância como a instância única
            DontDestroyOnLoad(gameObject); // Mantém o objeto vivo entre as cenas
        }
        else
        {
            Destroy(gameObject); // Destroi o objeto se já existir outra instância
        }
    }

    /// <summary>
    /// Carrega uma cena específica.
    /// </summary>
    /// <param name="sceneName">Nome da cena a ser carregada.</param>
    public void LoadScene(string sceneName)
    {
        previousScene = SceneManager.GetActiveScene().name; // Armazena o nome da cena atual como cena anterior
        SceneManager.LoadScene(sceneName); // Carrega a cena com o nome especificado
    }

    /// <summary>
    /// Obtém o nome da cena anteriormente carregada.
    /// </summary>
    /// <returns>O nome da cena anterior.</returns>
    public string GetPreviousScene()
    {
        return previousScene; // Retorna o nome da cena anterior
    }
}
