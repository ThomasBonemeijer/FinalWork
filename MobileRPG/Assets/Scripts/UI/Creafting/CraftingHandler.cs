using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class CraftingHandler : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public bool canDeleteItems = true;
    public GameObject slot1;
    public GameObject slot2;
    public GameObject outputSlot;

    void Update() {
        if ((slot1.transform.GetChild(0).GetComponent<IconItem>().isFilled == true) || (slot2.transform.GetChild(0).GetComponent<IconItem>().isFilled == true)) {
            canDeleteItems = false;
        }
        else {
            canDeleteItems = true;
        }
    }

    public void OnDrag(PointerEventData eventData) {

    }
    public void OnEndDrag(PointerEventData eventData) {

    }
}
