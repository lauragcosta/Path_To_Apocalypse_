using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
	public float moveSpeed = 2f;
	public float idleTime = 2f;
	public float wanderTime = 3f;
	private Animator animator;
	private Vector2 movement;
	private Rigidbody2D rb;
	private bool isMoving = false;

	void Start()
	{
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
		StartCoroutine(WanderRoutine());
	}

	void Update()
	{
		animator.SetFloat("Horizontal", movement.x);
		animator.SetFloat("Vertical", movement.y);
		animator.SetBool("IsMoving", isMoving);
	}

	IEnumerator WanderRoutine()
	{
		while (true)
		{
			if (Random.Range(0, 2) == 0)
			{
				isMoving = false;
				movement = Vector2.zero;
				yield return new WaitForSeconds(idleTime);
			}
			else
			{
				isMoving = true;
				movement = GetRandomDirection();
				yield return new WaitForSeconds(wanderTime);
			}
		}
	}

	void FixedUpdate()
	{
		if (isMoving)
		{
			rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
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
			movement = GetRandomDirection();
		}
	}
}