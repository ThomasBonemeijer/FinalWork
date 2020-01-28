using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IconItem : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public int slotIndex;
    public bool isInventory = true;
    bool itemLocked = false;
    public bool isFilled = false;
    public bool canDelete = true;
    bool isBeingDragged = false;
    public Transform inventoryPannel;
    public Transform tempDragParent;
    public Transform currentParent;
    public GameObject gameManager;
    public GameObject craftingPannel;
    public GameObject craftingSlot1;
    public GameObject craftingSlot2;
    public GameObject inventorySlot;
    public GameObject currentCraftingSlot;
    public GameObject currentInventoryItem;
    public Item item;
    public Image icon;
    Color iconColor;
    Color fadedIconColor;

    void Start() {
        currentParent = transform.parent;
        icon = GetComponent<Image>();
        iconColor = icon.color;
        fadedIconColor = iconColor;
        fadedIconColor.a = .5f;
    }

    void Update() {
        if (isInventory == true && isBeingDragged == false) {
            slotIndex = transform.parent.parent.GetComponent<InventorySlot>().currentIndex;
        }
        canDelete = craftingPannel.GetComponent<CraftingHandler>().canDeleteItems;
    }
    
    public void OnDrag(PointerEventData eventData) {
        isBeingDragged = true;
        transform.SetParent(tempDragParent);
        if (isInventory == true) {
            if (itemLocked == false) {
                transform.position = Input.mousePosition;
            } else {
                Debug.Log("Item locked!");
            }
        } else {
            craftingPannel.GetComponent<CraftingHandler>().ResetCraftingOutput();
            craftingPannel.GetComponent<CraftingHandler>().itemPickedUp = true;
            transform.position = Input.mousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData) {
        transform.SetParent(currentParent);
        isBeingDragged = false;

        RectTransform invPanel = inventoryPannel as RectTransform;
        RectTransform craftPanel = craftingPannel.transform as RectTransform;
        RectTransform craftSlot1 = craftingSlot1.transform as RectTransform;
        RectTransform craftSlot2 = craftingSlot2.transform as RectTransform;
        RectTransform currentCraftSlot = null;

        if (currentCraftingSlot != null) {
            currentCraftSlot = currentCraftingSlot.transform as RectTransform;
        }

        // Inventory functionality
        if (isInventory == true) {
            if (itemLocked == false) {
                // If icon is dropped in the inventory pannel
                if (RectTransformUtility.RectangleContainsScreenPoint(invPanel, Input.mousePosition)) {
                    icon.color = iconColor;
                    transform.localPosition = Vector3.zero;
                } 
                // If icon is dropped in the crafting pannel
                else if (RectTransformUtility.RectangleContainsScreenPoint(craftPanel, Input.mousePosition)) {
                    // If icon is dropped in first crafting slot
                    if (RectTransformUtility.RectangleContainsScreenPoint(craftSlot1, Input.mousePosition)) {
                        FillCraftingSlot(craftingSlot1.transform.GetChild(0));
                        return;
                    }
                    // If icon is dropped in second crafting slot
                    else if (RectTransformUtility.RectangleContainsScreenPoint(craftSlot2, Input.mousePosition)) {
                        FillCraftingSlot(craftingSlot2.transform.GetChild(0));
                        return;
                    }
                    icon.color = iconColor;
                    transform.localPosition = Vector3.zero;
                }
                else {
                    if (canDelete == true) {
                        transform.localPosition = Vector3.zero;
                        // Inventory.instance.Remove(item);
                        Inventory.instance.RemoveByIndex(slotIndex);
                        return;
                    }
                    transform.localPosition = Vector3.zero;
                }
            }
        }
        // Craftingscreen functionality
        else {
            craftingPannel.GetComponent<CraftingHandler>().itemPickedUp = false;
            if (!RectTransformUtility.RectangleContainsScreenPoint(currentCraftSlot, Input.mousePosition)) {
                ClearCraftingSlot();
            }
            else {
                transform.localPosition = Vector3.zero;
            }
        }
    }

    void FillCraftingSlot(Transform slotIcon) {
        craftingPannel.GetComponent<CraftingHandler>().ResetCraftingOutput();
        
        if (isInventory == true) {
            var craftSlotScript = slotIcon.GetComponent<IconItem>();

            if (craftSlotScript.isFilled == false) {
                craftSlotScript.isFilled = true;
                craftSlotScript.currentInventoryItem = gameObject;
                craftSlotScript.item = item;

                Image slotImg = slotIcon.GetComponent<Image>();
                slotImg.enabled = true;
                slotImg.sprite = icon.sprite;

                icon.color = fadedIconColor;
                transform.localPosition = Vector3.zero;
                itemLocked = true;
            } else {
                transform.localPosition = Vector3.zero;
            }
        }
    }
    public void ClearCraftingSlot() {
        if (isInventory == false) {
            isFilled = false;
            transform.localPosition = Vector3.zero;
            item = null;
            GetComponent<Image>().sprite = null;
            GetComponent<Image>().enabled = false;
            if (currentInventoryItem != null) {
                currentInventoryItem.GetComponent<Image>().color = iconColor;
                currentInventoryItem.GetComponent<IconItem>().itemLocked = false;
                currentInventoryItem = null;
            }
        }
    }

    public void RemoveCurrentInvItem() {
        // Inventory.instance.Remove(item);
        Inventory.instance.RemoveByIndex(slotIndex);
    }
}
