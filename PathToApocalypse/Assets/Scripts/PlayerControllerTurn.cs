using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class PlayerControllerTurn : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private GameObject mainCamera;
    private NavMeshAgent agent;
    private Animator animator;
    private float maxMoveDistance = 5f;
    private Vector3 targetPosition;
    private bool playerTurn = true;
    

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        agent.agentTypeID = NavMesh.GetSettingsByIndex(0).agentTypeID;
    }

    void Update()
    {
        if (playerTurn) { 
        // Verifica se houve clique com o bot�o esquerdo do mouse para mover o personagem
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 raycastPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Calcula a dire��o at� o ponto clicado
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
            Level1ApartmentFight level1ApartmentScript = mainCamera.GetComponent<Level1ApartmentFight>();
            level1ApartmentScript.PlayerMoved();
            playerTurn = false;
            agent.SetDestination(targetPosition);

        }
    }

        // Obt�m o vetor de movimento do NavMeshAgent
        Vector3 navMeshVelocity = agent.velocity;

        // Converte a velocidade do NavMeshAgent para um vetor 2D
        Vector2 movement = new Vector2(navMeshVelocity.x, navMeshVelocity.y);


        // Atualiza as anima��es no Animator
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetBool("IsMoving", movement.magnitude > 0.1f); // Considera que o personagem est� se movendo se a magnitude do vetor de movimento for maior que 0.1

        // Normaliza o vetor de movimento para garantir que as anima��es intermedi�rias sejam suaves
        if (movement.magnitude > 1f)
        {
            movement.Normalize();
        }
    }

    public void PlayerTurn()
    {
        playerTurn = true;
    }

}
