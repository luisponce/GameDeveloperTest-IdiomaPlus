using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    private static PlayerControl instance; //for the singleton

    [SerializeField]
    private CharacterController heroController;

    [SerializeField]
    private float maxMoveSpeed = 7f;
    private Vector3 curSpeed = new Vector3(0,0);

    [SerializeField]
    private float movementAceleration = 0.07f;
    [SerializeField]
    private float movementDeaceleration = 0.05f;
    [SerializeField]
    private float movementClamp = 0.1f;

    private Vector3 moveDir = new Vector3(0, 0);

    private bool isGainingSpeed = false;

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

    void Update()
    {
        if (isGainingSpeed)
        {
            curSpeed += movementAceleration * moveDir;
            if (curSpeed.magnitude >= maxMoveSpeed)
            {
                curSpeed = curSpeed.normalized * maxMoveSpeed;
            }
            isGainingSpeed = false;
        } else
        {
            if(curSpeed.magnitude > movementClamp)
            {
                curSpeed += movementDeaceleration * -curSpeed.normalized;
            }
        }

        if(curSpeed.magnitude > movementClamp) heroController.Move(curSpeed * Time.deltaTime);
    }

    #region Movement Events
    public void MoveForward()
    {
        moveDir += Vector3.forward;
        moveDir.Normalize();
        isGainingSpeed = true;
    }

    public void MoveLeft()
    {
        moveDir += Vector3.left;
        moveDir.Normalize();
        isGainingSpeed = true;
    }

    public void MoveBackward()
    {
        moveDir += Vector3.back;
        moveDir.Normalize();
        isGainingSpeed = true;
    }

    public void MoveRight()
    {
        moveDir += Vector3.right;
        moveDir.Normalize();
        isGainingSpeed = true;
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
