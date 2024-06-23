using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controla o movimento e animações do jogador.
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 1f; // Velocidade de movimento do jogador
    private Animator animator; // Referência ao componente Animator do jogador
    private Vector2 movement; // Direção do movimento do jogador
    private Rigidbody2D rb; // Referência ao Rigidbody2D do jogador

    void Start()
    {
        animator = GetComponent<Animator>(); // Obtém o componente Animator do jogador
        rb = GetComponent<Rigidbody2D>(); // Obtém o componente Rigidbody2D do jogador
    }

    void Update()
    {
        // Obtém os inputs do eixo horizontal e vertical
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Atualiza os parâmetros do Animator para controlar as animações de movimento
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetBool("IsMoving", movement.sqrMagnitude > 0); // Define se o jogador está se movendo

        // Verifica se o jogador pressionou a tecla de ataque
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Attack"); // Ativa a animação de ataque no Animator
        }
    }

    void FixedUpdate()
    {
        // Move o jogador conforme a direção de movimento atual
        if (animator.GetBool("IsMoving"))
        {
            Vector3 moveDirection = new Vector3(movement.x, movement.y, 0).normalized;
            rb.MovePosition(rb.position + (Vector2)moveDirection * moveSpeed * Time.fixedDeltaTime);
        }
    }


}
