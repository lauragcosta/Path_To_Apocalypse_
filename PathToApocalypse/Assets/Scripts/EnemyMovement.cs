using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
	public float moveSpeed = .5f;
	public int damage = 10;
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
				yield return new WaitForSeconds(Random.Range(1f, 3f));
			}
			else
			{
				isMoving = true;
				movement = GetRandomDirection();
				yield return new WaitForSeconds(Random.Range(2f, 4f));
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

		if (collision.gameObject.CompareTag("Player"))
		{
			animator.SetTrigger("Attack");
			PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
			if (player != null)
			{
				player.TakeDamage(damage);
			}
		}
	}

	public void TakeDamage(int damage)
	{
		animator.SetTrigger("Hurt");
	}
}
