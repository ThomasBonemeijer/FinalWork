using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IconItem : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public GameObject UIRoot;
    public int slotIndex;
    public bool isInventory = true;
    bool itemLocked = false;
    public bool isFilled = false;
    public bool canDelete = true;
    public bool canDelete2 = true;
    bool isBeingDragged = false;
    public Transform inventoryPannel;
    public GameObject craftingPannel;
    public GameObject loadoutPannel;
    public Transform tempDragParent;
    Transform currentParent;
    public GameObject gameManager;
    public GameObject bagUI;
    public GameObject craftingSlot1;
    public GameObject craftingSlot2;
    public GameObject inventorySlot;
    public GameObject knifeLoadoutSlot;
    public GameObject gunLoadoutSlot;
    public GameObject currentCraftingSlot;
    public GameObject currentInventoryItem;
    public Item item;
    public Image icon;
    Color iconColor;
    Color fadedIconColor;

    void Start() {
        UIRoot = GameObject.Find("UI");
        gameManager = UIRoot.GetComponent<UIHandler>().gameManager;
        bagUI = UIRoot.GetComponent<UIHandler>().BagUIGO;
        currentParent = transform.parent;
        craftingSlot1 = UIRoot.GetComponent<UIHandler>().craftingSlot1;
        craftingSlot2 = UIRoot.GetComponent<UIHandler>().craftingSlot2;
        knifeLoadoutSlot = UIRoot.GetComponent<UIHandler>().knifeLoadoutSlot;
        gunLoadoutSlot = UIRoot.GetComponent<UIHandler>().gunLoadoutSlot;
        tempDragParent = UIRoot.GetComponent<UIHandler>().tempDragParent;
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
        canDelete2 = loadoutPannel.GetComponent<LoadoutHandler>().canDeleteItems;
        
    }
    
    public void OnDrag(PointerEventData eventData) {
        isBeingDragged = true;
        transform.SetParent(tempDragParent);
        if (isInventory == true) {
            if (itemLocked == false) {
                transform.position = Input.mousePosition;
            } else {
                // Debug.Log("Item locked!");
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

        RectTransform loadPannel = loadoutPannel.transform as RectTransform;
        RectTransform knifeLSlot = knifeLoadoutSlot.transform as RectTransform;
        RectTransform gunLSlot = gunLoadoutSlot.transform as RectTransform;

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
                else if (RectTransformUtility.RectangleContainsScreenPoint(craftPanel, Input.mousePosition) && bagUI.GetComponent<BagUIHandler>().currentInventory == "crafting") {
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
                // If icon is dropped in the loadout pannel
                else if (RectTransformUtility.RectangleContainsScreenPoint(craftPanel, Input.mousePosition) && bagUI.GetComponent<BagUIHandler>().currentInventory == "loadout") {
                    // If icon is dropped in knife-loadout slot
                    if (RectTransformUtility.RectangleContainsScreenPoint(knifeLSlot, Input.mousePosition)) {
                        FillLoadoutSlot(knifeLSlot.transform.GetChild(0));
                        return;
                    }
                    // If icon is dropped in knife-loadout slot
                    else if (RectTransformUtility.RectangleContainsScreenPoint(gunLSlot, Input.mousePosition)) {
                        FillLoadoutSlot(gunLSlot.transform.GetChild(0));
                        return;
                    }
                    icon.color = iconColor;
                    transform.localPosition = Vector3.zero;
                }

                else {
                    if (canDelete == true && canDelete2 == true) {
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
        else if (isInventory == false) {
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

    void FillLoadoutSlot(Transform slotIcon) {
        var loadoutSlotScript = slotIcon.GetComponent<IconItem>();

        if (loadoutSlotScript.isFilled == false) {
            loadoutSlotScript.isFilled = true;
            loadoutSlotScript.currentInventoryItem = gameObject;
            loadoutSlotScript.item = item;

            Image slotImg = slotIcon.GetComponent<Image>();
            slotImg.enabled = true;
            slotImg.sprite = icon.sprite;

            icon.color = fadedIconColor;
            transform.localPosition = Vector3.zero;
            itemLocked = true;
        }
    }

    public void ClearLoadoutSlot() {
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

    public void RemoveCurrentInvItemV2() {
        Inventory.instance.Remove(item);
        // Inventory.instance.RemoveByIndex(slotIndex);
    }
}
