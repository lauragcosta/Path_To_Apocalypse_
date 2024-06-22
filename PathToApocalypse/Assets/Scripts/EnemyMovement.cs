using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour
{
	[SerializeField] private EnemyData enemyData;

    private int life;
    private int damage;
    private GameObject player;
    private NavMeshAgent agent;
    private Animator animator;


    private bool takeDamage = false;
    private GameObject mainCamera;

    void Start()
	{
		animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
		life = enemyData.Life;
		damage = enemyData.Damage;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

    
    }

	void Update()
	{
		UpdateAnimation();
    }

   private void UpdateAnimation()
    {
        // Obtém a velocidade do NavMeshAgent
        Vector3 navMeshVelocity = agent.velocity;

        // Converte a velocidade do NavMeshAgent para um vetor 2D
        Vector2 movement = new Vector2(navMeshVelocity.x, navMeshVelocity.y);

        // Atualiza os parâmetros da animação
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetBool("IsMoving", movement.magnitude > 0.1f);

        if (movement.magnitude > 1f)
        {
            movement.Normalize();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Verifica se o jogador não é nulo antes de acessar o componente PlayerControllerTurn
            if (player != null)
            {
                PlayerControllerTurn playerScript = player.GetComponent<PlayerControllerTurn>();
				FightsScript mainCameraScript = mainCamera.GetComponent<FightsScript>();
				bool isEnemyTurn = mainCameraScript.EnemyTurn();
                // Verifica se é o turno do jogador e se o dano ainda não foi aplicado
                if (!takeDamage && isEnemyTurn)
                {
                    playerScript.TakeDamage(damage);
                    takeDamage = true;
                    Attack(); // Método para realizar a ação de ataque após aplicar o dano
                    Invoke("SetTakeDamage", 4f);
				}
				else
				{
					MoveAwayFromPlayer();
				}
            }
        }
        if (collision.gameObject.layer.Equals("Wall"))
        {

            MoveAwayFromPlayer();
        }
		if (collision.gameObject.CompareTag("Enemy"))
		{
      
            MoveAwayFromPlayer();
        }

    }

  
    private void SetTakeDamage()
    {
        takeDamage = false;
    }

    private void Attack()
    {
        agent.velocity = Vector2.zero;
        animator.SetTrigger("Attack");
    }

    public void MoveEnemy()
	{
       
            // Calcula a direção até o jogador
            Vector3 direction = player.transform.position - transform.position;
            float distanceToPlayer = direction.magnitude;

            
                float randomScale = Random.Range(enemyData.MinDistance, enemyData.MaxDistance);
                // Calcula o ponto dentro da distância máxima ao longo da direção até o jogador
                Vector3 targetPosition = transform.position + direction.normalized * randomScale;

                // Define o destino do NavMeshAgent para o ponto dentro da distância máxima
                agent.SetDestination(targetPosition);

    }


    public void SetPlayer(GameObject player)
    {
        this.player = player;
    }


	public void TakeDamage(int damage) 
	{ 
		life-=damage;
		if (life <= 0)
		{
			Die();
		}
		agent.velocity = Vector2.zero;
		animator.SetTrigger("Hurt");
	}

	private void Die()
	{
		agent.enabled = false;
		Destroy(gameObject);
	}

    public void MoveAwayFromPlayer()
    {
        
            // Calcula a direção oposta ao jogador
            Vector3 directionAwayFromPlayer = (transform.position - player.transform.position).normalized;

            // Adiciona um valor aleatório para a direção para não ser sempre na mesma direção
            directionAwayFromPlayer += new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0).normalized;

            // Define a nova posição a 1 unidade de distância na direção oposta
            Vector3 newPosition = transform.position + directionAwayFromPlayer;

            // Move o agente para a nova posição
            agent.SetDestination(newPosition);
        
    }

	public int GetLife()
	{
		return life;
	}


}
