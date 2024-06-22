using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using Unity.VisualScripting;


public class FightsScript : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject enemyPrefab3;
    [SerializeField] private GameObject enemyPrefab1;
    [SerializeField] private GameObject enemyPrefab2;
    [SerializeField] private GameObject player;
    [SerializeField] private CombatData combatData;
    private bool enemyTurn = false;
    private bool playerTurn = true;
    private bool hasMoved = false;

    /// <summary>
    /// Inicializa o controlador do jogo ao iniciar a cena.
    /// Define o comportamento de spawn de inimigos com base na dificuldade
    /// configurada no ScriptableObject 'combatData'.
    /// </summary>
    /// <remarks>
    /// O método determina o número de inimigos a serem spawnados aleatoriamente 
    /// entre 3 e 9. Se a dificuldade estiver definida, spawna diferentes tipos 
    /// de inimigos de acordo com o nível de dificuldade. Caso contrário, spawna
    /// inimigos padrão.
    /// </remarks>
    void Start()
    {
        Difficulty? difficulty = combatData.Difficulty.Value;
        int randomValue = Random.Range(3, 9);
        if (difficulty != null) {


            switch (difficulty.Value)
            {
                case Difficulty.Easy:


                    for (int i = 0; i < randomValue; i++)
                    {
                        SpawnDifficultEnemy(enemyPrefab1);
                    }
                    break;
                case Difficulty.Medium:

                    for (int i = 0; i < randomValue; i++)
                    {
                        SpawnDifficultEnemy(enemyPrefab2);
                    }
                    break;
                case Difficulty.Hard:

                    for (int i = 0; i < randomValue; i++)
                    {
                        SpawnDifficultEnemy(enemyPrefab3);
                    }
                    break;
                default:
                    break;
            }
            
        }
        else if(difficulty == null){
            for (int i = 0; i < randomValue; i++)
            {
                SpawnEnemy();
            }
        }
      
    }

    /// <summary>
    /// Spawna um inimigo em uma posição aleatória dentro de uma área definida,
    /// e configura o comportamento de movimento do inimigo para seguir o jogador.
    /// </summary>
    private void SpawnEnemy()
    {
        float minX = -5f;
        float maxX = 5f;
        float minY = -4f;
        float maxY = 4f;

        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        Vector3 spawnPosition = new Vector3(randomX, randomY, 0);

        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        EnemyMovement enemyScript = newEnemy.GetComponent<EnemyMovement>();

        enemyScript.SetPlayer(player);
        
    }

    /// <summary>
    /// Spawna um inimigo específico em uma posição aleatória dentro de uma área definida,
    /// e configura o comportamento de movimento do inimigo para seguir o jogador.
    /// </summary>
    /// <param name="enemy">Prefab do inimigo a ser spawnado.</param>
    private void SpawnDifficultEnemy(GameObject enemy)
    {
        float minX = -5f;
        float maxX = 5f;
        float minY = -4f;
        float maxY = 4f;

        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        Vector3 spawnPosition = new Vector3(randomX, randomY, 0);

        GameObject newEnemy = Instantiate(enemy, spawnPosition, Quaternion.identity);
        EnemyMovement enemyScript = newEnemy.GetComponent<EnemyMovement>();

        enemyScript.SetPlayer(player);

    }

    /// <summary>
    /// Método chamado a cada quadro para atualizar o estado do jogo quando for a vez do jogador.
    /// Verifica se o jogador já se moveu e se parou de se mover para iniciar o turno dos inimigos.
    /// </summary>
    void Update()
    {
        if (playerTurn)
        {
            // Verifica se o jogador já se moveu
            if (hasMoved && IsPlayerStopped())
            {
                StartEnemyTurn();
            }
        }
   
    }

    /// <summary>
    /// Verifica se o jogador está parado, baseado na magnitude da velocidade do componente NavMeshAgent.
    /// </summary>
    /// <returns>Verdadeiro se o jogador estiver completamente parado; falso caso contrário.</returns>
    private bool IsPlayerStopped()
    {
        // Verifica se o jogador está parado
        Vector2 movement = player.GetComponent<NavMeshAgent>().velocity;
        return movement.magnitude == 0;
    }

    /// <summary>
    /// Método chamado pelo jogador para indicar que ele terminou de se mover.
    /// Define a flag 'hasMoved' como verdadeira para indicar que o jogador se moveu.
    /// </summary>
    public void PlayerMoved()
    {
        // Chamado pelo jogador quando ele termina de se mover
        hasMoved = true;
    }

    /// <summary>
    /// Inicia o turno dos inimigos, desativando o turno do jogador e ativando o turno dos inimigos.
    /// Inicia uma rotina assíncrona para controlar o turno dos inimigos.
    /// </summary>
    private void StartEnemyTurn()
    {
        // Inicia o turno dos inimigos
        playerTurn = false;
        enemyTurn = true;
        StartCoroutine(EnemyTurnRoutine());
    }

    /// <summary>
    /// Rotina assíncrona que controla o turno dos inimigos.
    /// Aguarda um tempo determinado e então executa o movimento do inimigo atual.
    /// </summary>
    /// <returns>Uma rotina que pode ser pausada e continuada.</returns>
    private IEnumerator EnemyTurnRoutine()
    {
       
        yield return new WaitForSeconds(3);
        // Executa o movimento do inimigo atual
        MoveEnemy();
        Invoke("EndEnemyTurn",1f);
    }

    /// <summary>
    /// Move um inimigo aleatório entre todos os inimigos ativos na cena.
    /// </summary>
    private void MoveEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        int randomIndex = Random.Range(0, enemies.Length);
        GameObject randomEnemy = enemies[randomIndex];

        EnemyMovement enemyScript = randomEnemy.GetComponent<EnemyMovement>();
        enemyScript.MoveEnemy();

    }


    /// <summary>
    /// Finaliza o turno dos inimigos, reiniciando o turno do jogador e iniciando uma rotina para controlar o turno do jogador.
    /// </summary>
    private void EndEnemyTurn()
    {
        // Finaliza o turno dos inimigos
        enemyTurn = false;

        // Reinicia o turno do jogador
        playerTurn = true;
        hasMoved = false;

        StartCoroutine(PlayerTurnRoutine());
       
    }

    /// <summary>
    /// Rotina assíncrona que controla o turno do jogador.
    /// Aguarda um tempo determinado e então chama o método para iniciar o turno do jogador.
    /// </summary>
    /// <returns>Uma rotina que pode ser pausada e continuada.</returns>
    private IEnumerator PlayerTurnRoutine()
    {

        yield return new WaitForSeconds(3);
      
        PlayerControllerTurn playerScript = player.GetComponent<PlayerControllerTurn>();
        playerScript.PlayerTurn();
    }

    /// <summary>
    /// Retorna o estado atual do turno do jogador.
    /// </summary>
    /// <returns>Verdadeiro se é o turno do jogador; falso caso contrário.</returns>
    public bool PlayerTurn()
    {
        return playerTurn;
    }

    /// <summary>
    /// Retorna o estado atual do turno dos inimigos.
    /// </summary>
    /// <returns>Verdadeiro se é o turno dos inimigos; falso caso contrário.</returns>
    public bool EnemyTurn() 
    {
        return enemyTurn;
    }
}

