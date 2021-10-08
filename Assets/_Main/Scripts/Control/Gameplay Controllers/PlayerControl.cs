using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    private static PlayerControl instance; //for the singleton

    public CharacterController heroController;
    public Transform characterModel;

    #region Movement Variables
    [SerializeField]
    private float maxMoveSpeed = 7f;
    private Vector3 curSpeed = new Vector3(0,0);

    [SerializeField]
    private float movementAceleration = 0.07f;
    [SerializeField]
    private float movementDeaceleration = 0.05f;
    [SerializeField]
    private float movementClamp = 0.1f;

    private Vector3 moveDir = new Vector3(0, 0);
    private bool isGainingSpeed = false;
    #endregion

    #region Combat variables
    private float attackSpherecastRadius = 0.5f;
    [SerializeField]
    private float attackReach = 1.5f;

    [SerializeField]
    private int damage = 5;
    [SerializeField]
    private float atkSpeed = 1.5f;
    private float lastAtkTime;

    private readonly int maxHealth = 50; //remove readonly for mechanics that change maxHP
    private int health;

    public delegate void HealthChange();
    public HealthChange OnHealthChange;

    public delegate void DamageChange();
    public DamageChange OnDamageChange;
    #endregion

    #region Inventory Variables
    private int invSize = 5;
    private Item[] inventory;

    public delegate void InventoryChange();
    public InventoryChange OnInventoryChange;

    private float invLastUse;
    private float useItemCooldown = 1;
    #endregion

    #region unity events
    void Awake()
    {
        #region SINGLETON
        if (instance != null)
        {
            Destroy(gameObject); //Delete if another instance already exists and is assigned.
            return;
        }
        instance = this; //set this as the singleton
        //DontDestroyOnLoad(gameObject); //to make it persist between levels
        #endregion

        health = maxHealth;
        inventory = new Item[invSize];

        invLastUse = 0;
    }

    void Start()
    {
        InputHandler.Instance.OnForwardMovement += MoveForward;
        InputHandler.Instance.OnLeftMovement += MoveLeft;
        InputHandler.Instance.OnBackwardsMovement += MoveBackward;
        InputHandler.Instance.OnRightMovement += MoveRight;
        InputHandler.Instance.OnAttack += Attack;

        InputHandler.Instance.OnHotbar += UseInventory;
    }

    void Update()
    {
        if (isGainingSpeed)
        {
            curSpeed += movementAceleration * moveDir;
            if (curSpeed.magnitude >= maxMoveSpeed)
            {
                curSpeed = curSpeed.normalized * maxMoveSpeed;
            }
            characterModel.LookAt(characterModel.transform.position + curSpeed);
            isGainingSpeed = false;
        } else
        {
            if(curSpeed.magnitude > movementClamp)
            {
                curSpeed += movementDeaceleration * -curSpeed.normalized;
            }
        }

        if(curSpeed.magnitude > movementClamp) heroController.Move(curSpeed * Time.deltaTime);
    }

    private void OnDestroy()
    {
        InputHandler.Instance.OnForwardMovement -= MoveForward;
        InputHandler.Instance.OnLeftMovement -= MoveLeft;
        InputHandler.Instance.OnBackwardsMovement -= MoveBackward;
        InputHandler.Instance.OnRightMovement -= MoveRight;
        InputHandler.Instance.OnAttack -= Attack;

        InputHandler.Instance.OnHotbar -= UseInventory;
    }
    #endregion

    #region Movement Events
    public void MoveForward()
    {
        moveDir += CameraControl.Instance.transform.forward;
        moveDir.y = 0;
        moveDir.Normalize();
        isGainingSpeed = true;
    }

    public void MoveLeft()
    {
        moveDir -= CameraControl.Instance.transform.right;
        moveDir.y = 0;
        moveDir.Normalize();
        isGainingSpeed = true;
    }

    public void MoveBackward()
    {
        moveDir -= CameraControl.Instance.transform.forward;
        moveDir.y = 0;
        moveDir.Normalize();
        isGainingSpeed = true;
    }

    public void MoveRight()
    {
        moveDir += CameraControl.Instance.transform.right;
        moveDir.y = 0;
        moveDir.Normalize();
        isGainingSpeed = true;
    }
    #endregion

    #region Combat
    public void Attack()
    {
        if(lastAtkTime + atkSpeed < Time.time)
        {
            if (Physics.SphereCast(characterModel.position + heroController.center, attackSpherecastRadius, characterModel.forward, out RaycastHit hit, attackReach))
            {
                Grunt grunt = hit.collider.GetComponent<Grunt>();
                if (grunt != null)
                {
                    grunt.Damage(damage);
                }
            }

            lastAtkTime = Time.time;
        }
    }

    public void DealDamage(int dmg)
    {
        health -= dmg;
        OnHealthChange?.Invoke();
        if (health <= 0)
        {
            health = 0;
            Die();
        }
    }

    public void Die()
    {
        GameController.Instance.LoseGame();
    }
    #endregion

    #region items
    public void Heal(int hp)
    {
        if (health > 0)
        {
            health += hp;
            if (health > maxHealth) health = maxHealth;
            OnHealthChange?.Invoke();
        }
    }

    public void AtkUp(int atkUp)
    {
        //TODO: change to a temporary power up
        damage += atkUp;

        OnDamageChange?.Invoke();
    }

    public bool AddToInventory(Item item) //returns if the item was added or not
    {
        foreach (Item i in inventory)
        {
            if(i != null && item.Name == i.Name)
            {
                i.Amount += item.Amount;
                OnInventoryChange?.Invoke();
                return true;
            }
        }

        for (int i=0; i < inventory.Length; i++)
        {
            if(inventory[i] == null)
            {
                inventory[i] = item;
                OnInventoryChange?.Invoke();
                return true;
            }
        }

        return false;
    }

    public void UseInventory(int slot)
    {
        if (invLastUse + useItemCooldown <= Time.time && inventory[slot] != null)
        {
            int remaining = inventory[slot].Use(this);
            invLastUse = Time.time;
            if (remaining <= 0)
            {
                inventory[slot] = null;
                SortInventory();
            } else
            {
                inventory[slot].Amount = remaining;
            }
            OnInventoryChange?.Invoke();
        } 
    }

    public void SortInventory()
    {
        Item[] newInv = new Item[inventory.Length];
        int i = 0;
        foreach (Item item in inventory)
        {
            if(item != null)
            {
                newInv[i] = item;
                i++;
            }
        }
        inventory = newInv;
        OnInventoryChange?.Invoke();
    }
    #endregion

    #region PROPERTIES
    public static PlayerControl Instance { get => instance; }
    public int Health { get => health; }

    public int MaxHealth => maxHealth;

    public Item[] Inventory { get => inventory; }
    public int Damage { get => damage; }
    #endregion
}
