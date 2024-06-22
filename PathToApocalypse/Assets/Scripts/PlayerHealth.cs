
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerHealth", menuName = "Game/PlayerHealth")]
public class PlayerHealth : ScriptableObject
{

    [SerializeField] private float hunger;
    [SerializeField] private float thirst;
    [SerializeField] private int health;
 
    public float Hunger
    {
        get { return hunger; }
        set { hunger = value; }
    }

    public float Thirst
    {
        get { return thirst; }
        set { thirst = value; }
    }
    public int Health
    {
        get { return health; }
        set { health = value; }
    }

}
