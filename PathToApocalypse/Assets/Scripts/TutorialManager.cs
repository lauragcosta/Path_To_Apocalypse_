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
		"Welcome to Path to Apocalypse, an immersive 2D action-adventure game where survival is your ultimate goal. To do that, you must navigate through a desolate landscape where NPCs will ask for your help in exchange for the items you need. Your skills will be put to the test as you manage your hunger, thirst, and health while fending off relentless enemies.\n\nPress 'Enter' to continue.",
		"Welcome to the Game Tutorial!\n\n- Use WASD keys to move.\n- Press Space to attack.\n- Defeat the zombies to gain items.\n- Avoid enemies to stay alive.\n\nPress 'Enter' to continue.",
		"Find the NPCs thoughout the map and kill all the zombies to complete the goals for the day!\nDon't forget to keep your hunger and thirst bar high!\n\nPress 'Enter' to begin the game!"
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
				SceneManager.LoadScene("Map1");
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
