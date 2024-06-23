using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Classe respons�vel por permitir que o jogador entre em diferentes tipos de edif�cios no jogo.
/// </summary>
public class EnterBuilding : MonoBehaviour
{
    private Transform confirmation; // Refer�ncia ao objeto de confirma��o dentro do edif�cio
    [SerializeField] private CombatData combatData; // Dados de combate para passar para a cena de combate
    private bool isCharacterInside = false; // Indica se o jogador est� dentro do gatilho do edif�cio

    void Start()
    {
        confirmation = transform.Find("confirmation"); // Procura pelo objeto de confirma��o dentro do edif�cio
    }

    void Update()
    {
        // Verifica se o jogador pressiona 'K' para confirmar a entrada no edif�cio
        if (Input.GetKeyDown(KeyCode.K) && isCharacterInside)
        {
            // Define os dados de combate com base na tag do edif�cio
            if (gameObject.CompareTag("Hospital"))
            {
              //  combatData.ResetValues();
                combatData.Difficulty = Difficulty.Hard;
                combatData.RewardType = RewardType.Need;
                combatData.RewardNeed = Need.Health;
                SceneManager.LoadScene("HospitalScene");
            }
            else if (gameObject.CompareTag("Bar"))
            {
                //  combatData.ResetValues();
                combatData.Difficulty = Difficulty.Easy;
                combatData.RewardType = RewardType.Need;
                combatData.RewardNeed = Need.Thirst;
                SceneManager.LoadScene("BarScene");
            }
            else if (gameObject.CompareTag("SuperMarket"))
            {
                //  combatData.ResetValues();
                combatData.Difficulty = Difficulty.Easy;
                combatData.RewardType = RewardType.Need;
                combatData.RewardNeed = Need.Hunger;
                SceneManager.LoadScene("SupermarketScene");
            }
            else if (gameObject.CompareTag("Prison"))
            {
                //   combatData.ResetValues();
                combatData.Difficulty = Difficulty.Hard;
                combatData.RewardType = RewardType.Weapon;
                combatData.RewardWeapon = "sword";
                SceneManager.LoadScene("PrisionScene");
            }

            // Carrega a cena de combate ap�s definir os dados de combate
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isCharacterInside = true;
            // Ativa o objeto de confirma��o quando o jogador entra no gatilho do edif�cio
            if (confirmation != null)
            {
                confirmation.gameObject.SetActive(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Desativa o objeto de confirma��o quando o jogador sai do gatilho do edif�cio
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
