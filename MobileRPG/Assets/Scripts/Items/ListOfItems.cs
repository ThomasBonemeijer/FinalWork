using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListOfItems : MonoBehaviour
{
    public List<Item> allAvailableItems;
    public Item returnItem;

    public void setReturnItem(string itemName) {
        for (int i = 0; i < allAvailableItems.Count; i++) {
            if (allAvailableItems[i].name == itemName) {
                returnItem = allAvailableItems[i];
            }
        }
    }
}
