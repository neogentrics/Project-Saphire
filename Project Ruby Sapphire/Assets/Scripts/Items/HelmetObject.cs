using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equiptment Object", menuName = "Inventory System/Items/Helmet Object")]
public class HelmetObject : ItemObject
{    
    public void Awake()
    {
        type = ItemType.Helmet;
    }
}