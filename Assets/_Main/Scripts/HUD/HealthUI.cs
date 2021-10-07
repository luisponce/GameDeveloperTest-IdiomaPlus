using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Image healthBarMask;
    
    // Start is called before the first frame update
    void Start()
    {
        UpdateHealthBar();
        PlayerControl.Instance.OnHealthChange += UpdateHealthBar;
    }

    private void UpdateHealthBar()
    {
        healthBarMask.fillAmount = (float) PlayerControl.Instance.Health / (float) PlayerControl.Instance.MaxHealth;
    }
}
