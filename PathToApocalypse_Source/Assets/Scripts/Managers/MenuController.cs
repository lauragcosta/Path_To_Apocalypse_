using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
	public void PlayGame()
	{
        SceneController.Instance.LoadScene("StoryScene");
	}

	public void Tutorial()
	{
        SceneController.Instance.LoadScene("Tutorial");
	}

	public void Credits()
	{
        SceneController.Instance.LoadScene("Credits");
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}
