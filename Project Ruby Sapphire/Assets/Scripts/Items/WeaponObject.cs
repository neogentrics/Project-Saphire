using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equiptment Object", menuName = "Inventory System/Items/Weapon Object")]

public class WeaponObject : ItemObject
{
    public void Awake()
    {
        type = ItemType.Sword;
        type = ItemType.Staff;
        type = ItemType.Arrow;
    }
}
