using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;
    void OnTriggerEnter2D (Collider2D col) {
        PickUp();
    }

    void PickUp() {
        Debug.Log("Picking up: " + item.name);
        bool wasPickedUp = Inventory.instance.Add(item);

        if (wasPickedUp) {
            Destroy(gameObject);
        }
    }
}
