using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AtackStatUI : MonoBehaviour
{

    void Start()
    {
        PlayerControl.Instance.OnDamageChange += UpdateAtkNumber;
        UpdateAtkNumber();
    }

    void UpdateAtkNumber()
    {
        GetComponent<Text>().text = "" + PlayerControl.Instance.Damage;
    }
}
