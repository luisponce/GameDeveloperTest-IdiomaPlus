using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRestore : Item
{
    private readonly int hpToRestore;

    public HealthRestore(string name, int amount, int hpToRestore) : base(name, amount)
    {
        this.hpToRestore = hpToRestore;
    }

    public override int Use(PlayerControl player)
    {
        if (player.Health < player.MaxHealth && Amount > 0) //only heal if health missing and has uses remaining
        {
            player.Heal(hpToRestore);
            return --Amount;
        }
        else return Amount;
    }
}
