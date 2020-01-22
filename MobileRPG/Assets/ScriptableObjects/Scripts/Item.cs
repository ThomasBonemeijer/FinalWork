using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;

    public enum ItemType{useable, material}
    public ItemType itemType;

    public virtual void Use() {
        // Use the item
        Debug.Log("Using " + name);
    }

}
