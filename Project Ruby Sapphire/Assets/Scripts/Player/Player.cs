using UnityEngine;

public class Player : PlayerHealth
{
    public static Player Instance { get; private set; }

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    public Attribute[] attributes;


    public InventoryObject inventory;
    public InventoryObject equipment;

    // Logic
    private bool isWalking;
    private SpriteRenderer spriteRenderer;

   

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one Player Instance");
        }
        Instance = this;
    }

    protected override void Start()
    {
        base.Start();


        spriteRenderer = GetComponent<SpriteRenderer>();

        for (int i = 0; i < attributes.Length; i++)
        {
            attributes[i].SetParent(this);
        }

        for (int i = 0; i < equipment.GetSlots.Length; i++)
        {
            equipment.GetSlots[i].OnBeforeUpdate += OnBeforeSlotUpdate;
            equipment.GetSlots[i].OnAfterUpdate += OnAfterSlotUpdate;
        }

    }


    private void Update()
    {
        HandleMovement();

        // Old Inventory Save/Load System
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            inventory.Save();
            equipment.Save();
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            inventory.Load();
            equipment.Load();
        }
        */
    }

    // Player Movement

    public bool IsWalking()
    {
        return isWalking;
    }

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = .7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        if (!canMove)
        {
            // Cannot move towards moveDir

            //Attempt only X movement
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = (moveDir.x < -.5f || moveDir.x > +.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                // Can move only on the X
                moveDir = moveDirX;
            }
            else
            {
                // Cannot move only on the X

                //Attempt only Y movement
                Vector3 moveDirY = new Vector3(0, moveDir.y, 0).normalized;
                canMove = (moveDir.y < -.5f || moveDir.y > +.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirY, moveDistance);

                if (canMove)
                {
                    // Can move only on the Y
                    moveDir = moveDirY;
                }
                else
                {
                    // Cannot move in any direction

                }
            }

        }

        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }

        isWalking = moveDir != Vector3.zero;

        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }

    // Player Damage System

    protected override void ReceiveDamage(DamageSystem dmg)
    {
        if (!isAlive)
            return;
        base.ReceiveDamage(dmg);
        GameManager.Instance.OnHitPointChange();

    }

    // Player Level System 

    public void OnLevelUp()
    {
        maxHitpoint *= 2;
        hitpoints = maxHitpoint;
        GameManager.Instance.ShowText("+" + 5 + " Ruby!", 30, Color.magenta, transform.position, Vector3.up * 50, 1.5f);
        inventory.Save();
    }
    public void SetLevel(int level)
    {
        for (int i = 0; i < level; i++)
            OnLevelUp();
    }

    // Inventory System Access 

    public void OnBeforeSlotUpdate(InventorySlot _slot)
    {
        if (_slot.ItemObject == null)
            return;
        switch (_slot.parent.inventory.type)
        {
            case InterfaceType.Inventory:
                break;
            case InterfaceType.Equipment:
                print(string.Concat("Removed ", _slot.ItemObject, " on ", _slot.parent.inventory.type, ", Allowed Items: ", string.Join(", ", _slot.AllowedItems)));

                for (int i = 0; i < _slot.item.buffs.Length; i++)
                {
                    for (int j = 0; j < attributes.Length; j++)
                    {
                        if (attributes[j].type == _slot.item.buffs[i].attribute)
                            attributes[j].value.RemoveModifer(_slot.item.buffs[i]);
                    }
                }

                break;
            case InterfaceType.Chest:
                break;
            default:
                break;
        }
    }
    public void OnAfterSlotUpdate(InventorySlot _slot)
    {
        if (_slot.ItemObject == null)
            return;
        switch (_slot.parent.inventory.type)
        {
            case InterfaceType.Inventory:
                break;
            case InterfaceType.Equipment:
                print(string.Concat("Placed ", _slot.ItemObject, " on ", _slot.parent.inventory.type, ", Allowed Items: ", string.Join(", ", _slot.AllowedItems)));

                for (int i = 0; i < _slot.item.buffs.Length; i++)
                {
                    for (int j = 0; j < attributes.Length; j++)
                    {
                        if (attributes[j].type == _slot.item.buffs[i].attribute)
                            attributes[j].value.AddModifer(_slot.item.buffs[i]);
                    }
                }

                break;
            case InterfaceType.Chest:
                break;
            default:
                break;
        }
    }

    public void UseItem(Item item)
    {
        Debug.Log("I habe been ordered to use item :" + item.Name);
    }

    public void AttributeModified(Attribute attribute)
    {
        Debug.Log(string.Concat(attribute.type, " was updated! Value is now ", attribute.value.ModifiedValue));
    }

    //Clear inventory when closing
    private void OnApplicationQuit()
    {
        inventory.Clear();
        equipment.Clear();
    }

}

[System.Serializable]
public class Attribute
{
    [System.NonSerialized]
    public Player parent;
    public Attributes type;
    public ModifiableInt value;


    public void SetParent(Player _parent)
    {
        parent = _parent;
        value = new ModifiableInt(AttributeModified);
    }
    public void AttributeModified()
    {
        parent.AttributeModified(this);
    }

    public int TotalValue()
    {
        return value.BaseValue + value.ModifiedValue;
    }

    /*
    
    // Player Class Check

    // Get the current class of the player
PlayerClass currentClass = GetComponent<PlayerClass>();

// Check if the player can change class
if (currentClass.canChangeClass)
{
    // Display the available classes to the player
    Debug.Log("Available Classes:");
    foreach (UnlockedList className in currentClass.unlockedClasses)
    {
        Debug.Log(className.ToString() + " (Level " + currentClass.GetClassLevel(className) + ")");
    }

    foreach (LockedClasses className in currentClass.lockedClasses)
    {
        Debug.Log(className.ToString() + " (Level " + currentClass.GetClassLevel(className) + ")");
    }

    foreach (AdvancedLockedClasses className in currentClass.advancedLockedClasses)
    {
        Debug.Log(className.ToString() + " (Level " + currentClass.GetClassLevel(className) + ")");
    }

    // Let the player select a new class
    UnlockedList newClass = UnlockedList.Swordsman; // replace with actual input from player

    // Change the player's class if the selected class is unlocked and the player's level is high enough
    if (currentClass.unlockedClasses.Contains(newClass) && currentClass.GetClassLevel(newClass) >= 30)
    {
        currentClass.ChangeClass(newClass);
        Debug.Log("Changed class to " + newClass.ToString());
    }
    else
    {
        Debug.Log("Selected class is locked or player's level is too low");
    }
}
else
{
    Debug.Log("Cannot change class at the moment");
}
*/

}

