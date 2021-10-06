using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    private static PlayerControl instance; //for the singleton

    void Awake()
    {
        #region SINGLETON
        if (instance != null)
        {
            Destroy(gameObject); //Delete if another instance already exists and is assigned.
            return;
        }
        instance = this; //set this as the singleton
        DontDestroyOnLoad(gameObject); //to make it persist between levels
        #endregion
    }

    void Start()
    {
        InputHandler.Instance.OnForwardMovement += MoveForward;
        InputHandler.Instance.OnLeftMovement += MoveLeft;
        InputHandler.Instance.OnBackwardsMovement += MoveBackward;
        InputHandler.Instance.OnRightMovement += MoveRight;
        InputHandler.Instance.OnAttack += Attack;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Movement
    public void MoveForward()
    {
        //TODO: actually move
        Debug.Log("Forward");
    }

    public void MoveLeft()
    {
        //TODO: actually move
        Debug.Log("Left");
    }

    public void MoveBackward()
    {
        //TODO: actually move
        Debug.Log("Backward");
    }

    public void MoveRight()
    {
        //TODO: actually move
        Debug.Log("Right");
    }
    #endregion

    public void Attack()
    {
        //TODO: actually attack
        Debug.Log("attack");
    }

    #region PROPERTIES
    public static PlayerControl Instance { get => instance; set => instance = value; }
    #endregion
}
