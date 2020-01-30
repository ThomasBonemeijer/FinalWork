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

    public GameObject knifeLSlot;
    public GameObject gunLSlot;

    public void OpenBag () {
        Time.timeScale = 0;
        MainUI.enabled = false;
        BagUI.enabled = true;
    }

    public void CloseBag () {
        Time.timeScale = 1;
        BagUI.enabled = false;
        MainUI.enabled = true;
        ClearInvSlots();
    }

    // Clears the crafting slots when closing the bag
    public void ClearInvSlots() {
        cs1.transform.GetChild(0).GetComponent<IconItem>().ClearCraftingSlot();
        cs2.transform.GetChild(0).GetComponent<IconItem>().ClearCraftingSlot();
        BagUI.transform.Find("CraftingInventory").GetComponent<CraftingHandler>().ResetCraftingOutput();
        knifeLSlot.transform.GetChild(0).GetComponent<IconItem>().ClearLoadoutSlot();
        gunLSlot.transform.GetChild(0).GetComponent<IconItem>().ClearLoadoutSlot();
    }
}
