using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    private bool isGamePaused = false;

    public Canvas pauseMenu;

    private float pauseDelay = 0.5f;
    private float lastPause;

    private static PauseController instance;

    private void Awake()
    {
        #region SINGLETON
        if (Instance != null)
        {
            Destroy(gameObject); //Delete if another instance already exists and is assigned.
            return;
        }
        instance = this; //set this as the singleton
        DontDestroyOnLoad(gameObject); //to make it persist between levels
        #endregion
    }

    // Start is called before the first frame update
    void Start()
    {
        InputHandler.Instance.OnPauseMenu += TogglePauseMenu;
        lastPause = 0;
    }

    public void TogglePauseMenu()
    {
        if(lastPause + pauseDelay < Time.unscaledTime)
        {
            if (isGamePaused)
            {
                isGamePaused = false;
                Time.timeScale = 1;

                pauseMenu.enabled = false;
            }
            else
            {
                isGamePaused = true;
                Time.timeScale = 0;
                

                pauseMenu.enabled = true;
            }
            lastPause = Time.unscaledTime;
        }
    }

    public void ForceUnpause()
    {
        if (isGamePaused)
        {
            isGamePaused = false;
            Time.timeScale = 1;

            pauseMenu.enabled = false;
        }
    }

    #region PROPERTIES
    public static PauseController Instance { get => instance; }
    public bool IsGamePaused { get => isGamePaused; }
    #endregion
}
