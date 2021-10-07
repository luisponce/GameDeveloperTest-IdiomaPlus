using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    enum PickupType { hp, atkUp };

    [SerializeField]
    private PickupType type = PickupType.hp;

    private void OnTriggerStay(Collider other)
    {
        PlayerControl player;
        if (player = other.GetComponent<PlayerControl>()){
            bool taken = player.AddToInventory(GetPickableItem());

            if (taken)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private Item GetPickableItem()
    {
        switch (type)
        {
            case PickupType.hp:
                return ItemFactory.CreateHealthPot();
            case PickupType.atkUp:
                return ItemFactory.CreatePowerUp();
            default:
                return null;
        }
    }
}
