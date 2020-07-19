using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;
    void Start() {

    }
    void OnTriggerEnter2D (Collider2D col) {
        if(col.CompareTag("Player")) {
            PickUp();
        }
    }

    void PickUp() {
        Debug.Log("Picking up: " + item.name);
        bool wasPickedUp = Inventory.instance.Add(item);

        if (wasPickedUp) {
            GameObject.Find("Player").GetComponent<PlayerInfoCanvas>().showPickup(GetComponent<SpriteRenderer>().sprite);
            Destroy(gameObject);
            GameObject.Find("Player").GetComponent<PlayerHandler>().SavePlayer();
        }
        
    }
}
