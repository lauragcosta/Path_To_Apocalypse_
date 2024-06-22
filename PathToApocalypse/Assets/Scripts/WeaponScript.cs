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

    /// <summary>
    /// Método chamado uma vez no início para inicializar componentes e variáveis necessárias.
    /// Configura o componente de áudio, e inicializa os valores de dano com base nos atributos do item.
    /// </summary>
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        damage = item.Damage;
        damageOne = item.DamageOne;
        damageTwo = item.DamageTwo;
    }

    /// <summary>
    /// Método chamado quando este Collider2D entra em colisão com outro Collider2D.
    /// Verifica se a colisão é com um inimigo ou uma parede e executa ações correspondentes.
    /// </summary>
    /// <param name="collision">Informações sobre a colisão.</param>
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


    /// <summary>
    /// Define a flag 'takeDamage' como falso, permitindo que o jogador possa atacar novamente.
    /// </summary>
    private void SetTakeDamage()
    {
        takeDamage = false;
    }

    /// <summary>
    /// Define o valor do dano de ataque do jogador.
    /// </summary>
    /// <param name="damage">Valor do dano de ataque a ser definido.</param>
    public void SetAttackDamage(int damage)
    {
        this.attackDamage = damage;
    }

    /// <summary>
    /// Retorna o valor do primeiro tipo de dano do jogador.
    /// </summary>
    /// <returns>Valor do primeiro tipo de dano.</returns>
    public int GetDamage()
    {
        return damage;
    }

    /// <summary>
    /// Retorna o valor do segundo tipo de dano do jogador.
    /// </summary>
    /// <returns>Valor do segundo tipo de dano.</returns>
    public int GetDamageOne()
    {
       return damageOne;
    }

    /// <summary>
    /// Retorna o valor do terceiro tipo de dano do jogador.
    /// </summary>
    /// <returns>Valor do terceiro tipo de dano.</returns>
    public int GetDamageTwo()
    {
        return damageTwo;
    }

}
