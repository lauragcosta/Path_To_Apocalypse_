using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<GameObject> inventorySlots = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject g in gameObject.GetComponentsInChildren<GameObject>())
        {
            inventorySlots.Add(g); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
