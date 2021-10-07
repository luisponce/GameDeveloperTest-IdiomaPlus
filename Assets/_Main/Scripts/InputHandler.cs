using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour {

    //things to be set on the inspector
    #region REFERENCES
    public Controls controlsKeyBinds;
    #endregion

    private static InputHandler instance; //for the singleton implementation

    //delegates to allow other entities to subscribe to them for input events
    #region DELEGATES
    public delegate void ForwardMovement();
    public ForwardMovement OnForwardMovement;

    public delegate void LeftMovement();
    public LeftMovement OnLeftMovement;

    public delegate void BackwardsMovement();
    public BackwardsMovement OnBackwardsMovement;

    public delegate void RightMovement();
    public RightMovement OnRightMovement;

    public delegate void Attack();
    public Attack OnAttack;

    public delegate void PauseMenu();
    public PauseMenu OnOpenPauseMenu;

    #region hotbar
    //TODO: change to a array
    public delegate void Hotbar(int i);
    public Hotbar OnHotbar;
    #endregion
    #endregion

    void Awake()
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

    void Update()
    {
        //send the events acording to the inputs

        if (Input.GetKey(controlsKeyBinds.forwardMovementKC))
        {
            OnForwardMovement?.Invoke(); //if there is someone subscribed to the event, send the event.
        }

        if (Input.GetKey(controlsKeyBinds.leftMovementKC))
        {
            OnLeftMovement?.Invoke();
        }

        if (Input.GetKey(controlsKeyBinds.backMovementKC))
        {
            OnBackwardsMovement?.Invoke();
        }

        if (Input.GetKey(controlsKeyBinds.rightMovementKC))
        {
            OnRightMovement?.Invoke();
        }

        if (Input.GetKey(controlsKeyBinds.attackCK))
        {
            OnAttack?.Invoke();
        }

        if (Input.GetKey(controlsKeyBinds.pauseMenuKC))
        {
            OnOpenPauseMenu?.Invoke();
        }

        #region hotbar
        if (Input.GetKey(controlsKeyBinds.hotbar1))
        {
            OnHotbar?.Invoke(0);
        }

        if (Input.GetKey(controlsKeyBinds.hotbar2))
        {
            OnHotbar?.Invoke(1);
        }

        if (Input.GetKey(controlsKeyBinds.hotbar3))
        {
            OnHotbar?.Invoke(2);
        }

        if (Input.GetKey(controlsKeyBinds.hotbar4))
        {
            OnHotbar?.Invoke(3);
        }

        if (Input.GetKey(controlsKeyBinds.hotbar5))
        {
            OnHotbar?.Invoke(4);
        }
        #endregion
    }

    #region PROPERTIES
    public static InputHandler Instance { get => instance; }
    #endregion
}
