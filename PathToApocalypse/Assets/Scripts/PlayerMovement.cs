using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float moveSpeed = 1f;
	public int damage = 20;
	private Animator animator;
	private Vector2 movement;
	private Rigidbody2D rb;
	// public int health = 100;

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

	/*public void TakeDamage(int damage)
	{
		health -= damage;
		animator.SetTrigger("Hurt");

		if (health <= 0)
		{
			Die();
		}
	}*/
	/*
	private void Die()
	{
		Debug.Log("Player has died.");
		//MUDAR SCENE
	}*/
}
