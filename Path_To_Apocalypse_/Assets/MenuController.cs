using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
	public void PlayGame()
	{
		SceneManager.LoadScene("StoryScene");
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}
