using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
	public float moveSpeed = 0.5f;
	public int damage = 10;
	private Animator animator;
	private Vector2 movement;
	private Rigidbody2D rb;
	private bool isMoving = false;
	private bool isAttacking = false;  // Flag to indicate if the enemy is attacking

	void Start()
	{
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
		StartCoroutine(WanderRoutine());
	}

	void Update()
	{
		// Update animation parameters
		animator.SetFloat("Horizontal", movement.x);
		animator.SetFloat("Vertical", movement.y);
		animator.SetBool("IsMoving", isMoving);
	}

	IEnumerator WanderRoutine()
	{
		while (true)
		{
			if (!isAttacking) // Only wander if not attacking
			{
				if (Random.Range(0, 2) == 0)
				{
					// Idle
					isMoving = false;
					movement = Vector2.zero;
					yield return new WaitForSeconds(Random.Range(1f, 3f));
				}
				else
				{
					// Move
					isMoving = true;
					movement = GetRandomDirection();
					yield return new WaitForSeconds(Random.Range(2f, 4f));
				}
			}
			yield return null; // Ensure the loop continues
		}
	}

	void FixedUpdate()
	{
		if (isMoving && !isAttacking) // Only move if not attacking
		{
			rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
		}
		else if (isAttacking)
		{
			rb.velocity = Vector2.zero; // Ensure velocity is zero during attack
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
			// Change direction upon collision
			movement = GetRandomDirection();
		}

		if (collision.gameObject.CompareTag("Player"))
		{
			if (!isAttacking) // Only attack if not already attacking
			{
				StartCoroutine(AttackPlayer(collision.gameObject));
			}
		}
	}

	IEnumerator AttackPlayer(GameObject player)
	{
		isAttacking = true;
		isMoving = false; // Stop moving
		rb.velocity = Vector2.zero; // Stop any momentum

		// Trigger attack animation
		animator.SetTrigger("Attack");

		// Wait for the duration of the attack animation (adjust time as needed)
		yield return new WaitForSeconds(0.5f);

		// Deal damage to the player
		PlayerMovement playerController = player.GetComponent<PlayerMovement>();
		if (playerController != null)
		{
			playerController.TakeDamage(damage);
		}

		// Wait for a short delay before resuming movement (if needed)
		yield return new WaitForSeconds(0.5f);

		isAttacking = false;
	}

	// Example method to be called when the enemy takes damage
	public void TakeDamage(int damage)
	{
		animator.SetTrigger("Hurt");
		// Handle enemy health reduction and potential death here
	}
}
