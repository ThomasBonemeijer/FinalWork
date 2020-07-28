using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadoutHandler : MonoBehaviour
{
    
    GameObject player;
    public bool canDeleteItems = true;
    public GameObject lanternSlot;
    public GameObject gunSlot;
    public GameObject lanternImage;
    public GameObject gunImage;
    bool fuelInSlot = false;
    bool ammoInSlot = false;
    public Sprite lanternOnImg;
    public Sprite lanternOffImg;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null) {
            if(player.GetComponent<PlayerResourceHandler>().fuelCount != 0) {
                lanternImage.transform.GetChild(0).GetComponent<Image>().sprite = lanternOnImg;
            } else {
                lanternImage.transform.GetChild(0).GetComponent<Image>().sprite = lanternOffImg;
            }
        }

        // check if both crafting slots have a child element
        if (lanternSlot.transform.childCount > 0 && gunSlot.transform.childCount > 0) {
            Item knifeSlotItem = lanternSlot.transform.GetChild(0).GetComponent<IconItem>().item;
            Item gunSlotItem = gunSlot.transform.GetChild(0).GetComponent<IconItem>().item;

            // both loadout slots are filled
            if ((lanternSlot.transform.GetChild(0).GetComponent<IconItem>().isFilled == true) && (gunSlot.transform.GetChild(0).GetComponent<IconItem>().isFilled == true)) {
                canDeleteItems = false;
                // onlyFirstItemIsFilled = false;
                // CraftCombinedItem(slot1Item, slot2Item);
                // Debug.Log("Both");
            }
            // only knife slot is filled
            else if (lanternSlot.transform.GetChild(0).GetComponent<IconItem>().isFilled == true && gunSlot.transform.GetChild(0).GetComponent<IconItem>().isFilled == false) {
                canDeleteItems = false;
                // onlyFirstItemIsFilled = true;
                // CraftSingleItem(slot1Item);
                // Debug.Log("First");
                if (lanternSlot.transform.GetChild(0).GetComponent<IconItem>().item.name == "TreeSap") {
                    fuelInSlot = true;
                } else {
                    fuelInSlot = false;
                }
            } 
            // only gun slot is filled
            else if (gunSlot.transform.GetChild(0).GetComponent<IconItem>().isFilled == true && lanternSlot.transform.GetChild(0).GetComponent<IconItem>().isFilled == false) {
                canDeleteItems = false;
                // onlyFirstItemIsFilled = false;
                // CraftSingleItem(slot2Item);
                // Debug.Log("Second");
                if (gunSlot.transform.GetChild(0).GetComponent<IconItem>().item.name == "NormalBullet") {
                    ammoInSlot = true;
                } else {
                    ammoInSlot = false;
                }
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
            // Debug.Log("Fuel added!");
            if (player != null) {
                player.GetComponent<PlayerResourceHandler>().fuelCount += 5;
                ClearSlot(lanternSlot);
            } else {
                // Debug.LogError("loadout inventory has no Player assigned");
            }
        } else {
            // Debug.Log("No fuel in slot!");
        }
    }

    public void AddAmmo() {
        if (ammoInSlot == true) {
            // Debug.Log("Ammo Added!");
            if (player != null) {
                player.GetComponent<PlayerResourceHandler>().ammoCount += 25;
                ClearSlot(gunSlot);
            } else {
                // Debug.LogError("loadout inventory has no Player assigned");
            }
        } else {
            // Debug.Log("No ammo in slot!");
        }
    }

    void ClearSlot(GameObject slotName) {
        slotName.transform.GetChild(0).GetComponent<Image>().sprite = null;
            slotName.transform.GetChild(0).GetComponent<Image>().enabled = false;
            slotName.transform.GetChild(0).GetComponent<IconItem>().currentInventoryItem.GetComponent<IconItem>().RemoveCurrentInvItem();
            slotName.transform.GetChild(0).GetComponent<IconItem>().ClearCraftingSlot();
            slotName.transform.GetChild(0).GetComponent<IconItem>().item = null;
            ammoInSlot = false;
            fuelInSlot = false;
    }
}
