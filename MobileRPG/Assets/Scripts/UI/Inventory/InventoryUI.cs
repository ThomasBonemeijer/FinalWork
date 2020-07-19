using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    GameObject player;
    public Transform itemsParent;
    Inventory inventory;

    InventorySlot[] slots;
    
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        player = GameObject.Find("Player");
        UpdateUI();
    }

    public void UpdateUI() {
        Debug.Log("Updating Inventory!");
        GameObject.Find("Player").GetComponent<PlayerHandler>().fillHands();
        for (int i = 0; i < slots.Length; i++) {
            if (i < inventory.items.Count) {
                slots[i].AddItem(inventory.items[i]);
            } else {
                slots[i].ClearSlot();
            }
        }
        if (player != null) {
            player.GetComponent<PlayerHandler>().SetInventoryValues();
        } else {
            Debug.LogError("InventoryUI cant find player!");
        }
        // player.GetComponent<PlayerHandler>().SavePlayer();
    }
}
