using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResourceHandler : MonoBehaviour
{
    public int fuelCount;
    public int ammoCount;
    public bool hasWaveSpawnObject;
    public bool hasOpenedStoneGate;
    public List<string> openedChestsList = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("FuelTick", 0f, 5f);
        openedChestsList = new List<string>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForSpawnItem();
    }

    void FuelTick() {
        if(fuelCount > 0 && GetComponent<PlayerHandler>().lanternIsOn == true) {
            fuelCount -= 1;
        }
    }

    public void AddChestnameToList(string chestName) {
        if (openedChestsList != null) {
            openedChestsList.Add(chestName);
        } else {
            openedChestsList = new List<string>();
            openedChestsList.Add(chestName);
        }
    }

    void CheckForSpawnItem() {
        for (int i = 0; i < Inventory.instance.items.Count; i++) {
                if (Inventory.instance.items[i].name == "SummonItem") {
                    // Inventory.instance.Remove(Inventory.instance.items[i]);
                    // player.GetComponent<PlayerHandler>().HealPlayer(false, 0, 50, "HealthPotion");
                    // return;
                    hasWaveSpawnObject = true;
                    return;
                } else {
                    hasWaveSpawnObject = false;
                }
            }
    }
}
