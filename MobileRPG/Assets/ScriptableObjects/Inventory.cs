using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;
    void Awake() {
        if (instance != null) {
            Debug.LogWarning("More than one instance of inventory found");
            return;
        }

        instance = this;
    }
    #endregion
    public Transform craftingInv;
    public Canvas BagUI;
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;
    public int space = 20;
    public List<Item> items = new List<Item>();

    public bool Add(Item item) {
        if (!item.isDefaultItem) {
            if (items.Count >= space) {
                Debug.Log("Inventory full!");
                return false;
            }
            items.Add(item);
            
            if (onItemChangedCallback != null) 
                onItemChangedCallback.Invoke();
        }
        return true;
    }

    public void Remove(Item item) {
        items.Remove(item);

        if (onItemChangedCallback != null && craftingInv.GetComponent<CraftingHandler>().isDeleting == false) {
            onItemChangedCallback.Invoke();
        }
    }

    public void RemoveByIndex(int index) {
        Debug.Log("Removed item [" + index + "]");
        items.RemoveAt(index);

        if (onItemChangedCallback != null && craftingInv.GetComponent<CraftingHandler>().isDeleting == false) {
            onItemChangedCallback.Invoke();
        }
    }
}
