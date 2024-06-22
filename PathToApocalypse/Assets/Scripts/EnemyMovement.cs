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
        // Obt�m a velocidade do NavMeshAgent
        Vector3 navMeshVelocity = agent.velocity;

        // Converte a velocidade do NavMeshAgent para um vetor 2D
        Vector2 movement = new Vector2(navMeshVelocity.x, navMeshVelocity.y);

        // Atualiza os par�metros da anima��o
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
            // Verifica se o jogador n�o � nulo antes de acessar o componente PlayerControllerTurn
            if (player != null)
            {
                PlayerControllerTurn playerScript = player.GetComponent<PlayerControllerTurn>();
				FightsScript mainCameraScript = mainCamera.GetComponent<FightsScript>();
				bool isEnemyTurn = mainCameraScript.EnemyTurn();
                // Verifica se � o turno do jogador e se o dano ainda n�o foi aplicado
                if (!takeDamage && isEnemyTurn)
                {
                    playerScript.TakeDamage(damage);
                    takeDamage = true;
                    Attack(); // M�todo para realizar a a��o de ataque ap�s aplicar o dano
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
       
            // Calcula a dire��o at� o jogador
            Vector3 direction = player.transform.position - transform.position;
            float distanceToPlayer = direction.magnitude;

            
                float randomScale = Random.Range(enemyData.MinDistance, enemyData.MaxDistance);
                // Calcula o ponto dentro da dist�ncia m�xima ao longo da dire��o at� o jogador
                Vector3 targetPosition = transform.position + direction.normalized * randomScale;

                // Define o destino do NavMeshAgent para o ponto dentro da dist�ncia m�xima
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
        
            // Calcula a dire��o oposta ao jogador
            Vector3 directionAwayFromPlayer = (transform.position - player.transform.position).normalized;

            // Adiciona um valor aleat�rio para a dire��o para n�o ser sempre na mesma dire��o
            directionAwayFromPlayer += new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0).normalized;

            // Define a nova posi��o a 1 unidade de dist�ncia na dire��o oposta
            Vector3 newPosition = transform.position + directionAwayFromPlayer;

            // Move o agente para a nova posi��o
            agent.SetDestination(newPosition);
        
    }

	public int GetLife()
	{
		return life;
	}


}
