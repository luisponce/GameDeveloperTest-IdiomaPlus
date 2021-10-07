using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item 
{
    private string name; //for UI and stacking
    private int amount; //number of uses

    public Item(string name, int amount = 1)
    {
        this.name = name;
        this.amount = amount;
    }

    public abstract int Use(PlayerControl player); //returns the amount remaining after use

    #region properties
    public string Name { get => name; }
    public int Amount { get => amount; set => amount = value; }
    #endregion
}

public class ItemFactory
{
    public const string hpName = "healt pot";
    public const string atkUpName = "Attack Power Up";

    public static HealthRestore CreateHealthPot(int amount = 1)
    {
        return new HealthRestore(hpName, amount, 10);
    }

    public static AttackUp CreatePowerUp(int amount = 1)
    {
        return new AttackUp(atkUpName, amount, 3);
    }
}
