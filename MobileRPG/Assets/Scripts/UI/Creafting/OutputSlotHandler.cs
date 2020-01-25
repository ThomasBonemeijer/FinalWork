using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OutputSlotHandler : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public Item item;
    public Transform tempDragParent;
    public Transform currentParent;
    public Transform inventoryPannel;
    public Transform craftingPannel;

    void Start() {
        currentParent = transform.parent;
        craftingPannel = transform.parent.parent.parent;
    }
    public void OnDrag(PointerEventData eventData) {
        transform.SetParent(tempDragParent);
        transform.position = Input.mousePosition;
    }
    public void OnEndDrag(PointerEventData eventData) {
        transform.SetParent(currentParent);
        RectTransform invPanel = inventoryPannel as RectTransform;

        if (RectTransformUtility.RectangleContainsScreenPoint(invPanel, Input.mousePosition)) {
            transform.localPosition = Vector3.zero;
            addAndClear();
        } else {
            transform.localPosition = Vector3.zero;
        } 
    }

    void addAndClear() {
        Inventory.instance.Add(item);
        craftingPannel.GetComponent<CraftingHandler>().ClearCraftingSlots();
        transform.GetComponent<Image>().sprite = null;
        transform.GetComponent<Image>().enabled = false;
        item = null;
    }
}
