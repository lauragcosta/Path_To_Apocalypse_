using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerControllerTurn : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject weapon;
    [SerializeField] private Vector3 offset;
    [SerializeField] private TextMeshProUGUI probabilityText; // Para TextMeshPro
    [SerializeField] private Button attack1;
    [SerializeField] private Button attack2;
    [SerializeField] private Button attack3;

    private NavMeshAgent agent;
    private Animator animator;
    private float maxMoveDistance = 5f;
    private Vector3 targetPosition;
    private bool playerTurn = true;
    private bool playerTurnRoutine;
    private GameObject instantiatedWeapon;
    [SerializeField]private int life;
    private bool takeDamage = false;
    private int damage = 15;
    private int damageOne = 25;
    private int damageTwo = 35;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        agent.agentTypeID = NavMesh.GetSettingsByIndex(0).agentTypeID;
        if(weapon != null)
        {
           
            Vector3 newPosition = gameObject.transform.position + offset;
            instantiatedWeapon = Instantiate(weapon,newPosition,Quaternion.identity);
            instantiatedWeapon.transform.SetParent(gameObject.transform);

        }
        playerTurnRoutine = true;
        life = playerHealth.Health;

        probabilityText.gameObject.SetActive(false);
        attack1.gameObject.SetActive(false);
        attack2.gameObject.SetActive(false);
        attack3.gameObject.SetActive(false);
        TextMeshProUGUI attackText = attack1.GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI attack1Text = attack2.GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI attack2Text = attack3.GetComponentInChildren<TextMeshProUGUI>();

        attackText.text = "Attack 1(" + damage + "Damage)";
        attack1Text.text = "Attack 2(" + damageOne + "Damage)";
        attack2Text.text = "Attack 3(" + damageTwo + "Damage)";
    }

    void Update()
    {
        playerTurn = mainCamera.GetComponent<Level1ApartmentFight>().PlayerTurn();
        if (playerTurn) {
            // Verifica se houve clique com o botão esquerdo do mouse para mover o personagem
            if (!IsPointerOverUIObject())
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Vector2 raycastPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    RaycastHit2D hit = Physics2D.Raycast(raycastPoint, Vector2.zero);

                    

                    if (hit.collider != null) // Verifica se o Raycast colidiu com algum objeto
                    {
                        if (hit.collider.CompareTag("Enemy")) // Verifica se o objeto colidido tem a tag "Enemy"
                        {
                               
                            // Coloque aqui a lógica que você deseja executar ao clicar em um inimigo
                            EnemyMovement enemyScript = hit.collider.GetComponent<EnemyMovement>();
                            Vector3 enemyPosition = hit.collider.transform.position;
                            float distance = Vector3.Distance(agent.transform.position, hit.collider.transform.position);
                            if (distance <= 5f)
                            {
                                probabilityText.gameObject.SetActive(true);
                                attack1.gameObject.SetActive(true);
                                attack2.gameObject.SetActive(true);
                                attack3.gameObject.SetActive(true);



                                attack1.onClick.AddListener(() => MovePlayerWithAttack(enemyPosition));
                                attack2.onClick.AddListener(() => MovePlayerWithAttack1(enemyPosition));
                                attack3.onClick.AddListener(() => MovePlayerWithAttack2(enemyPosition));

                            }
                            else
                            {
                                probabilityText.gameObject.SetActive(false);
                                attack1.gameObject.SetActive(false);
                                attack2.gameObject.SetActive(false);
                                attack3.gameObject.SetActive(false);
                                MovePlayer();
                            }
                        }

                    }
                    else
                    {
                        probabilityText.gameObject.SetActive(false);
                        attack1.gameObject.SetActive(false);
                        attack2.gameObject.SetActive(false);
                        attack3.gameObject.SetActive(false);
                        MovePlayer();
                    }
                }
            }
    }

        // Obtém o vetor de movimento do NavMeshAgent
        Vector3 navMeshVelocity = agent.velocity;

        // Converte a velocidade do NavMeshAgent para um vetor 2D
        Vector2 movement = new Vector2(navMeshVelocity.x, navMeshVelocity.y);


        if (movement.magnitude > 0.1f) // Se estiver se movendo
        {
            if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y)) // Movimento predominante na horizontal
            {
                if (movement.x > 0.1f)
                {
                    instantiatedWeapon.transform.rotation = Quaternion.Euler(0f, 180f, 0f); // Virar a arma para a direita
                    offset.x = 0.4f;
                    offset.y = 0f;
                }
                else if (movement.x < -0.1f)
                {
                    instantiatedWeapon.transform.rotation = Quaternion.Euler(0f, 0f, 0f); // Virar a arma para a esquerda
                    offset.x = -0.4f;
                    offset.y = 0f;
                }
            }
            else // Movimento predominante na vertical
            {
                if (movement.y > 0.1f)
                {
                    instantiatedWeapon.transform.rotation = Quaternion.Euler(0f, 180f, 0f); // Arma normal para cima
                    offset.y = 0.2f; // Colocar a arma mais alta
                    offset.x = 0f;
                }
                else if (movement.y < -0.1f)
                {
                    instantiatedWeapon.transform.rotation = Quaternion.Euler(0f, 0f, 180f); // Virar a arma para baixo
                    offset.y = -0.4f; // Colocar a arma mais baixa
                    offset.x = 0f;
                }
            }

            Vector3 newPosition = gameObject.transform.position + offset;
            instantiatedWeapon.transform.position = newPosition;
        }
        // Atualiza as animações no Animator
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetBool("IsMoving", movement.magnitude > 0.1f); // Considera que o personagem está se movendo se a magnitude do vetor de movimento for maior que 0.1

        // Normaliza o vetor de movimento para garantir que as animações intermediárias sejam suaves
        if (movement.magnitude > 1f)
        {
            movement.Normalize();
        }
    }

    private bool IsPointerOverUIObject()
    {
        // Verifica se o clique foi em um objeto da UI (se necessário)
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return true;
        }
        return false;
    }

    private void MovePlayerWithAttack(Vector3 targetPosition)
    {
        probabilityText.gameObject.SetActive(false);
        attack1.gameObject.SetActive(false);
        attack2.gameObject.SetActive(false);
        attack3.gameObject.SetActive(false);
        SwordScript swordScript = weapon.GetComponent<SwordScript>();
        swordScript.SetDamage(damage);
        Invoke("DelayPlayerMoved", 1f);
        agent.SetDestination(targetPosition);
        PlayerTurnRoutine();
    }

    private void MovePlayerWithAttack1(Vector3 targetPosition)
    {
        probabilityText.gameObject.SetActive(false);
        attack1.gameObject.SetActive(false);
        attack2.gameObject.SetActive(false);
        attack3.gameObject.SetActive(false);
        SwordScript swordScript = weapon.GetComponent<SwordScript>();
        swordScript.SetDamage(damageOne);
        Invoke("DelayPlayerMoved", 1f);
        agent.SetDestination(targetPosition);
        PlayerTurnRoutine();
    }

    private void MovePlayerWithAttack2(Vector3 targetPosition)
    {
        probabilityText.gameObject.SetActive(false);
        attack1.gameObject.SetActive(false);
        attack2.gameObject.SetActive(false);
        attack3.gameObject.SetActive(false);
        SwordScript swordScript = weapon.GetComponent<SwordScript>();
        swordScript.SetDamage(damageTwo);
        Invoke("DelayPlayerMoved", 1f);
        agent.SetDestination(targetPosition);
        PlayerTurnRoutine();
    }

    private void MovePlayer()
    {
        Vector2 raycastPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 direction = raycastPoint - (Vector2)transform.position;

        // Verifica se a distância até o ponto clicado é maior que a distância máxima permitida
        if (direction.magnitude > maxMoveDistance)
        {
            // Calcula o ponto dentro da distância máxima ao longo da direção até o ponto clicado
            targetPosition = transform.position + direction.normalized * maxMoveDistance;
        }
        else
        {
            // Define o destino do NavMeshAgent para o ponto clicado diretamente se estiver dentro da distância máxima
            targetPosition = raycastPoint;
        }

        Invoke("DelayPlayerMoved", 1f);

        agent.SetDestination(targetPosition);
        PlayerTurnRoutine();
    }

    private void DelayPlayerMoved()
    {
        Level1ApartmentFight level1ApartmentScript = mainCamera.GetComponent<Level1ApartmentFight>();
        level1ApartmentScript.PlayerMoved();
    }

    private IEnumerator PlayerTurnRoutine()
    {

        yield return new WaitForSeconds(0.5f);
        playerTurnRoutine = playerTurn;
    }

    public void PlayerTurn()
    {
        playerTurn = true;
    }

    public bool GetPlayerTurn()
    {
        return playerTurnRoutine;
    }

    public void Attack()
    {
        agent.velocity = Vector2.zero;
        animator.SetTrigger("Attack");
    }

    public void TakeDamage(int damage)
    {

        life -= damage;
        if (life == 0)
        {
            GameOver();
        }
        agent.velocity = Vector2.zero;
        animator.SetTrigger("Hurt");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Verifica se o jogador não é nulo antes de acessar o componente PlayerControllerTurn
            if (weapon== null)
            {
                EnemyMovement enemyScript = GetComponent<EnemyMovement>();
              
                // Verifica se é o turno do jogador e se o dano ainda não foi aplicado
                if (!takeDamage && playerTurnRoutine)
                {
                    enemyScript.TakeDamage(damage);
                    takeDamage = true;
                    Attack(); // Método para realizar a ação de ataque após aplicar o dano
                    Invoke("SetTakeDamage", 4f);
                }
            } else { MovePlayerAway(); }
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            MovePlayerAway();
        }
    }
    private void SetTakeDamage()
    {
        takeDamage = false;
    }


    public void GameOver()
    {

    }

    public void MovePlayerAway()
    {
       
            // Calcula a direção oposta ao jogador
            Vector3 directionAwayFromPlayer = transform.position.normalized;

            // Adiciona um valor aleatório para a direção para não ser sempre na mesma direção
            directionAwayFromPlayer += new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0).normalized;

            // Define a nova posição a 1 unidade de distância na direção oposta
            Vector3 newPosition = transform.position + directionAwayFromPlayer;

            // Move o agente para a nova posição
            agent.SetDestination(newPosition);
        
    }

}
