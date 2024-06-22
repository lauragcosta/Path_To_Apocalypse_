using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour
{
	[SerializeField] private EnemyData enemyData;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private AudioClip audioClip1;

    private int life;
    private int damage;
    private GameObject player;
    private NavMeshAgent agent;
    private Animator animator;
    private AudioSource audioSource;


    private bool takeDamage = false;
    private GameObject mainCamera;

    /// <summary>
    /// M�todo chamado uma vez no in�cio para inicializar componentes e vari�veis necess�rias.
    /// Obt�m componentes como Animator, AudioSource, NavMeshAgent e configura valores iniciais de vida e dano baseados nos dados do inimigo.
    /// </summary>
    void Start()
	{
		animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
		life = enemyData.Life;
		damage = enemyData.Damage;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        

    
    }

    /// <summary>
    /// M�todo chamado a cada quadro para atualizar a anima��o do inimigo.
    /// </summary>
    void Update()
	{
		UpdateAnimation();
    }

    /// <summary>
    /// Atualiza os par�metros da anima��o do inimigo com base na velocidade do NavMeshAgent.
    /// </summary>
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

    /// <summary>
    /// M�todo chamado quando este Collider2D entra em colis�o com outro Collider2D.
    /// Verifica se a colis�o � com o jogador, uma parede ou outro inimigo, e executa as a��es correspondentes.
    /// </summary>
    /// <param name="collision">Informa��es sobre a colis�o.</param>
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
        if (collision.gameObject.CompareTag("Wall"))
        {

            MoveAwayFromPlayer();
        }
		if (collision.gameObject.CompareTag("Enemy"))
		{
      
            MoveAwayFromPlayer();
        }

    }

    /// <summary>
    /// Define a flag 'takeDamage' como falso, permitindo que o inimigo possa atacar novamente.
    /// </summary>
    private void SetTakeDamage()
    {
        takeDamage = false;
    }

    /// <summary>
    /// Realiza a anima��o de ataque do inimigo, para a movimenta��o e reproduz o efeito sonoro do ataque.
    /// </summary>
    private void Attack()
    {
        agent.velocity = Vector2.zero;
        animator.SetTrigger("Attack");
        audioSource.PlayOneShot(audioClip);
    }

    /// <summary>
    /// Move o inimigo em dire��o ao jogador com um destino aleat�rio dentro de uma faixa espec�fica.
    /// </summary>
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

    /// <summary>
    /// Define o jogador como alvo deste inimigo.
    /// </summary>
    /// <param name="player">O GameObject representando o jogador.</param>
    public void SetPlayer(GameObject player)
    {
        this.player = player;
    }

    /// <summary>
    /// Aplica dano ao inimigo e executa a anima��o de dano.
    /// Reproduz um som de efeito de �udio quando o dano � aplicado.
    /// Destroi o inimigo se sua vida chegar a zero ou menos.
    /// </summary>
    /// <param name="damage">Quantidade de dano a ser aplicado.</param>
    public void TakeDamage(int damage) 
	{ 
        audioSource.PlayOneShot(audioClip1);
		life-=damage;
		if (life <= 0)
		{
			Die();
		}
		agent.velocity = Vector2.zero;
		animator.SetTrigger("Hurt");
	}

    /// <summary>
    /// Desativa o NavMeshAgent e destr�i o GameObject do inimigo.
    /// </summary>
    private void Die()
	{
		agent.enabled = false;
		Destroy(gameObject);
	}

    /// <summary>
    /// Move o inimigo para longe do jogador atual, calculando uma dire��o oposta ao jogador com um pequeno desvio aleat�rio.
    /// </summary>
    public void MoveAwayFromPlayer()
    {
        
            // Calcula a dire��o oposta ao jogador
            Vector3 directionAwayFromPlayer = (transform.position - player.transform.position).normalized;

            // Adiciona um valor aleat�rio para a dire��o para n�o ser sempre na mesma dire��o
            directionAwayFromPlayer += new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0).normalized;

            // Define a nova posi��o a 1 unidade de dist�ncia na dire��o oposta
            Vector3 newPosition = transform.position + directionAwayFromPlayer;

        if (newPosition != null)
        {
            // Move o agente para a nova posi��o
            agent.SetDestination(newPosition);
        }
    }

    /// <summary>
    /// Retorna o valor atual da vida do inimigo.
    /// </summary>
    /// <returns>Valor atual da vida do inimigo.</returns>
    public int GetLife()
	{
		return life;
	}


}
