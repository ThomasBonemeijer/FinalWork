using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class CraftingHandler : MonoBehaviour
{
    public bool itemPickedUp = false;
    public bool canDeleteItems = true;
    public bool isDeleting = false;
    bool onlyFirstItemIsFilled = false;
    public GameObject slot1;
    public GameObject slot2;
    public GameObject outputSlot;
    Transform outputIcon;
    public GameObject gameManager;

    void Start() {
        gameManager = GameObject.Find("UI").GetComponent<UIHandler>().gameManager;
        outputSlot = GameObject.Find("UI").GetComponent<UIHandler>().outputSlot;
        outputIcon = outputSlot.transform.GetChild(0);
    }

    void Update() {
        // check if both crafting slots have a child element
        if (slot1.transform.childCount > 0 && slot2.transform.childCount > 0) {
            Item slot1Item = slot1.transform.GetChild(0).GetComponent<IconItem>().item;
            Item slot2Item = slot2.transform.GetChild(0).GetComponent<IconItem>().item;

            // both crafting slots are filled
            if ((slot1.transform.GetChild(0).GetComponent<IconItem>().isFilled == true) && (slot2.transform.GetChild(0).GetComponent<IconItem>().isFilled == true)) {
                canDeleteItems = false;
                onlyFirstItemIsFilled = false;
                CraftCombinedItem(slot1Item, slot2Item);
                // Debug.Log("Both");
            }
            // only first crafting slot is filled
            else if (slot1.transform.GetChild(0).GetComponent<IconItem>().isFilled == true && slot2.transform.GetChild(0).GetComponent<IconItem>().isFilled == false) {
                canDeleteItems = false;
                onlyFirstItemIsFilled = true;
                CraftSingleItem(slot1Item);
                // Debug.Log("First");
            } 
            // only second crafting slot is filled
            else if (slot2.transform.GetChild(0).GetComponent<IconItem>().isFilled == true && slot1.transform.GetChild(0).GetComponent<IconItem>().isFilled == false) {
                canDeleteItems = false;
                onlyFirstItemIsFilled = false;
                CraftSingleItem(slot2Item);
                // Debug.Log("Second");
            } 
            // no crafting slots are filled
            else {
                canDeleteItems = true;
                onlyFirstItemIsFilled = false;
                // Debug.Log("None");
            }
        }
    }

    public void CraftCombinedItem(Item material1, Item material2) {
        // Debug.Log("Crafting combined item using: " + material1.name + " " + material2.name);

        var craftableItems = gameManager.GetComponent<CraftableItems>();

        if (itemPickedUp == false) {
            if ((material1.name == "Gunpowder" || material2.name == "Gunpowder") && (material1.name == "Metal" || material2.name == "Metal")) {
                Item theCraftedItem = craftableItems.CraftableItem1.GetComponent<ItemPickup>().item;
                outputIcon.GetComponent<Image>().enabled = true;
                outputIcon.GetComponent<Image>().sprite = theCraftedItem.icon;
                outputIcon.GetComponent<OutputSlotHandler>().item = theCraftedItem;
            }
        }
    }

    public void CraftSingleItem(Item material) {
        Debug.Log("Crafting Single item using: " + material.name);

        var craftableItems = gameManager.GetComponent<CraftableItems>();

        if (material.name == "Wood") {
            outputIcon.GetComponent<Image>().enabled = true;
            outputIcon.GetComponent<Image>().sprite = craftableItems.treeSap.icon;
            outputIcon.GetComponent<OutputSlotHandler>().item = craftableItems.treeSap;
        } else {
            // Debug.Log(material.name);
        }
    }

    public void ResetCraftingOutput() {
        outputIcon.GetComponent<Image>().enabled = false;
        outputIcon.GetComponent<Image>().sprite = null;
        outputIcon.GetComponent<OutputSlotHandler>().item = null;
    }

    public void ClearCraftingSlots() {
        if (onlyFirstItemIsFilled == false) {
            isDeleting = true;
        }

        if (slot1.transform.GetChild(0).GetComponent<IconItem>().currentInventoryItem != null) {
            slot1.transform.GetChild(0).GetComponent<Image>().sprite = null;
            slot1.transform.GetChild(0).GetComponent<Image>().enabled = false;
            slot1.transform.GetChild(0).GetComponent<IconItem>().currentInventoryItem.GetComponent<IconItem>().RemoveCurrentInvItemV2();
            slot1.transform.GetChild(0).GetComponent<IconItem>().ClearCraftingSlot();
            slot1.transform.GetChild(0).GetComponent<IconItem>().item = null;
        }

        isDeleting = false;

        if (slot2.transform.GetChild(0).GetComponent<IconItem>().currentInventoryItem != null) {
            slot2.transform.GetChild(0).GetComponent<Image>().sprite = null;
            slot2.transform.GetChild(0).GetComponent<Image>().enabled = false;
            slot2.transform.GetChild(0).GetComponent<IconItem>().currentInventoryItem.GetComponent<IconItem>().RemoveCurrentInvItemV2();
            slot2.transform.GetChild(0).GetComponent<IconItem>().ClearCraftingSlot();
            slot2.transform.GetChild(0).GetComponent<IconItem>().item = null;
        }
    }

    // public void ClearCraftingSlots() {

    //     if (onlyFirstItemIsFilled == false) {
    //         isDeleting = true;
    //     }

    //     else {
    //         if (slot1.transform.GetChild(0).GetComponent<IconItem>().currentInventoryItem != null && slot2.transform.GetChild(0).GetComponent<IconItem>().currentInventoryItem != null) {
    //             int item1Index = slot1.transform.GetChild(0).GetComponent<IconItem>().currentInventoryItem.GetComponent<IconItem>().slotIndex;
    //             int item2Index = slot1.transform.GetChild(0).GetComponent<IconItem>().currentInventoryItem.GetComponent<IconItem>().slotIndex;

    //             slot1.transform.GetChild(0).GetComponent<Image>().sprite = null;
    //             slot2.transform.GetChild(0).GetComponent<Image>().sprite = null;
    //             slot1.transform.GetChild(0).GetComponent<Image>().enabled = false;
    //             slot2.transform.GetChild(0).GetComponent<Image>().enabled = false;

    //             Inventory.instance.RemoveMultipleByIndex(item1Index, item2Index);

    //             slot1.transform.GetChild(0).GetComponent<IconItem>().ClearCraftingSlot();
    //             slot2.transform.GetChild(0).GetComponent<IconItem>().ClearCraftingSlot();
    //             slot1.transform.GetChild(0).GetComponent<IconItem>().item = null;
    //             slot2.transform.GetChild(0).GetComponent<IconItem>().item = null;
    //         }
    //     }
    // }
}
