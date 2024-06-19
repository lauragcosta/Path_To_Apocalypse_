using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float moveSpeed = 5f;
	private Animator animator;
	private Vector2 movement;

	void Start()
	{
		animator = GetComponent<Animator>();
	}

	void Update()
	{
		movement.x = Input.GetAxisRaw("Horizontal");
		movement.y = Input.GetAxisRaw("Vertical");

		animator.SetFloat("Horizontal", movement.x);
		animator.SetFloat("Vertical", movement.y);
		animator.SetBool("IsMoving", movement.sqrMagnitude > 0);

		Vector3 moveDirection = new Vector3(movement.x, movement.y, 0).normalized;
		transform.position += moveDirection * moveSpeed * Time.deltaTime;
	}
}
