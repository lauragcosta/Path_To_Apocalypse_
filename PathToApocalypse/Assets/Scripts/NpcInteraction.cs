using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NpcInteraction : MonoBehaviour
{
    private bool isCharacterInside = false;
    private int currentClicks = 0;
    private List<GameObject> textObjects = new();
    private Transform speechBubbleCanvas;
    [SerializeField] private TextMeshProUGUI npcName;
    private Transform speechBubble;
    private Transform confirmation;
    [SerializeField] private CombatData combatData;

    private void Start()
    {
        confirmation = transform.Find("Confirmation");
        confirmation.gameObject.SetActive(false);
        speechBubbleCanvas = transform.Find("SpeechBubbleCanvas");
        if (speechBubbleCanvas != null)
        {
            speechBubble = speechBubbleCanvas.Find("SpeechBubble");
            if (speechBubble != null)
            {
                FindAllTextObjects(speechBubble, textObjects);
                if (textObjects.Count > 0)
                {
                    textObjects[currentClicks].SetActive(true);
                }
            }
        }
    }
    void Update()
    {
        if (isCharacterInside && Input.GetKeyDown(KeyCode.Space))
        {
            // Ensure currentClicks does not go out of bounds
            if (currentClicks < textObjects.Count)
            {
                // Deactivate the current text object
                textObjects[currentClicks].SetActive(false);
                // Move to the next text object
                currentClicks = PassToNextText(currentClicks);
            }
        }

        ShowConfirmationWindow();
    }

    void ShowConfirmationWindow()
    {
        // If all text objects have been shown, show the confirmation
        if (currentClicks >= textObjects.Count)
        {
            speechBubbleCanvas.gameObject.SetActive(false);
            if (confirmation != null)
            {
                confirmation.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.K))
                {
                    combatData.ResetValues();
                    combatData.Npc = gameObject;
                    switch (npcName.text)
                    {
                        case "John":
                            combatData.Difficulty = Difficulty.Medium;
                            combatData.RewardType = RewardType.Weapon;
                            combatData.RewardWeapon = "dagger";
                            break;
                        case "Miguel":
                            combatData.Difficulty = Difficulty.Hard;
                            combatData.RewardType = RewardType.Weapon;
                            combatData.RewardWeapon = "axe";
                            break;
                        case "Maria":
                            combatData.Difficulty = Difficulty.Easy;
                            combatData.RewardType = RewardType.Need;
                            combatData.RewardNeed = Need.Hunger;
                            break;
                    }
                    SceneController.Instance.LoadScene("level1ApartmentScene");
                }
            }
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isCharacterInside = true;
            currentClicks = 0; // Reset clicks when entering the trigger
            if (speechBubbleCanvas != null && speechBubble != null)
            {
                // Activate the SpeechBubbleCanvas and the first text object
                speechBubbleCanvas.gameObject.SetActive(true);
                speechBubble.gameObject.SetActive(true);
                if (textObjects.Count > 0)
                {
                    textObjects[currentClicks].SetActive(true);
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isCharacterInside = false;
            // Optionally deactivate all text objects when player exits
            foreach (var textObj in textObjects)
            {
                textObj.SetActive(false);
            }
            speechBubbleCanvas.gameObject.SetActive(false); // Deactivate the SpeechBubbleCanvas
            confirmation.gameObject.SetActive(false);
        }
    }

    // Recursive method to find all "Text" objects
    void FindAllTextObjects(Transform parent, List<GameObject> list)
    {
        foreach (Transform child in parent)
        {
            if (child.name == "Text")
            {
                list.Add(child.gameObject);
            }
            FindAllTextObjects(child, list); // Recursive call
        }
    }

    int PassToNextText(int currentClicks)
    {
        currentClicks++; // Move to the next click count
        if (currentClicks < textObjects.Count)
        {
            textObjects[currentClicks].SetActive(true); // Activate the next text object
        }
    
        return currentClicks;
    }
}
