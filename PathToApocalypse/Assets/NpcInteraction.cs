using System.Collections.Generic;
using UnityEngine;

public class NpcInteraction : MonoBehaviour
{
    private bool isCharacterInside = false;
    private int currentClicks = 0;
    private List<GameObject> textObjects = new List<GameObject>();

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
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isCharacterInside = true;
            currentClicks = 0; // Reset clicks when entering the trigger
            textObjects.Clear(); // Clear the list to avoid duplicates

            Transform speechBubbleCanvas = transform.Find("SpeechBubbleCanvas");
            if (speechBubbleCanvas != null)
            {
                Transform speechBubble = speechBubbleCanvas.Find("SpeechBubble");
                if (speechBubble != null)
                {
                    speechBubble.gameObject.SetActive(true);
                    speechBubbleCanvas.gameObject.SetActive(true); // Activate the SpeechBubbleCanvas
                    FindAllTextObjects(speechBubble, textObjects);
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
            Transform speechBubbleCanvas = transform.Find("SpeechBubbleCanvas");
            if (speechBubbleCanvas != null)
            {
                speechBubbleCanvas.gameObject.SetActive(false); // Deactivate the SpeechBubbleCanvas
            }
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
