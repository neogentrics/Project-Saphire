using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equiptment Object", menuName = "Inventory System/Items/Boots Object")]
public class BootsObject : ItemObject
{
    public void Awake()
    {
        type = ItemType.boots;
    }
}
