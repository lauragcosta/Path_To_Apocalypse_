using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    
    public Slider healthBar;

    // Start is called before the first frame update
    void Start()
    {
        UpdateHealthBar();

    }


    // Update is called once per frame
    void Update()
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        EnemyMovement enemyScript = gameObject.GetComponent<EnemyMovement>();

        healthBar.value = enemyScript.GetLife();
    }


}
