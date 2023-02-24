using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equiptment Object", menuName = "Inventory System/Items/Ring Object")]

public class RingObject : ItemObject
{
    public void Awake()
    {
        type = ItemType.Ring;
    }
}
