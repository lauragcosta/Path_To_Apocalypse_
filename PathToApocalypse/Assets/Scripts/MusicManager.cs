using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
	// Define your audio clips for different scenes
	public AudioClip menuMusic;
	public AudioClip tutorialMusic;
	public AudioClip creditsMusic;

	// Reference to the AudioSource component attached to this GameObject
	private AudioSource audioSource;

	// Singleton instance to ensure only one AudioManager exists
	private static MusicManager instance;

	void Awake()
	{
		// Ensure only one instance of MusicManager exists
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject); // Persist across scene changes
		}
		else
		{
			Destroy(gameObject); // Destroy duplicate instances
		}

		// Get the AudioSource component attached to this GameObject
		audioSource = GetComponent<AudioSource>();

		// Play menu music by default
		PlayMenuMusic();
	}

	void Update()
	{
		// Check if the current scene is different from the previous one
		if (SceneManager.GetActiveScene().name != audioSource.clip.name)
		{
			// Adjust music based on the current scene
			switch (SceneManager.GetActiveScene().name)
			{
				case "MainMenu":
					PlayMenuMusic();
					break;
				case "Tutorial":
					PlayTutorialMusic();
					break;
				case "Credits":
					PlayCreditsMusic();
					break;
				default:
					// Default to menu music if scene not recognized
					PlayMenuMusic();
					break;
			}
		}
	}

	void PlayMenuMusic()
	{
		audioSource.Stop();
		audioSource.clip = menuMusic;
		audioSource.loop = true;
		audioSource.Play();
	}

	void PlayTutorialMusic()
	{
		audioSource.Stop();
		audioSource.clip = tutorialMusic;
		audioSource.loop = true;
		audioSource.Play();
	}

	void PlayCreditsMusic()
	{
		audioSource.Stop();
		audioSource.clip = creditsMusic;
		audioSource.loop = true;
		audioSource.Play();
	}
}
