using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance { get; private set; }
    private string previousScene;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene(string sceneName)
    {
        previousScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }

    public string GetPreviousScene()
    {
        return previousScene;
    }
}
