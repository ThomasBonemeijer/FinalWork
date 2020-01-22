using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Usable Object", menuName = "Inventory System/Items/Usable")]
public class UsableObject : ItemObject
{
   public void Awake() {
       type = ItemType.Usable;
   }
}
