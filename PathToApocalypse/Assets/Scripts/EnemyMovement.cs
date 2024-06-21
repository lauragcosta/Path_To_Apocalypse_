using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
	[SerializeField] private EnemyData enemyData;

    private int life;
    private int damage;
    private GameObject player;
    private NavMeshAgent agent;
    private Animator animator;
	private float maxDistance = 3f;
	//private bool isMoving = false;
	//private bool isAttacking = false;  // Flag to indicate if the enemy is attacking

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
				Level1ApartmentFight mainCameraScript = mainCamera.GetComponent<Level1ApartmentFight>();
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

            
                float randomScale = Random.Range(1.2f, maxDistance);
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
		if (life == 0)
		{
			Die();
		}
		agent.velocity = Vector2.zero;
		animator.SetTrigger("Hurt");
	}

	public void Die()
	{
		Destroy(gameObject);
	}

    public void MoveAwayFromPlayer()
    {
        if (player != null)
        {
            // Calcula a direção oposta ao jogador
            Vector3 directionAwayFromPlayer = (transform.position - player.transform.position).normalized;

            // Adiciona um valor aleatório para a direção para não ser sempre na mesma direção
            directionAwayFromPlayer += new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f), 0).normalized;

            // Define a nova posição a 1 unidade de distância na direção oposta
            Vector3 newPosition = transform.position + directionAwayFromPlayer;

            // Move o agente para a nova posição
            agent.SetDestination(newPosition);
        }
    }


    /*
	IEnumerator WanderRoutine()
	{
		while (true)
		{
			if (!isAttacking) // Only wander if not attacking
			{
				if (Random.Range(0, 2) == 0)
				{
					// Idle
					isMoving = false;
					movement = Vector2.zero;
					yield return new WaitForSeconds(Random.Range(1f, 3f));
				}
				else
				{
					// Move
					isMoving = true;
					movement = GetRandomDirection();
					yield return new WaitForSeconds(Random.Range(2f, 4f));
				}
			}
			yield return null; // Ensure the loop continues
		}
	}

	void FixedUpdate()
	{
		if (isMoving && !isAttacking) // Only move if not attacking
		{
			rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
		}
		else if (isAttacking)
		{
			rb.velocity = Vector2.zero; // Ensure velocity is zero during attack
		}
	}

	Vector2 GetRandomDirection()
	{
		int direction = Random.Range(0, 4);
		switch (direction)
		{
			case 0: return Vector2.up;
			case 1: return Vector2.down;
			case 2: return Vector2.left;
			case 3: return Vector2.right;
		}
		return Vector2.zero;
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Wall"))
		{
			// Change direction upon collision
			movement = GetRandomDirection();
		}

		if (collision.gameObject.CompareTag("Player"))
		{
			if (!isAttacking) // Only attack if not already attacking
			{
				StartCoroutine(AttackPlayer(collision.gameObject));
			}
		}
	}

	IEnumerator AttackPlayer(GameObject player)
	{
		isAttacking = true;
		isMoving = false; // Stop moving
		rb.velocity = Vector2.zero; // Stop any momentum

		// Trigger attack animation
		animator.SetTrigger("Attack");

		// Wait for the duration of the attack animation (adjust time as needed)
		yield return new WaitForSeconds(0.5f);

		// Deal damage to the player
		PlayerMovement playerController = player.GetComponent<PlayerMovement>();
		if (playerController != null)
		{
			playerController.TakeDamage(damage);
		}

		// Wait for a short delay before resuming movement (if needed)
		yield return new WaitForSeconds(0.5f);

		isAttacking = false;
	}

	// Example method to be called when the enemy takes damage
	public void TakeDamage(int damage)
	{
		animator.SetTrigger("Hurt");
		// Handle enemy health reduction and potential death here
	}
	*/
}
