using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterBuilding : MonoBehaviour
{
    private Transform confirmation;
    [SerializeField] private CombatData combatData;
    private bool isCharacterInside = false;

    void Start()
    {
        confirmation = transform.Find("confirmation");
    }

    private void Update()
    {
        // Check if the player presses 'K' to confirm entering the building
        if (Input.GetKeyDown(KeyCode.K) && isCharacterInside)
        {
            // Set combat data based on the building's tag
            if (gameObject.CompareTag("Hospital"))
            {
                combatData.ResetValues();
                combatData.Difficulty = Difficulty.Hard;
                combatData.RewardType = RewardType.Need;
                combatData.RewardNeed = Need.Health;
                SceneManager.LoadScene("HospitalScene");
            }
            else if (gameObject.CompareTag("Bar"))
            {
                combatData.ResetValues();
                combatData.Difficulty = Difficulty.Easy;
                combatData.RewardType = RewardType.Need;
                combatData.RewardNeed = Need.Thirst;
                SceneManager.LoadScene("BarScene");
            }
            else if (gameObject.CompareTag("SuperMarket"))
            {
                combatData.ResetValues();
                combatData.Difficulty = Difficulty.Easy;
                combatData.RewardType = RewardType.Need;
                combatData.RewardNeed = Need.Hunger;
                SceneManager.LoadScene("SupermarketScene");
            }
            else if (gameObject.CompareTag("Prison"))
            {
                combatData.ResetValues();
                combatData.Difficulty = Difficulty.Hard;
                combatData.RewardType = RewardType.Weapon;
                combatData.RewardWeapon = "sword";
                SceneManager.LoadScene("PrisionScene");
            }

            // Load the combat scene after setting combat data
            
        }
    }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                isCharacterInside = true;
                // Activate the confirmation object when the player enters the building trigger
                if (confirmation != null)
                {
                    confirmation.gameObject.SetActive(true);
                }
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            // Disable the confirmation object when the player exits the building trigger
            if (other.CompareTag("Player"))
            {
                isCharacterInside = false;
                if (confirmation != null)
                {
                    confirmation.gameObject.SetActive(false);
                }
            }
        }
}
