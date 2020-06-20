using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public int currentIndex;
    public Image icon;
    public Button removeButton;
    public Item item;

    void Start() {
        currentIndex = transform.GetSiblingIndex();
    }

    public void AddItem(Item newItem) {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
        transform.GetChild(0).GetChild(0).GetComponent<IconItem>().item = newItem;
    }

    public void ClearSlot() {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    public void UseItem() {
        if (item == null) {
            return;
        }
        // item.Use(currentIndex);
        if(item.name == "Apple") {
            Debug.Log("Apple!!");
            GameObject.Find("Player").GetComponent<PlayerHandler>().HealPlayer(currentIndex, 25);
        }
    }
}
