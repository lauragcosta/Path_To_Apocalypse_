
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerHealth", menuName = "Game/PlayerHealth")]
public class PlayerHealth : ScriptableObject
{

    [SerializeField] private int hunger;
    [SerializeField] private int thirst;
    [SerializeField] private int health;
    // Start is called before the first frame update
    public int Hunger
    {
        get { return hunger; }
        set { hunger = value; }
    }

    public int Thirst
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
