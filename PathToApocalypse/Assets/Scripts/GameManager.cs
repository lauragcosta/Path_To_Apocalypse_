using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public GameObject playerPrefab;
	public GameObject canvasPrefab;

	private void Start()
	{
		GameObject playerInstance = Instantiate(playerPrefab);
		GameObject canvasInstance = Instantiate(canvasPrefab);

		PlayerStats playerStats = playerInstance.GetComponent<PlayerStats>();

		Slider healthBar = canvasInstance.transform.Find("HealthBar").GetComponent<Slider>();
		Slider thirstBar = canvasInstance.transform.Find("ThirstBar").GetComponent<Slider>();
		Slider hungerBar = canvasInstance.transform.Find("HungerBar").GetComponent<Slider>();

		playerStats.healthBar = healthBar;
		playerStats.thirstBar = thirstBar;
		playerStats.hungerBar = hungerBar;
	}
}
