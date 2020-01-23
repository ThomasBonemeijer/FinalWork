using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasHandler : MonoBehaviour
{
    public Canvas MainUI;
    public Canvas BagUI;
    public GameObject cs1;
    public GameObject cs2;

    public void OpenBag () {
        MainUI.enabled = false;
        BagUI.enabled = true;
    }

    public void CloseBag () {
        BagUI.enabled = false;
        MainUI.enabled = true;
        ClearCraftingSlots();
    }

    // Clears the crafting slots when closing tre bag
    void ClearCraftingSlots() {
        cs1.transform.GetChild(0).GetComponent<IconItem>().ClearCraftingSlot();
        cs2.transform.GetChild(0).GetComponent<IconItem>().ClearCraftingSlot();
    }
}
