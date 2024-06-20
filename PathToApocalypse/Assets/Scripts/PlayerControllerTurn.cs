using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerControllerTurn : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    private NavMeshAgent agent;
    private Animator animator;
    // float raycastDistance = 5f;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        agent.agentTypeID = NavMesh.GetSettingsByIndex(0).agentTypeID;
    }

    private void Update()
    {
        // Verifica se houve clique com o botão esquerdo do mouse para mover o personagem
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 raycastPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(raycastPoint, Vector2.zero);

            // Define o destino do NavMeshAgent para o ponto clicado
            agent.SetDestination(raycastPoint);
        }

        // Obtém o vetor de movimento do NavMeshAgent
        Vector3 navMeshVelocity = agent.velocity;

        // Converte a velocidade do NavMeshAgent para um vetor 2D
        Vector2 movement = new Vector2(navMeshVelocity.x, navMeshVelocity.y);

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


}
