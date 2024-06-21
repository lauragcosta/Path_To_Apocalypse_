using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "Game/EnemyData")]
public class EnemyData : ScriptableObject 
{
    [SerializeField] private int life;
    [SerializeField] private int damage;
    // Start is called before the first frame update
    public int Life
    {
        get { return life; }
        set { life = value; }
    }

    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }
}
