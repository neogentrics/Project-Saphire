using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equiptment Object", menuName = "Inventory System/Items/Chest Object")]
public class ChestObject : ItemObject
{
    public void Awake()
    {
        type = ItemType.Chest;
    }
}