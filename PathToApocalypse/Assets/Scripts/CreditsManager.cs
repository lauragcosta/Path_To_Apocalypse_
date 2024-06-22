using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour
{
	public TextMeshProUGUI creditsText;
	public float rollSpeed = 30f;

	private RectTransform creditsRectTransform;

	void Start()
	{
		creditsRectTransform = creditsText.GetComponent<RectTransform>();
		StartCoroutine(StartRolling());
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Return))
		{
			SceneController.Instance.LoadScene("MainMenu");
		}
	}

	IEnumerator StartRolling()
	{
		RectTransform parentRectTransform = creditsText.transform.parent.GetComponent<RectTransform>();
		float parentHeight = parentRectTransform.rect.height;

		float finalPosition = creditsRectTransform.anchoredPosition.y + creditsRectTransform.rect.height + parentHeight;

		while (creditsRectTransform.anchoredPosition.y < finalPosition)
		{
			creditsRectTransform.anchoredPosition += new Vector2(0, rollSpeed * Time.deltaTime);
			yield return null;
		}
	}
}
