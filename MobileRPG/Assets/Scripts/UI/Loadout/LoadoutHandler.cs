using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadoutHandler : MonoBehaviour
{
    GameObject player;
    public bool canDeleteItems = true;
    public GameObject knifeSlot;
    public GameObject gunSlot;
    public GameObject knifeImage;
    public GameObject gunImage;
    bool fuelInSlot = false;
    bool ammoInSlot = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // check if both crafting slots have a child element
        if (knifeSlot.transform.childCount > 0 && gunSlot.transform.childCount > 0) {
            Item knifeSlotItem = knifeSlot.transform.GetChild(0).GetComponent<IconItem>().item;
            Item gunSlotItem = gunSlot.transform.GetChild(0).GetComponent<IconItem>().item;

            // both loadout slots are filled
            if ((knifeSlot.transform.GetChild(0).GetComponent<IconItem>().isFilled == true) && (gunSlot.transform.GetChild(0).GetComponent<IconItem>().isFilled == true)) {
                canDeleteItems = false;
                // onlyFirstItemIsFilled = false;
                // CraftCombinedItem(slot1Item, slot2Item);
                // Debug.Log("Both");
            }
            // only knife slot is filled
            else if (knifeSlot.transform.GetChild(0).GetComponent<IconItem>().isFilled == true && gunSlot.transform.GetChild(0).GetComponent<IconItem>().isFilled == false) {
                canDeleteItems = false;
                // onlyFirstItemIsFilled = true;
                // CraftSingleItem(slot1Item);
                // Debug.Log("First");
                if (knifeSlot.transform.GetChild(0).GetComponent<IconItem>().item.name == "TreeSap") {
                    fuelInSlot = true;
                } else {
                    fuelInSlot = false;
                }
            } 
            // only gun slot is filled
            else if (gunSlot.transform.GetChild(0).GetComponent<IconItem>().isFilled == true && knifeSlot.transform.GetChild(0).GetComponent<IconItem>().isFilled == false) {
                canDeleteItems = false;
                // onlyFirstItemIsFilled = false;
                // CraftSingleItem(slot2Item);
                // Debug.Log("Second");
            } 
            // no crafting slots are filled
            else {
                canDeleteItems = true;
                // onlyFirstItemIsFilled = false;
                // Debug.Log("None");
            }
        }
    }

    public void AddFuel() {
        if (fuelInSlot == true) {
            Debug.Log("Fuel added!");
            if (player != null) {
                player.GetComponent<PlayerResourceHandler>().fuelCount += 5;
            } else {
                Debug.LogError("loadout inventory has no Player assigned");
            }
        } else {
            Debug.Log("No fuel in slot!");
        }
    }

    public void AddAmmo() {

    }
}
