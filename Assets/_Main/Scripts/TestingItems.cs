using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingItems : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        HealthRestore hp = ItemFactory.CreateHealthPot(5);
        PlayerControl.Instance.AddToInventory(hp);

        AttackUp atk = ItemFactory.CreatePowerUp(5);
        PlayerControl.Instance.AddToInventory(atk);
    }
}
