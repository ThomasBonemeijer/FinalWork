using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OutputSlotHandler : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public Item item;
    public Transform inventoryPannel;
    public void OnDrag(PointerEventData eventData) {
        transform.position = Input.mousePosition;
    }
    public void OnEndDrag(PointerEventData eventData) {
        RectTransform invPanel = inventoryPannel as RectTransform;

        if (RectTransformUtility.RectangleContainsScreenPoint(invPanel, Input.mousePosition)) {
            Debug.Log("Dropped in inventory!");
            Inventory.instance.Add(item);
            transform.localPosition = Vector3.zero;
        } else {
            transform.localPosition = Vector3.zero;
        } 
    }
}
