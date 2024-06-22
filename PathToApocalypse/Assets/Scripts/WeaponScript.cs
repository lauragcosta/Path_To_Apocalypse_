using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    [SerializeField] private Item item;
    [SerializeField] private AudioClip audioClip;
    private int damage;
    private int damageOne;
    private int damageTwo;
    private bool takeDamage;
    private int attackDamage;
    private AudioSource audioSource;
    // Start is called before the first frame update

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        damage = item.Damage;
        damageOne = item.DamageOne;
        damageTwo = item.DamageTwo;
    }

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
                audioSource.PlayOneShot(audioClip);
                enemy.TakeDamage(attackDamage);
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

    public void SetAttackDamage(int damage)
    {
        this.attackDamage = damage;
    }

    public int GetDamage()
    {
        return damage;
    }

    public int GetDamageOne()
    {
       return damageOne;
    }

    public int GetDamageTwo()
    {
        return damageTwo;
    }

}
