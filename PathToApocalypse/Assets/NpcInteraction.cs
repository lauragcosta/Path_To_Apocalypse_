using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcInteraction : MonoBehaviour
{
    private Collider2D myCollider;
    private bool isCharacterInside = false;

    void Start()
    {
        // Get the Collider2D component attached to this GameObject
        myCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        // Check if the spacebar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Toggle the enabled state of the collider if another character is inside
            if (isCharacterInside)
            {
                myCollider.enabled = !myCollider.enabled;
            }
        }
    }

    // Check when another collider enters the trigger area
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            isCharacterInside = true;
            Debug.Log("heyyy");
        }
    }

    // Check when another collider exits the trigger area
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            isCharacterInside = false;
        }
    }
}
