using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Classe responsável por gerenciar a interação do jogador com um NPC.
/// Controla o fluxo de diálogos e ações baseadas na interação com o NPC.
/// </summary>
public class NpcInteraction : MonoBehaviour
{
    private bool isCharacterInside = false; // Indica se o personagem jogador está dentro do trigger do NPC
    private int currentClicks = 0; // Contador para acompanhar a progressão do diálogo
    private List<GameObject> textObjects = new(); // Lista de objetos de texto que representam as falas do NPC
    private Transform speechBubbleCanvas; // Referência ao canvas da fala do NPC
    [SerializeField] private TextMeshProUGUI npcName; // Nome do NPC
    private Transform speechBubble; // Referência ao balão de fala do NPC
    private Transform confirmation; // Referência ao painel de confirmação
    [SerializeField] private CombatData combatData; // Dados de combate compartilhados

    /// <summary>
    /// Inicializa as referências e configura os elementos de UI.
    /// </summary>
    private void Start()
    {
        // Inicializa o painel de confirmação e o desativa
        confirmation = transform.Find("Confirmation");
        confirmation.gameObject.SetActive(false);

        // Inicializa o canvas de falas e procura pelos objetos de texto
        speechBubbleCanvas = transform.Find("SpeechBubbleCanvas");
        if (speechBubbleCanvas != null)
        {
            speechBubble = speechBubbleCanvas.Find("SpeechBubble");
            if (speechBubble != null)
            {
                FindAllTextObjects(speechBubble, textObjects);
                if (textObjects.Count > 0)
                {
                    textObjects[currentClicks].SetActive(true); // Ativa o primeiro objeto de texto
                }
            }
        }
    }

    /// <summary>
    /// Verifica as entradas do jogador e exibe a janela de confirmação se necessário.
    /// </summary>
    void Update()
    {
        if (isCharacterInside && Input.GetKeyDown(KeyCode.Space))
        {
            // Garante que currentClicks não ultrapasse os limites da lista de textos
            if (currentClicks < textObjects.Count)
            {
                // Desativa o objeto de texto atual e avança para o próximo
                textObjects[currentClicks].SetActive(false);
                currentClicks = PassToNextText(currentClicks);
            }
        }

        ShowConfirmationWindow();
    }

    /// <summary>
    /// Exibe a janela de confirmação quando todos os textos foram mostrados.
    /// </summary>
    void ShowConfirmationWindow()
    {
        if (currentClicks >= textObjects.Count)
        {
            speechBubbleCanvas.gameObject.SetActive(false);
            if (confirmation != null)
            {
                confirmation.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.K))
                {
                    // Configura os dados de combate com base no NPC e carrega a cena de combate
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
                    SceneController.Instance.LoadScene("level1ApartmentFight");
                }
            }
        }
    }

    /// <summary>
    /// Detecta quando o jogador entra na área de trigger do NPC.
    /// </summary>
    /// <param name="other">Collider do objeto que entrou no trigger.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isCharacterInside = true;
            currentClicks = 0; // Reseta os cliques quando o jogador entra no trigger
            if (speechBubbleCanvas != null && speechBubble != null)
            {
                // Ativa o canvas de falas e o primeiro objeto de texto
                speechBubbleCanvas.gameObject.SetActive(true);
                speechBubble.gameObject.SetActive(true);
                if (textObjects.Count > 0)
                {
                    textObjects[currentClicks].SetActive(true);
                }
            }
        }
    }

    /// <summary>
    /// Detecta quando o jogador sai da área de trigger do NPC.
    /// </summary>
    /// <param name="other">Collider do objeto que saiu do trigger.</param>
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isCharacterInside = false;
            // Desativa todos os objetos de texto e o canvas de falas
            foreach (var textObj in textObjects)
            {
                textObj.SetActive(false);
            }
            speechBubbleCanvas.gameObject.SetActive(false);
            confirmation.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Método recursivo para encontrar todos os objetos de texto dentro de um transform.
    /// </summary>
    /// <param name="parent">Transform pai onde procurar objetos de texto.</param>
    /// <param name="list">Lista onde adicionar os objetos de texto encontrados.</param>
    void FindAllTextObjects(Transform parent, List<GameObject> list)
    {
        foreach (Transform child in parent)
        {
            if (child.name == "Text")
            {
                list.Add(child.gameObject);
            }
            FindAllTextObjects(child, list); // Chamada recursiva
        }
    }

    /// <summary>
    /// Avança para o próximo texto no diálogo.
    /// </summary>
    /// <param name="currentClicks">Número atual de cliques.</param>
    /// <returns>O número atualizado de cliques.</returns>
    int PassToNextText(int currentClicks)
    {
        currentClicks++; // Avança para o próximo clique
        if (currentClicks < textObjects.Count)
        {
            textObjects[currentClicks].SetActive(true); // Ativa o próximo objeto de texto
        }

        return currentClicks;
    }
}
