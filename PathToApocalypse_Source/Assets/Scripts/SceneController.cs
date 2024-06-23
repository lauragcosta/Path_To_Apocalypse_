using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controla o carregamento de cenas e mant�m o controle da cena anterior.
/// </summary>
public class SceneController : MonoBehaviour
{
    public static SceneController Instance { get; private set; } // Inst�ncia est�tica acess�vel globalmente

    private string previousScene; // Nome da cena anteriormente carregada

    /// <summary>
    /// M�todo chamado quando o script � inicializado.
    /// Garante que apenas uma inst�ncia deste objeto exista durante a execu��o do jogo.
    /// </summary>
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // Define esta inst�ncia como a inst�ncia �nica
            DontDestroyOnLoad(gameObject); // Mant�m o objeto vivo entre as cenas
        }
        else
        {
            Destroy(gameObject); // Destroi o objeto se j� existir outra inst�ncia
        }
    }

    /// <summary>
    /// Carrega uma cena espec�fica.
    /// </summary>
    /// <param name="sceneName">Nome da cena a ser carregada.</param>
    public void LoadScene(string sceneName)
    {
        previousScene = SceneManager.GetActiveScene().name; // Armazena o nome da cena atual como cena anterior
        SceneManager.LoadScene(sceneName); // Carrega a cena com o nome especificado
    }

    /// <summary>
    /// Obt�m o nome da cena anteriormente carregada.
    /// </summary>
    /// <returns>O nome da cena anterior.</returns>
    public string GetPreviousScene()
    {
        return previousScene; // Retorna o nome da cena anterior
    }
}
