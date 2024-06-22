using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class TutorialManager : MonoBehaviour
{
	public TextMeshProUGUI instructionsText;
	private int currentStep = 0;
	private bool isTyping = false;

	private string[] tutorialTexts = new string[]
	{
		"Welcome to Path to Apocalypse, an immersive 2D action-adventure game where survival is your ultimate goal. To do that, you must navigate through the map, help the NPCs by entering turn-based combats.\n\nPress 'Enter' to continue.",
		"Welcome to the Game Tutorial!\n\n- Use WASD keys to move while on the map. \n\n When a combat starts it's point and click, click on the zombies you want to attack and choose an attack \n- Finish the combats successfully in order to gain rewards.\n\nPress 'Enter' to continue.",
		"\nDon't forget to keep your hunger health and thirst bar high to win more combats!\n\nPress 'Enter' to return to menu!"
	};

	void Start()
	{
		StartCoroutine(TypeText(tutorialTexts[currentStep]));
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Return) && !isTyping)
		{
			currentStep++;
			if (currentStep < tutorialTexts.Length)
			{
				StartCoroutine(TypeText(tutorialTexts[currentStep]));
			}
			else
			{
				SceneController.Instance.LoadScene("Map1");
			}
		}
	}

	IEnumerator TypeText(string text)
	{
		isTyping = true;
		instructionsText.text = "";
		foreach (char letter in text.ToCharArray())
		{
			instructionsText.text += letter;
			yield return new WaitForSeconds(0.05f);
		}
		isTyping = false;
	}
}
