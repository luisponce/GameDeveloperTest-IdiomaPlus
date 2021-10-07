using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Sprite hpPot;
    public Sprite atkUp;

    public RectTransform[] inventoryFrames;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInventory();
    }

    public void UpdateInventory()
    {
        for(int i = 0; i < inventoryFrames.Length; i++)
        {
            Item itm = PlayerControl.Instance.Inventory[i];
            RectTransform frame = inventoryFrames[i];

            Image img = frame.Find("Icon").GetComponent<Image>();
            Text txt = frame.Find("Amount").GetComponent<Text>();

            if (itm != null)
            {
                switch (itm.Name)
                {
                    case ItemFactory.hpName:
                        img.enabled = true;
                        img.sprite = hpPot;

                        txt.enabled = true;
                        txt.text = "" + itm.Amount;
                        break;
                    case ItemFactory.atkUpName:
                        img.enabled = true;
                        img.sprite = atkUp;

                        txt.enabled = true;
                        txt.text = "" + itm.Amount;
                        break;
                    default:
                        img.enabled = false;
                        txt.enabled = false;
                        break;
                }
            } else
            {
                img.enabled = false;
                txt.enabled = false;
            }
        }
    }
}
