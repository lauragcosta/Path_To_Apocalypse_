using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
	[SerializeField] private Button story;
	[SerializeField] private Button tutorial;
	[SerializeField] private Button credits;
	[SerializeField] private Button quit;

	private void Start()
	{
		story.onClick.AddListener(() => PlayGame());
		tutorial.onClick.AddListener(() => Tutorial());
		credits.onClick.AddListener(() => Credits());
		quit.onClick.AddListener(() => QuitGame());
	}
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
