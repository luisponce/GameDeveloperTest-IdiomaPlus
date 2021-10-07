using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackUp : Item
{
    private readonly int attkToIncrease;

    public AttackUp(string name, int amount, int attkToIncrease) : base(name, amount)
    {
        this.attkToIncrease = attkToIncrease;
    }

    public override int Use(PlayerControl player)
    {
        if (Amount > 0)
        {
            player.AtkUp(attkToIncrease);
            return --Amount;
        }
        else return Amount;
        
    }
}
