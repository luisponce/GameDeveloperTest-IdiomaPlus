using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private List<Grunt> enemiesRemaining;

    public Animator victoryScreenAnim;
    public Animator defeatScreenAnim;

    private static GameController instance;

    public const string VICTORY_FADER_VISIBLE = "visible";
    public const string DEFEAT_FADER_VISIBLE = "visible";

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

        enemiesRemaining = new List<Grunt>();
    }


    public void AddGruntToList(Grunt g)
    {
        enemiesRemaining.Add(g);
    }

    public void RemoveGruntFromList(Grunt g)
    {
        enemiesRemaining.Remove(g);
        Debug.Log(enemiesRemaining.Count);
        if(enemiesRemaining.Count == 0)
        {
            WinGame();
        }
    }

    public void WinGame()
    {
        victoryScreenAnim.SetBool(VICTORY_FADER_VISIBLE, true);
        
    }

    public void LoseGame()
    {
        defeatScreenAnim.SetBool(DEFEAT_FADER_VISIBLE, true);
    }

    #region PROPERTIES
    public static GameController Instance { get => instance; }
    #endregion
}
