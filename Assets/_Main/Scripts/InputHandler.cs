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

    
    #endregion

    void Awake()
    {
        #region SINGLETON
        if (Instance != null)
        {
            Destroy(gameObject); //Delete if another instance already exists and is assigned.
            return;
        }
        Instance = this; //set this as the singleton
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
    }

    #region PROPERTIES
    public static InputHandler Instance { get => instance; set => instance = value; }
    #endregion
}
