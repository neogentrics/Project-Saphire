using UnityEngine;


//Type of Items
public enum ItemType
{
    HealthObject,
    ManaObject,
    XPObject,
    BagOfGold,
    BagOfRuby,
    Helmet,
    Ring,
    Necklace,
    Weapon,
    Chest,
    Shield,
    boots,
    Potion,
    Scroll,
    Default,
    Books,
    Arrow,
    Staff,
    Sword
}

public enum Attributes
{
    Health,
    Mana,
    Agility,
    Intelect,
    Stamina,
    Strength,
    Dexterity
}
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory System/Items/item")]
public class ItemObject : ScriptableObject
{

    //Sprite Display
    public Sprite uiDisplay;
    public bool stackable;

    public ItemType type;
    [TextArea(15, 20)]
    public string description;
    public Item data = new Item();

    /*/zmatias_v2
    //Prefab of item used on spawn
    public GameObject Prefab;
    */

    public Item CreateItem()
    {
        Item newItem = new Item(this);
        return newItem;
    }


}

[System.Serializable]
public class Item
{
    public string Name;
    public int Id = -1;
    public ItemBuff[] buffs;
    public Item()
    {
        Name = "";
        Id = -1;
    }
    public Item(ItemObject item)
    {
        Name = item.name;
        Id = item.data.Id;
        buffs = new ItemBuff[item.data.buffs.Length];
        for (int i = 0; i < buffs.Length; i++)
        {
            buffs[i] = new ItemBuff(item.data.buffs[i].min, item.data.buffs[i].max)
            {
                attribute = item.data.buffs[i].attribute
            };
        }
    }
}

[System.Serializable]
public class ItemBuff : IModifier
{
    public Attributes attribute;
    public int value;
    public int min;
    public int max;
    public ItemBuff(int _min, int _max)
    {
        min = _min;
        max = _max;
        GenerateValue();
    }

    public void Addvalue(ref int baseValue)
    {
        baseValue += value;
    }

    public void GenerateValue()
    {
        value = UnityEngine.Random.Range(min, max);
    }
}
