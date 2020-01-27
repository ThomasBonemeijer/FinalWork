using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class CraftingHandler : MonoBehaviour, IDragHandler, IEndDragHandler
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
        outputIcon = outputSlot.transform.GetChild(0);
    }

    void Update() {
        if (slot1.transform.childCount > 0 && slot2.transform.childCount > 0) {
            if ((slot1.transform.GetChild(0).GetComponent<IconItem>().isFilled == true) || (slot2.transform.GetChild(0).GetComponent<IconItem>().isFilled == true)) {
                canDeleteItems = false;
            }
            else {
                canDeleteItems = true;
            }
            CraftCombinedItem(slot1.transform.GetChild(0).GetComponent<IconItem>().item, slot2.transform.GetChild(0).GetComponent<IconItem>().item);
        }
    }

    public void OnDrag(PointerEventData eventData) {

    }
    public void OnEndDrag(PointerEventData eventData) {

    }

    public void CraftCombinedItem(Item item1, Item item2) {
        var craftableItems = gameManager.GetComponent<CraftableItems>();
        if (itemPickedUp == false) {
            // if both slots are filled
            if (item1 != null && item2 != null) {
                onlyFirstItemIsFilled = false;
                // craft item
                if ((item1.name == "Lantern" || item2.name == "Lantern") && (item1.name == "Knife" || item2.name == "Knife")) {
                    Item theCraftedItem = craftableItems.CraftableItem1.GetComponent<ItemPickup>().item;
                    outputIcon.GetComponent<Image>().enabled = true;
                    outputIcon.GetComponent<Image>().sprite = theCraftedItem.icon;
                    outputIcon.GetComponent<OutputSlotHandler>().item = theCraftedItem;
                }
            }
            // if first slot is filled
            if (item1 != null && item2 == null) {
                onlyFirstItemIsFilled = true;
                if (item1.name == "Wood") {
                    CraftSingleItem(craftableItems.treeSap);
                }
            }
            // if second slot is filled
            if (item1 == null && item2 != null) {
                onlyFirstItemIsFilled = false;
                if (item2.name == "Wood") {
                    CraftSingleItem(craftableItems.treeSap);
                }
            }
        }
    }

    void CraftSingleItem(Item craftItem) {
        outputIcon.GetComponent<Image>().enabled = true;
        outputIcon.GetComponent<Image>().sprite = craftItem.icon;
        outputIcon.GetComponent<OutputSlotHandler>().item = craftItem;
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
            slot1.transform.GetChild(0).GetComponent<IconItem>().currentInventoryItem.GetComponent<IconItem>().RemoveCurrentInvItem();
            slot1.transform.GetChild(0).GetComponent<IconItem>().ClearCraftingSlot();
            slot1.transform.GetChild(0).GetComponent<IconItem>().item = null;
        }

        isDeleting = false;

        if (slot2.transform.GetChild(0).GetComponent<IconItem>().currentInventoryItem != null) {
            slot2.transform.GetChild(0).GetComponent<Image>().sprite = null;
            slot2.transform.GetChild(0).GetComponent<Image>().enabled = false;
            slot2.transform.GetChild(0).GetComponent<IconItem>().currentInventoryItem.GetComponent<IconItem>().RemoveCurrentInvItem();
            slot2.transform.GetChild(0).GetComponent<IconItem>().ClearCraftingSlot();
            slot2.transform.GetChild(0).GetComponent<IconItem>().item = null;
        }
    }
}
