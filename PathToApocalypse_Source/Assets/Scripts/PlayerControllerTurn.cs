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
    [SerializeField] private CombatData combatData;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private AudioClip audioClip;
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
    private int life;
    private float thirst;
    private float hunger;
    private bool takeDamage = false;
    private int noWeaponDamage = 5;
    bool UIActive = false;
    private GameObject weapon = null;
    private EnemyMovement enemyScript;
    private AudioSource audioSource;


    private Vector3 enemyPosition;

    /// <summary>
    /// Configuração inicial do jogador ao iniciar a cena.
    /// </summary>
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        agent.agentTypeID = NavMesh.GetSettingsByIndex(0).agentTypeID;

        weapon = combatData.WeaponInHand;


        if (weapon!=null)
        {

            Vector3 newPosition = gameObject.transform.position + offset;
            instantiatedWeapon = Instantiate(weapon, newPosition, Quaternion.identity);
            instantiatedWeapon.transform.SetParent(gameObject.transform);
        }
        playerTurnRoutine = true;
        life = playerHealth.Health;
        thirst = playerHealth.Thirst;
        hunger = playerHealth.Hunger;

        HideUI();    

    }

    /// <summary>
    /// Atualizações de cada quadro para o comportamento do jogador durante o seu turno.
    /// </summary>
    void Update()
    {
        playerTurn = mainCamera.GetComponent<FightsScript>().PlayerTurn();
        if (playerTurn)
        {
   
            // Verifica se houve clique com o bot�o esquerdo do mouse para mover o personagem
            if (Input.GetMouseButtonDown(0) && !UIActive)
            {
                    Vector2 raycastPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    RaycastHit2D hit = Physics2D.Raycast(raycastPoint, Vector2.zero);

                   
                if (hit.collider != null)
                    {
                        if (hit.collider.CompareTag("Enemy"))
                        {
                           
                            enemyScript = hit.collider.GetComponent<EnemyMovement>();
                             enemyPosition = hit.collider.transform.position;
                            float distance = Vector3.Distance(agent.transform.position, enemyPosition);

                         
                        if (distance <= 5f && weapon != null)
                        {
                            ShowUI(enemyPosition);
                            UIActive = true;
                        }
                        else if(weapon==null){
                            ShowUIWithoutWeapon(enemyPosition);
                            UIActive = true;
                        }

                        }
                    }
                    else
                    {
                        HideUI();
                        MovePlayer();
                    }

            }
            else if(UIActive)
            {
                if(weapon != null) {
                    attack1.onClick.RemoveAllListeners();
                    attack1.onClick.AddListener(() => MovePlayerWithAttack(enemyPosition));
                    attack2.onClick.RemoveAllListeners();
                    attack2.onClick.AddListener(() => MovePlayerWithAttack1(enemyPosition));
                    attack3.onClick.RemoveAllListeners();
                    attack3.onClick.AddListener(() => MovePlayerWithAttack2(enemyPosition));
                }
                else if(weapon == null)
                {
                    attack1.onClick.RemoveAllListeners();
                    attack1.onClick.AddListener(() => MovePlayerWithoutWeapon(enemyPosition));
                    attack2.onClick.RemoveAllListeners();
                    attack2.onClick.AddListener(() => MovePlayerWithoutWeapon(enemyPosition));
                    attack3.onClick.RemoveAllListeners();
                    attack3.onClick.AddListener(() => MovePlayerWithoutWeapon(enemyPosition));
                }

            }

          

        }

        UpdateMovementAnimation();

        if (hunger < 7f && thirst < 7f)
        {
            attack3.gameObject.SetActive(false);
        }
        else if (hunger < 4f && hunger < 4f)
        {
            attack2.gameObject.SetActive(false);
            attack3.gameObject.SetActive(false);
        }

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length <= 0)
        {
            WonCombat();
        }

    }


    /// <summary>
    /// Calcula a probabilidade de uma determinada posição ser escolhida para movimento com base em vários fatores.
    /// </summary>
    /// <param name="position">A posição para a qual calcular a probabilidade.</param>
    /// <returns>A probabilidade calculada como um inteiro.</returns>
    private int GetProbability(Vector3 position)
    {
        int probability = 100;
        int walls = IsWallInRadius();
        probability -= walls * 20;

        float distance = Vector3.Distance(transform.position, position);
        probability -= (int)distance * 10;

        if (thirst < 7f && hunger < 7f)
        {
            probability -= 10;
        }
        else if (thirst < 4f && hunger < 4f)
        {
            probability -= 20;
        }


        return probability;
    }

    /// <summary>
    /// Verifica se há paredes dentro de um raio especificado ao redor do jogador.
    /// </summary>
    /// <returns>O número de paredes encontradas dentro do raio.</returns>
    int IsWallInRadius()
    {
        int walls = 0;
        // Define o ponto central da esfera como a posi��o atual do GameObject
        Vector3 center = transform.position;

        // Obtem todos os colliders dentro do raio especificado
        Collider[] hitColliders = Physics.OverlapSphere(center, maxMoveDistance);

        // Itera por todos os colliders encontrados
        foreach (Collider hitCollider in hitColliders)
        {
            // Verifica se o colisor tem a tag "wall"
            if (hitCollider.CompareTag("Wall"))
            {
                // Se encontrar, retorna true
                return walls++;
            }
        }

        // Se nenhum collider com a tag "wall" for encontrado, retorna false
        return walls;
    }

    /// <summary>
    /// Exibe a interface do usuário (UI) com opções de ataque e probabilidade de acerto.
    /// </summary>
    /// <param name="enemyPosition">A posição do inimigo para o qual a UI está sendo exibida.</param>
    private void ShowUI(Vector3 enemyPosition)
    {
        probabilityText.gameObject.SetActive(true);

        attack1.gameObject.SetActive(true);
        attack2.gameObject.SetActive(true);
        attack3.gameObject.SetActive(true);
        
        
        probabilityText.text = "Probability:" + GetProbability(enemyPosition);

        WeaponScript instantiatedWeaponScript = instantiatedWeapon.GetComponent<WeaponScript>();

        TextMeshProUGUI attackText = attack1.GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI attack1Text = attack2.GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI attack2Text = attack3.GetComponentInChildren<TextMeshProUGUI>();
        if (instantiatedWeapon != null)
        {
            attackText.text = "Attack 1(" + instantiatedWeaponScript.GetDamage() + "Damage)";
            attack1Text.text = "Attack 2(" + instantiatedWeaponScript.GetDamageOne() + "Damage)";
            attack2Text.text = "Attack 3(" + instantiatedWeaponScript.GetDamageTwo() + "Damage)";
        }
        else
        {
            attackText.text = "Attack 1(" + noWeaponDamage + "Damage)";
            attack1Text.text = "Attack 2(" + noWeaponDamage + "Damage)";
            attack2Text.text = "Attack 3(" + noWeaponDamage + "Damage)";
        }

        // Remove todos os listeners anteriores e adiciona novos
        attack1.onClick.RemoveAllListeners();
        attack1.onClick.AddListener(() => MovePlayerWithAttack(enemyPosition));
        attack2.onClick.RemoveAllListeners();
        attack2.onClick.AddListener(() => MovePlayerWithAttack1(enemyPosition));
        attack3.onClick.RemoveAllListeners();
        attack3.onClick.AddListener(() => MovePlayerWithAttack2(enemyPosition));
    }


    /// <summary>
    /// Exibe a interface do usuário (UI) sem arma equipada, com opções de ataque e probabilidade de acerto.
    /// </summary>
    /// <param name="enemyPosition">A posição do inimigo para o qual a UI está sendo exibida.</param>
    private void ShowUIWithoutWeapon(Vector3 enemyPosition)
    {
        probabilityText.gameObject.SetActive(true);

        attack1.gameObject.SetActive(true);
        attack2.gameObject.SetActive(true);
        attack3.gameObject.SetActive(true);


        probabilityText.text = "Probability:" + GetProbability(enemyPosition);

        // Remove todos os listeners anteriores e adiciona novos
        attack1.onClick.RemoveAllListeners();
        attack1.onClick.AddListener(() => MovePlayerWithoutWeapon(enemyPosition));
        attack2.onClick.RemoveAllListeners();
        attack2.onClick.AddListener(() => MovePlayerWithoutWeapon(enemyPosition));
        attack3.onClick.RemoveAllListeners();
        attack3.onClick.AddListener(() => MovePlayerWithoutWeapon(enemyPosition));
    }

    /// <summary>
    /// Esconde a interface do usuário (UI) de opções de ataque e probabilidade de acerto.
    /// </summary>
    private void HideUI()
    {
        probabilityText.gameObject.SetActive(false);
        attack1.gameObject.SetActive(false);
        attack2.gameObject.SetActive(false);
        attack3.gameObject.SetActive(false);
    }


    /// <summary>
    /// Atualiza a animação de movimento do jogador, ajusta a posição da arma (se equipada) e configura parâmetros de animação com base na velocidade do NavMeshAgent.
    /// </summary>
    private void UpdateMovementAnimation()
    {
        Vector3 navMeshVelocity = agent.velocity;
        Vector2 movement = new Vector2(navMeshVelocity.x, navMeshVelocity.y);
        if (weapon != null)
        {

            if (movement.magnitude > 0.1f)
            {
                if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
                {
                    if (movement.x > 0.1f)
                    {
                        instantiatedWeapon.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                        offset.x = 0.4f;
                        offset.y = 0f;
                    }
                    else if (movement.x < -0.1f)
                    {
                        instantiatedWeapon.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                        offset.x = -0.4f;
                        offset.y = 0f;
                    }
                }
                else
                {
                    if (movement.y > 0.1f)
                    {
                        instantiatedWeapon.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                        offset.y = 0.2f;
                        offset.x = 0f;
                    }
                    else if (movement.y < -0.1f)
                    {
                        instantiatedWeapon.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
                        offset.y = -0.4f;
                        offset.x = 0f;
                    }
                }

                Vector3 newPosition = gameObject.transform.position + offset;
                instantiatedWeapon.transform.position = newPosition;
            }
        }

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetBool("IsMoving", movement.magnitude > 0.1f);

        if (movement.magnitude > 1f)
        {
            movement.Normalize();
        }
    }

    /// <summary>
    /// Move o jogador para uma posição específica sem uma arma equipada.
    /// </summary>
    /// <param name="targetPosition">A posição para onde o jogador deve se mover.</param>
    private void MovePlayerWithoutWeapon(Vector3 targetPosition)
    {
        HideUI();
        thirst -= 0.1f;
        hunger -= 0.1f;
        if (thirst <= 0 || hunger <= 0)
        {
            GameOver();
        }

        UIActive = false;

        Invoke("DelayEnemyHit", 1f);
        Invoke("DelayPlayerMoved", 1f);
        Invoke("MovePlayerAway", 2f);
        agent.SetDestination(targetPosition);
        PlayerTurnRoutine();
    }

    /// <summary>
    /// Adia o momento em que o inimigo sofrerá dano sem arma.
    /// </summary>
    private void DelayEnemyHit()
    {
        enemyScript.TakeDamage(noWeaponDamage);
    }


    /// <summary>
    /// Move o jogador para uma posição específica e aplica o primeiro ataque com arma equipada.
    /// </summary>
    /// <param name="targetPosition">A posição para onde o jogador deve se mover.</param>
    private void MovePlayerWithAttack(Vector3 targetPosition)
    {
        HideUI();
        thirst -= 0.1f;
        hunger -= 0.1f;
        if (thirst <= 0 || hunger <= 0)
        {
            GameOver();
        }

        UIActive = false;
        WeaponScript instantiatedWeaponScript = instantiatedWeapon.GetComponent<WeaponScript>();
        instantiatedWeaponScript.SetAttackDamage(instantiatedWeaponScript.GetDamage());
        Invoke("DelayPlayerMoved", 1f);
        agent.SetDestination(targetPosition);
        PlayerTurnRoutine();
    }

    /// <summary>
    /// Move o jogador para uma posição específica e aplica o segundo ataque com arma equipada.
    /// </summary>
    /// <param name="targetPosition">A posição para onde o jogador deve se mover.</param>
    private void MovePlayerWithAttack1(Vector3 targetPosition)
    {
        HideUI();
        UIActive = false;
        thirst -= 0.15f;
        hunger -= 0.15f;
        if (thirst <= 0 || hunger <= 0)
        {
            GameOver();
        }

        WeaponScript instantiatedWeaponScript = instantiatedWeapon.GetComponent<WeaponScript>();
        instantiatedWeaponScript.SetAttackDamage(instantiatedWeaponScript.GetDamageOne());
        Invoke("DelayPlayerMoved", 1f);
        agent.SetDestination(targetPosition);
        PlayerTurnRoutine();
    }

    /// <summary>
    /// Move o jogador para uma posição específica e aplica o terceiro ataque com arma equipada.
    /// </summary>
    /// <param name="targetPosition">A posição para onde o jogador deve se mover.</param>
    private void MovePlayerWithAttack2(Vector3 targetPosition)
    {
        HideUI();
        UIActive = false;
        thirst -= 0.2f;
        hunger -= 0.2f;

        if (thirst <= 0 || hunger <= 0)
        {
            GameOver();
        }

        WeaponScript instantiatedWeaponScript = instantiatedWeapon.GetComponent<WeaponScript>();
        instantiatedWeaponScript.SetAttackDamage(instantiatedWeaponScript.GetDamageTwo());
        Invoke("DelayPlayerMoved", 1f);
        agent.SetDestination(targetPosition);
        PlayerTurnRoutine();
    }

    /// <summary>
    /// Move o jogador para uma posição específica baseada no clique do mouse.
    /// </summary>
    private void MovePlayer()
    {
        thirst -= 0.1f;
        hunger -= 0.1f;
        Vector2 raycastPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 direction = raycastPoint - (Vector2)transform.position;

        // Verifica se a dist�ncia at� o ponto clicado � maior que a dist�ncia m�xima permitida
        if (direction.magnitude > maxMoveDistance)
        {
            // Calcula o ponto dentro da dist�ncia m�xima ao longo da dire��o at� o ponto clicado
            targetPosition = transform.position + direction.normalized * maxMoveDistance;
        }
        else
        {
            // Define o destino do NavMeshAgent para o ponto clicado diretamente se estiver dentro da dist�ncia m�xima
            targetPosition = raycastPoint;
        }

        if (thirst <= 0 || hunger <=0)
        {
            GameOver();
        }

        Invoke("DelayPlayerMoved", 1f);

        agent.SetDestination(targetPosition);
        PlayerTurnRoutine();
    }

    /// <summary>
    /// Invoca o método que indica que o jogador se moveu após um atraso.
    /// </summary>
    private void DelayPlayerMoved()
    {
        FightsScript level1ApartmentScript = mainCamera.GetComponent<FightsScript>();
        level1ApartmentScript.PlayerMoved();
    }

    /// <summary>
    /// Rotina do turno do jogador. Define o estado do turno do jogador após um pequeno atraso.
    /// </summary>
    private IEnumerator PlayerTurnRoutine()
    {

        yield return new WaitForSeconds(0.5f);
        playerTurnRoutine = playerTurn;
    }

    /// <summary>
    /// Define o turno do jogador como ativo.
    /// </summary>
    public void PlayerTurn()
    {
        playerTurn = true;
    }

    /// <summary>
    /// Obtém o estado atual do turno do jogador.
    /// </summary>
    /// <returns>O estado atual do turno do jogador.</returns>
    public bool GetPlayerTurn()
    {
        return playerTurnRoutine;
    }

    /// <summary>
    /// Realiza a ação de ataque do jogador.
    /// </summary>
    public void Attack()
    {
        agent.velocity = Vector2.zero;
        animator.SetTrigger("Attack");
    }

    /// <summary>
    /// Aplica dano ao jogador.
    /// </summary>
    /// <param name="damage">A quantidade de dano a ser aplicada.</param>
    public void TakeDamage(int damage)
    {
        audioSource.PlayOneShot(audioClip);
        life -= damage;
        if (life <= 0)
        {
            GameOver();
        }
        agent.velocity = Vector2.zero;
        animator.SetTrigger("Hurt");
    }

    /// <summary>
    /// Método chamado quando ocorre uma colisão 2D.
    /// </summary>
    /// <param name="collision">A colisão que ocorreu.</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Verifica se o jogador n�o � nulo antes de acessar o componente PlayerControllerTurn
            if (weapon== null)
            {
                 GameObject enemy = collision.gameObject;
                 enemyScript = enemy.GetComponent<EnemyMovement>();
              
                // Verifica se � o turno do jogador e se o dano ainda n�o foi aplicado
                if (!takeDamage && playerTurnRoutine)
                {
                    enemyScript.TakeDamage(noWeaponDamage);
                    takeDamage = true;
                    Attack(); // M�todo para realizar a a��o de ataque ap�s aplicar o dano
                    Invoke("SetTakeDamage", 4f);
                }
            } else { MovePlayerAway(); }
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            MovePlayerAway();
        }
    }

    /// <summary>
    /// Reseta a variável de controle de dano após um atraso.
    /// </summary>
    private void SetTakeDamage()
    {
        takeDamage = false;
    }

    /// <summary>
    /// Carrega a cena de jogo principal quando o jogo termina.
    /// </summary>
    public void GameOver()
    {
        SceneController.Instance.LoadScene("Map1");
        combatData.IsWonCombat = false;
    }

    /// <summary>
    /// Carrega a cena de jogo principal quando o jogador vence o combate.
    /// </summary>
    public void WonCombat()
    {
        SceneController.Instance.LoadScene("Map1");
        combatData.IsWonCombat = true;

    }

    /// <summary>
    /// Move o jogador para longe do jogador atual, adicionando uma pequena direção aleatória.
    /// </summary>
    public void MovePlayerAway()
    {
       
            // Calcula a dire��o oposta ao jogador
            Vector3 directionAwayFromPlayer = transform.position.normalized;

            // Adiciona um valor aleat�rio para a dire��o para n�o ser sempre na mesma dire��o
            directionAwayFromPlayer += new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0).normalized;

            // Define a nova posi��o a 1 unidade de dist�ncia na dire��o oposta
            Vector3 newPosition = transform.position + directionAwayFromPlayer;

            // Move o agente para a nova posi��o
            agent.SetDestination(newPosition);
        
    }

    /// <summary>
    /// Retorna a vida atual do jogador.
    /// </summary>
    /// <returns>A vida atual do jogador.</returns>
    public int GetLife()
    {
        return life;
    }


    /// <summary>
    /// Retorna o nível de sede atual do jogador.
    /// </summary>
    /// <returns>O nível de sede atual do jogador.</returns>
    public float GetThirst()
    {
        return thirst;
    }

    /// <summary>
    /// Retorna o nível de fome atual do jogador.
    /// </summary>
    /// <returns>O nível de fome atual do jogador.</returns>
    public float GetHunger()
    {
        return hunger;
    }
}