using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    public int level;
    public int health;
    public List<string> playerInventoryList;
    public GameObject gameManager;
    // Inventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        // inventory = Inventory.instance;
        // inventory.onItemChangedCallback += UpdateUI();
        LoadPlayer();
    }

    public void SavePlayer() {
        SetPlayerInv();
        SaveSystem.SavePlayer(this);
        Debug.Log("Player saved!");
    }

    public void LoadPlayer() {
        PlayerData data = SaveSystem.LoadPlayer();

        health = data.health;
        level = data.level;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;

        playerInventoryList = data.playerInventoryList;
        SetGameHandlerInventory();

        Debug.Log("Player loaded!");
    }

    public void ResetPlayer() {
        health = 100;
        level = 1;
        transform.position = new Vector3(0, 0, 0);

        SavePlayer();
    }

    public void SetPlayerInv() {
        playerInventoryList.Clear();
        for (int i = 0; i < Inventory.instance.items.Count; i++) {
            if (playerInventoryList.Count <= Inventory.instance.space) {
                playerInventoryList.Add(Inventory.instance.items[i].name);
            } else {
                return;
            }
        }
    }

    public void SetGameHandlerInventory() {
        for (int i = 0; i < playerInventoryList.Count; i++) {
            var listOfItemsScript = gameManager.GetComponent<ListOfItems>();
            listOfItemsScript.setReturnItem(playerInventoryList[i]);
            Item ItemToAdd = listOfItemsScript.returnItem;
            Inventory.instance.Add(ItemToAdd);
        }
    }
}
