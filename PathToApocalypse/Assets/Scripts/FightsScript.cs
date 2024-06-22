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

    void Update()
    {
        if (playerTurn)
        {
            // Verifica se o jogador j� se moveu
            if (hasMoved && IsPlayerStopped())
            {
                StartEnemyTurn();
            }
        }
   
    }

    private bool IsPlayerStopped()
    {
        // Verifica se o jogador est� parado
        Vector2 movement = player.GetComponent<NavMeshAgent>().velocity;
        return movement.magnitude == 0;
    }

    public void PlayerMoved()
    {
        // Chamado pelo jogador quando ele termina de se mover
        hasMoved = true;
    }

    private void StartEnemyTurn()
    {
        // Inicia o turno dos inimigos
        playerTurn = false;
        enemyTurn = true;
        StartCoroutine(EnemyTurnRoutine());
    }

    private IEnumerator EnemyTurnRoutine()
    {
       
        yield return new WaitForSeconds(3);
        // Executa o movimento do inimigo atual
        MoveEnemy();
        Invoke("EndEnemyTurn",1f);
    }

    private void MoveEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        int randomIndex = Random.Range(0, enemies.Length);
        GameObject randomEnemy = enemies[randomIndex];

        EnemyMovement enemyScript = randomEnemy.GetComponent<EnemyMovement>();
        enemyScript.MoveEnemy();

    }


    private void EndEnemyTurn()
    {
        // Finaliza o turno dos inimigos
        enemyTurn = false;

        // Reinicia o turno do jogador
        playerTurn = true;
        hasMoved = false;

        StartCoroutine(PlayerTurnRoutine());
       
    }

    private IEnumerator PlayerTurnRoutine()
    {

        yield return new WaitForSeconds(3);
      
        PlayerControllerTurn playerScript = player.GetComponent<PlayerControllerTurn>();
        playerScript.PlayerTurn();
    }

    public bool PlayerTurn()
    {
        return playerTurn;
    }

    public bool EnemyTurn() 
    {
        return enemyTurn;
    }
}

