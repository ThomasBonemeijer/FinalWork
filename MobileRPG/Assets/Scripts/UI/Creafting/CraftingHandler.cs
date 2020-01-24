using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class CraftingHandler : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public bool itemPickedUp = false;
    public bool canDeleteItems = true;
    public GameObject slot1;
    public GameObject slot2;
    public GameObject outputSlot;
    Transform outputIcon;
    public GameObject gameManager;

    void Start() {
        outputIcon = outputSlot.transform.GetChild(0);
    }

    void Update() {
        if ((slot1.transform.GetChild(0).GetComponent<IconItem>().isFilled == true) || (slot2.transform.GetChild(0).GetComponent<IconItem>().isFilled == true)) {
            canDeleteItems = false;
        }
        else {
            canDeleteItems = true;
        }
        CraftItem(slot1.transform.GetChild(0).GetComponent<IconItem>().item, slot2.transform.GetChild(0).GetComponent<IconItem>().item);
    }

    public void OnDrag(PointerEventData eventData) {

    }
    public void OnEndDrag(PointerEventData eventData) {

    }

    public void CraftItem(Item item1, Item item2) {
        var craftableItems = gameManager.GetComponent<CraftableItems>();
        if (itemPickedUp == false) {
            // if both slots are filled
            if (item1 != null && item2 != null) {
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
            }
            // if second slot is filled
            if (item1 == null && item2 != null) {
            }
        }
    }

    public void ResetCraftingOutput() {
        outputIcon.GetComponent<Image>().enabled = false;
        outputIcon.GetComponent<Image>().sprite = null;
        outputIcon.GetComponent<OutputSlotHandler>().item = null;
    }
}
