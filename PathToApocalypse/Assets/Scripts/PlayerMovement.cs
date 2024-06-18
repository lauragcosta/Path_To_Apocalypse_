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
		// Get input from the user
		movement.x = Input.GetAxisRaw("Horizontal");
		movement.y = Input.GetAxisRaw("Vertical");

		// Update animation parameters
		animator.SetFloat("Horizontal", movement.x);
		animator.SetFloat("Vertical", movement.y);
		animator.SetBool("IsMoving", movement.sqrMagnitude > 0);

		// Move the player
		Vector3 moveDirection = new Vector3(movement.x, movement.y, 0).normalized;
		transform.position += moveDirection * moveSpeed * Time.deltaTime;
	}

	/**
	private Rigidbody2D _rb;
	private bool _upCommand;
	private bool _downCommand;
	private bool _leftCommand;
	private bool _rightCommand;

	void Start()
    {
		_rb = GetComponent<Rigidbody2D>();
	}

    void Update()
    {
		if (Input.GetKeyDown(KeyCode.W))
		{
			_upCommand = true;
		}
		else if (Input.GetKey(KeyCode.S))
		{
			_downCommand = true;
		}
		else if (Input.GetKey(KeyCode.A))
		{
			_leftCommand = true;
		}
		else if (Input.GetKey(KeyCode.D))
		{
			_rightCommand = true;
		}
	}

	private void FixedUpdate()
	{
		if (_leftCommand)
		{
			_rb.velocity = new Vector2(-_runSpeed, _rb.velocity.y);
			_leftCommand = false;
			GetComponent<SpriteRenderer>().flipX = true;
		}
		else if (_rightCommand)
		{
			_rb.velocity = new Vector2(_runSpeed, _rb.velocity.y);
			_rightCommand = false;
			GetComponent<SpriteRenderer>().flipX = false;
		}
	}
	**/
}
