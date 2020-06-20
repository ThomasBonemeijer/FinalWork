using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
// [System.Serializable]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;

    public enum ItemType{useable, material}
    public ItemType itemType;

    public virtual void Use(int itemIndex) {
        // Use the item
        Debug.Log("Using " + itemIndex + " " + name);
    }

}
