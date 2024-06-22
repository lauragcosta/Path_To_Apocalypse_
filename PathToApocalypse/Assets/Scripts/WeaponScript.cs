using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    [SerializeField] private int damage;
    private bool takeDamage;
    // Start is called before the first frame update
  

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            PlayerControllerTurn playerScript = GetComponentInParent<PlayerControllerTurn>();
            bool isPlayerTurn = playerScript.GetPlayerTurn();
            // Causar dano ao inimigo
            EnemyMovement enemy = collision.gameObject.GetComponent<EnemyMovement>();
            if (enemy != null && isPlayerTurn && !takeDamage)
            {
                enemy.TakeDamage(damage);
                enemy.MoveAwayFromPlayer();
                takeDamage = true;
                playerScript.Attack();
                Invoke("SetTakeDamage", 4f);

            } 
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            PlayerControllerTurn playerScript = GetComponentInParent<PlayerControllerTurn>();
            playerScript.MovePlayerAway();
        }
        
    }

    private void SetTakeDamage()
    {
        takeDamage = false;
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }
}
