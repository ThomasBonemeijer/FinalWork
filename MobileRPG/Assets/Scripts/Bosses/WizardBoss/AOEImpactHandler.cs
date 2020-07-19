using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEImpactHandler : MonoBehaviour
{
    bool canDealDamage = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.name == "Player") {
            if (canDealDamage == true) {
                col.GetComponent<PlayerHandler>().takeDamage(25);
                Debug.Log("Player has been hit!");
                canDealDamage = false;
            }
        }
    }
}
