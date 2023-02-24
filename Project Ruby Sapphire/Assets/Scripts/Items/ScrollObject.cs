using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equiptment Object", menuName = "Inventory System/Items/Scroll Object")]

public class ScrollObject : ItemObject
{
    public void Awake()
    {
        type = ItemType.Scroll;
        type = ItemType.Books;
    }
}

