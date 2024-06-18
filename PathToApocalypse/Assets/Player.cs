using UnityEngine;

public class simplemove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f; // Speed at which the character moves
    private Rigidbody2D rb2D;
    private Vector2 movement;

    void Start()
    {
        // Get the Rigidbody2D component attached to this GameObject
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get input from the player
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Create a movement vector based on the input
        movement = new Vector2(moveHorizontal, moveVertical).normalized * moveSpeed;
    }

    void FixedUpdate()
    {
        // Apply the movement to the Rigidbody2D
        rb2D.MovePosition(rb2D.position + movement * Time.fixedDeltaTime);
    }
}
