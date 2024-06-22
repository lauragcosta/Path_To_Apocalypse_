using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controla o movimento e anima��es do jogador.
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 1f; // Velocidade de movimento do jogador
    private Animator animator; // Refer�ncia ao componente Animator do jogador
    private Vector2 movement; // Dire��o do movimento do jogador
    private Rigidbody2D rb; // Refer�ncia ao Rigidbody2D do jogador

    void Start()
    {
        animator = GetComponent<Animator>(); // Obt�m o componente Animator do jogador
        rb = GetComponent<Rigidbody2D>(); // Obt�m o componente Rigidbody2D do jogador
    }

    void Update()
    {
        // Obt�m os inputs do eixo horizontal e vertical
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Atualiza os par�metros do Animator para controlar as anima��es de movimento
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetBool("IsMoving", movement.sqrMagnitude > 0); // Define se o jogador est� se movendo

        // Verifica se o jogador pressionou a tecla de ataque
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Attack"); // Ativa a anima��o de ataque no Animator
        }
    }

    void FixedUpdate()
    {
        // Move o jogador conforme a dire��o de movimento atual
        if (animator.GetBool("IsMoving"))
        {
            Vector3 moveDirection = new Vector3(movement.x, movement.y, 0).normalized;
            rb.MovePosition(rb.position + (Vector2)moveDirection * moveSpeed * Time.fixedDeltaTime);
        }
    }


}
