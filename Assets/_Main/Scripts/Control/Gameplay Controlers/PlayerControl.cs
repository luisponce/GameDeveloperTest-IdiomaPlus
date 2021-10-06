using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    private static PlayerControl instance; //for the singleton

    public CharacterController heroController;
    public Transform characterModel;

    #region Movement Variables
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
    #endregion

    #region Combat variables
    private float attackSpherecastRadius = 0.5f;
    [SerializeField]
    private float attackReach = 1.5f;

    [SerializeField]
    private int damage = 5;
    [SerializeField]
    private float atkSpeed = 1.5f;
    private float lastAtkTime;
    #endregion

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
            characterModel.LookAt(characterModel.transform.position + curSpeed);
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
        moveDir += CameraControl.Instance.transform.forward;
        moveDir.y = 0;
        moveDir.Normalize();
        isGainingSpeed = true;
    }

    public void MoveLeft()
    {
        moveDir -= CameraControl.Instance.transform.right;
        moveDir.y = 0;
        moveDir.Normalize();
        isGainingSpeed = true;
    }

    public void MoveBackward()
    {
        moveDir -= CameraControl.Instance.transform.forward;
        moveDir.y = 0;
        moveDir.Normalize();
        isGainingSpeed = true;
    }

    public void MoveRight()
    {
        moveDir += CameraControl.Instance.transform.right;
        moveDir.y = 0;
        moveDir.Normalize();
        isGainingSpeed = true;
    }
    #endregion

    public void Attack()
    {
        if(lastAtkTime + atkSpeed < Time.time)
        {
            Debug.Log("attack");
            if (Physics.SphereCast(characterModel.position + heroController.center, attackSpherecastRadius, characterModel.forward, out RaycastHit hit, attackReach))
            {
                Debug.Log("hit");
                Grunt grunt = hit.collider.GetComponent<Grunt>();
                if (grunt != null)
                {
                    grunt.Damage(damage);
                }
            }

            lastAtkTime = Time.time;
        }
    }

    #region PROPERTIES
    public static PlayerControl Instance { get => instance; set => instance = value; }
    #endregion
}
