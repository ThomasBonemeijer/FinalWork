using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IconItem : MonoBehaviour, IDragHandler, IEndDragHandler
{
    
    Item item;
    // public Button removeButton;
    public Transform inventoryPannel;
    public GameObject inventorySlot;
    
    public void OnDrag(PointerEventData eventData) {
        transform.position = Input.mousePosition;
        // removeButton.image.enabled = false;
    }

    public void OnEndDrag(PointerEventData eventData) {
        RectTransform invPanel = inventoryPannel as RectTransform;

        if (!RectTransformUtility.RectangleContainsScreenPoint(invPanel, Input.mousePosition)) {
            Debug.Log("Remove Item");
            transform.localPosition = Vector3.zero;
            // removeButton.image.enabled = true;
            inventorySlot.GetComponent<InventorySlot>().OnRemoveButton();
        } else {
            Debug.Log("Reset Item Position");
            transform.localPosition = Vector3.zero;
            // removeButton.image.enabled = false;
        }
    }
}
