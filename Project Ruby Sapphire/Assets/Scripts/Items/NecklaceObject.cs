using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equiptment Object", menuName = "Inventory System/Items/Necklace Object")]

public class NecklaceObject : ItemObject
{
    public void Awake()
    {
        type = ItemType.Necklace;
    }
}
