using System.Collections;
using UnityEngine;
using TMPro;

public class StoryTextController : MonoBehaviour
{
	public TextMeshProUGUI storyText;
	public float typingSpeed = 0.05f;
	public string[] storyLines;

	private int currentLine = 0;

	void Start()
	{
		storyLines = new string[]
		{
			"The world was once a place of peace and prosperity...",
			"But everything changed when the apocalypse struck...",
			"Now, only the strongest survive in this desolate land..."
		};


		StartCoroutine(TypeStory());
	}

	IEnumerator TypeStory()
	{
		foreach (char letter in storyLines[currentLine].ToCharArray())
		{
			storyText.text += letter;
			yield return new WaitForSeconds(typingSpeed);
		}

		yield return new WaitForSeconds(2f);
		currentLine++;

		if (currentLine < storyLines.Length)
		{
			storyText.text = "";
			StartCoroutine(TypeStory());
		}
		else
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
		}
	}
}
