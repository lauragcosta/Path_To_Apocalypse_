using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float moveSpeed = 5f;
	private Animator animator;
	private Vector2 movement;
	private Rigidbody2D rb;

	void Start()
	{
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		movement.x = Input.GetAxisRaw("Horizontal");
		movement.y = Input.GetAxisRaw("Vertical");

		animator.SetFloat("Horizontal", movement.x);
		animator.SetFloat("Vertical", movement.y);
		animator.SetBool("IsMoving", movement.sqrMagnitude > 0);

		//MUDAR TECLA
		if (Input.GetKeyDown(KeyCode.Space))
		{
			animator.SetTrigger("Attack");
		}
	}

	void FixedUpdate()
	{
		if (animator.GetBool("IsMoving"))
		{
			Vector3 moveDirection = new Vector3(movement.x, movement.y, 0).normalized;
			rb.MovePosition(rb.position + (Vector2)moveDirection * moveSpeed * Time.fixedDeltaTime);
		}
	}

	public void TakeDamage()
	{
		animator.SetTrigger("Hurt");
	}

	public void Attack()
	{
		animator.SetTrigger("Attack");
	}
}
