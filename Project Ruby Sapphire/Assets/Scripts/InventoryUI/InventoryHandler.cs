using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class Inventory_Handler : MonoBehaviour
{
    //Zmatias_V2
    //Serialised Attributes
    //Must be assigned on Inspectors
    [Header("Serialised Attributes")]
    public TextMeshProUGUI UseButton;
    public TextMeshProUGUI UnUseButton;
    public TextMeshProUGUI ItemName;
    public TextMeshProUGUI ItemDescription;
    public Image ItemIco;
    public TextMeshProUGUI Warning;
    public bool DropArea;
    public Transform charScreenValues;

    [Header("Changing Attributes")]

    //Zmatias_V2
    //Every time is slot is selected save it on variables
    //Same with GameObject
    public InventorySlot selectedInvSlot;
    public GameObject selectedInvObj;

    //Zmatias_V2
    //Save variable of GameManager in order to access it from below on heirachi obj.
    [HideInInspector] public GameManager gm;

    //Zmatias_V2
    //Instance of inventory, grab from player script
    private InventoryObject InvBag;
    private InventoryObject InvEquipment;



    void Start()
    {

        var player = gm.player.GetComponent<Player>();
        InvBag = Player.Instance.inventory;
        InvEquipment = Player.Instance.equipment;

        //Set defaul behavior of buttons
        //When no object is selected
        SetDefaultWarningMethod();
    }

    //Zmatias_V2
    //Update variables when opening menu
    //Null check for gm During first time to evade null Errors
    private void OnEnable()
    {
        if (gm == null)
        {
            gm = GameManager.Instance;
        }
        SetDefaultWarningMethod();
        UpdateCharStats();
    }

    //Zmatias_V2
    //Action executed when invetory is closed
    //Method is activated by Inventory button and its assigned on inspector
    public void onCloseInv()
    {
        UnselectSlot();
        Warning.gameObject.SetActive(false);
        SetDefaultWarningMethod();
    }

    //Zmatias_V2
    //Reset view of middle window on inventory
    //Change ico,description,name
    private void SetDefaultItemView()
    {
        ItemName.text = "[ Select Item ]";
        ItemDescription.text = " ";
        var tempColor = ItemIco.color;
        tempColor.a = 0f;
        ItemIco.color = tempColor;
    }

    //Zmatias_V2
    //This method is executed each time inventory(charMenu) is executed
    //The variable are gathered from player script
    //If any added valueS(from item) are applied the text is changing to Green color
    public void UpdateCharStats()
    {
        var playerAtrr = gm.player.GetComponent<Player>().attributes;
        for (int i = 0; i < playerAtrr.Length; i++)
        {
            //Dont Change Color
            var tmPRO = charScreenValues.GetChild(i).GetComponent<TextMeshProUGUI>();
            if (playerAtrr[i].value.BaseValue != playerAtrr[i].TotalValue())
            {
                tmPRO.color = Color.green;
            }
            //Change Color
            else
            {
                tmPRO.color = Color.white;
            }
            //Apply value to text
            tmPRO.text = playerAtrr[i].TotalValue().ToString();
        }
    }

    //Zmatias_V2
    //Set method when nothing is selected,add event to buttons
    //Activating message error screen when pressed
    public void SetDefaultWarningMethod()
    {
        //Set defaul behavior of buttons
        //When no object is selected
        var myButton = UnUseButton.transform.parent;
        UnityEngine.UI.Button btn = myButton.GetComponent<UnityEngine.UI.Button>();
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() => { DefaultNothingSelected(); });

        var mySecButton = UseButton.transform.parent;
        UnityEngine.UI.Button btn2 = mySecButton.GetComponent<UnityEngine.UI.Button>();
        btn2.onClick.RemoveAllListeners();
        btn2.onClick.AddListener(() => { DefaultNothingSelected(); });
    }

    //Zmatias_V2
    //Activate warning message
    public void DefaultNothingSelected()
    {
        //Debug.Log("Not selected");
        Warning.gameObject.SetActive(true);
    }

    //Zmatias_V2
    //Unselect slot 
    public void UnselectSlot()
    {
        if (selectedInvObj != null)
        {
            selectedInvObj.transform.Find("Selected").gameObject.SetActive(false);
            selectedInvObj = null;
            //selectedInvSlot = null;
        }

    }

    //Zmatias_V2
    //Select slot 
    public void SelectSlot(GameObject selSlot)
    {
        selectedInvObj = selSlot;
        if (selectedInvObj != null)
        {
            selectedInvObj.transform.Find("Selected").gameObject.SetActive(true);
        }
    }

    //Zmatias_V2
    //This method is used to find free inventory slot
    //It is used when player unequips an item
    //Find slot to place on Inventory bag
    public InventorySlot FindFreeSlot(InventorySlot[] GetSlots)
    {
        for (int i = 0; i < GetSlots.Length; i++)
        {
            if (GetSlots[i].item.Id <= -1)
            {
                return GetSlots[i];
            }
        }
        return null;
    }


    //Zmatias_V2
    //This method is used to equip item when pressed button
    //Find position that matches based on equiped object
    public InventorySlot FindEquipedSlotCorrectPOS(InventorySlot[] GetSlots, Item item)
    {
        for (int i = 0; i < GetSlots.Length; i++)
        {
            if (GetSlots[i].item.Id <= -1)
            {
                return GetSlots[i];
            }
        }
        return null;
    }


    //Zmatias_V2
    //Update text description based on input values
    public void UpdateTextDescr(string name, string textDes, Sprite itemIco)
    {
        ItemName.text = name;
        ItemDescription.text = textDes;

        //Alpha color change
        var tempColor = ItemIco.color;
        tempColor.a = 1f;
        ItemIco.sprite = itemIco;
        ItemIco.color = tempColor;
    }

    //Zmatias_V2
    //Change Button Based on selection
    //If selected on Consumable and on bag change buttons
    public void isConsumable()
    {
        //Debug.Log("is consumable");
        UseButton.text = "Use";
        var mySecButton = UseButton.transform.parent;
        UnityEngine.UI.Button btn2 = mySecButton.GetComponent<UnityEngine.UI.Button>();
        btn2.onClick.RemoveAllListeners();
        btn2.onClick.AddListener(() => { Use(); });

        //Function for drop
        UnUseButton.text = "Drop";
        var myButton = UnUseButton.transform.parent;
        UnityEngine.UI.Button btn = myButton.GetComponent<UnityEngine.UI.Button>();
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() => { DropObject(); });
    }

    //Zmatias_V2
    //Change Button Based on selection
    //If selected on Wearable and on Bag change buttons
    public void isWearable_bag()
    {
        UseButton.text = "Equip";
        var mySecButton = UseButton.transform.parent;
        UnityEngine.UI.Button btn2 = mySecButton.GetComponent<UnityEngine.UI.Button>();
        btn2.onClick.RemoveAllListeners();
        btn2.onClick.AddListener(() => { Equip(); });


        //Function for drop
        UnUseButton.text = "Drop";
        var myButton = UnUseButton.transform.parent;
        UnityEngine.UI.Button btn = myButton.GetComponent<UnityEngine.UI.Button>();
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() => { DropObject(); });
    }

    //Zmatias_V2
    //Change Button Based on selection
    //If selected on Wearable and on Equipemnt change buttons
    public void isWearable_EquipInv()
    {
        UseButton.text = "Unequip";
        var mySecButton = UseButton.transform.parent;
        UnityEngine.UI.Button btn2 = mySecButton.GetComponent<UnityEngine.UI.Button>();
        btn2.onClick.RemoveAllListeners();
        btn2.onClick.AddListener(() => { UnEquip(); });


        //Function for drop
        UnUseButton.text = "Drop";
        var myButton = UnUseButton.transform.parent;
        UnityEngine.UI.Button btn = myButton.GetComponent<UnityEngine.UI.Button>();
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() => { DropObject(); });
    }

    //Zmatias_V2
    //Drop Item method when pressed from button or placed outside inv
    private void DropObject()
    {
        if (selectedInvSlot.item.Id >= 0)
        {   /*
             * This Section is incomplete or bugged in code fix later
             * 
            var itemdropped = Instantiate(selectedInvSlot.ItemObject.Prefab, gm.player.transform.position + new Vector3(0, 0.15f, 0), Quaternion.identity);
            itemdropped.name = selectedInvSlot.ItemObject.name;
            //itemdropped.GetComponent<GroundItem>().enabled=false;
            itemdropped.GetComponent<GroundItem>().manualUpdateUI();
            //itemdropped.GetComponent<GroundItem>().enabled=true; */

            selectedInvSlot.RemoveItem();

            //Used for all
            UnselectSlot();
            SetDefaultItemView();
            SetDefaultWarningMethod();
        }
        else
        {
            //DefaultNothingSelected();
        }

    }

    //Zmatias_V2
    //Unquip Method
    private void UnEquip()
    {
        Debug.Log("UnEquip0");
        if (selectedInvSlot.item.Id >= 0)
        {
            var freeSlot = FindFreeSlot(InvBag.GetSlots);
            if (freeSlot != null)
            {
                Debug.Log("UnEquip");
                UnselectSlot();
                InvBag.SwapItems_NoCheck(selectedInvSlot, freeSlot);
                selectedInvSlot = freeSlot;
                SelectSlot(freeSlot.slotDisplay);
                isWearable_bag();
            }
            else
            {
                Debug.Log("UnEquip - no free slot");
            }
        }

    }

    //Zmatias_V2
    //Action when you use an item
    private void Use()
    {
        if (selectedInvSlot.item != null)
        {
            //Activate Action on player
            gm.player.GetComponent<Player>().UseItem(selectedInvSlot.item);

            //Change Amount
            if (selectedInvSlot.amount > 1)
            {
                selectedInvSlot.amount = selectedInvSlot.amount - 1;
                selectedInvSlot.UpdateSlot(selectedInvSlot.item, selectedInvSlot.amount);
            }
            //Remove Item
            else
            {
                UnselectSlot();
                SetDefaultItemView();
                SetDefaultWarningMethod();
                selectedInvSlot.RemoveItem();
                selectedInvSlot = null;
                selectedInvObj = null;
            }

        }
        else
        {
            Debug.Log("Null error");
        }

    }

    //Zmatias_V2
    //Equip Method
    private void Equip()
    {
        if (selectedInvSlot.item.Id >= 0)
        {
            if (selectedInvSlot.ItemObject)
            {
                Debug.Log("Is null : " + selectedInvSlot.ItemObject.type);
            }
            var newSlot = FindEquipmentSlot(selectedInvSlot.ItemObject);
            if (newSlot != null)
            {
                InvEquipment.SwapItems_NoCheck(selectedInvSlot, newSlot);
                selectedInvSlot = newSlot;
                UnselectSlot();
                SelectSlot(newSlot.slotDisplay);
                isWearable_EquipInv();
            }
            else
            {
                Debug.Log("Not enought space");
            }
        }
        else
        {
            //DefaultNothingSelected();
        }
    }

    //Zmatias_V2
    //This method is used to equip item when pressed button
    //Find position that matches based on equiped object
    public InventorySlot FindEquipmentSlot(ItemObject item)
    {
        var slotAllowedContainer = InvEquipment.Container.Slots;

        var pos = -1;

        for (int i = 0; i < slotAllowedContainer.Length; i++)
        {
            var slot = slotAllowedContainer[i];
            if (slot.AllowedItems[0].ToString() == item.type.ToString())
            {
                pos = i;
            }
        }

        if (pos != -1)
        {
            var newSlot = InvEquipment.GetSlots[pos];
            return newSlot;
        }

        return null;
    }



    private void Consume()
    {
        if (selectedInvSlot.item.Id >= 0)
        {
        }
        else
        {
            //DefaultNothingSelected();
        }
    }

    //Zmatias_V2
    //Check if drugable object is on drop zone
    public void IamOnDrop()
    {
        //Debug.Log("Drop");
        DropArea = true;

        var myButton = UnUseButton.transform.parent;
        UnityEngine.UI.Button btn = myButton.GetComponent<UnityEngine.UI.Button>();
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() => { DropObject(); });
    }

    //Zmatias_V2
    //Check if drugable object is outside drop zone
    public void ILeftDrop()
    {
        //Debug.Log("Not In Drop");
        DropArea = false;
    }




}
