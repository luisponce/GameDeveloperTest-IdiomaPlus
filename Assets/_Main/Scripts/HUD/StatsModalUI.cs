using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsModalUI : MonoBehaviour
{
    public Text winsTxt;
    public Text losesTxt;
    public Text playedTxt;

    void Start()
    {
        ReloadStats();
    }

    public void ReloadStats()
    {
        winsTxt.text = "" + Persistence.Instance.Wins;
        losesTxt.text = "" + Persistence.Instance.Loses;
        playedTxt.text = "" + Persistence.Instance.Played;
    }

    public void SetModal(bool active)
    {
        if (active)
        {
            ReloadStats();
            GetComponent<CanvasGroup>().alpha = 1;
        } else
        {
            GetComponent<CanvasGroup>().alpha = 0;
        }

        GetComponent<CanvasGroup>().interactable = active;
        GetComponent<CanvasGroup>().blocksRaycasts = active;
    }
}
