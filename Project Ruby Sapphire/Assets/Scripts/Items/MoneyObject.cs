using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Money Object", menuName = "Inventory System/Items/Money Object")]

public class MoneyObject : ItemObject
{

    public void Awake()
    {
        type = ItemType.BagOfGold;
        type = ItemType.BagOfRuby;
    }
}
