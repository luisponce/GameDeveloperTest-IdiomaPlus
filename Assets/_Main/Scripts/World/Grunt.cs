using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Grunt : MonoBehaviour
{
    #region REFERENCES
    public NavMeshAgent navAgent;
    #endregion

    #region combat variables
    private int health;

    [SerializeField]
    private int atkDamage = 2;
    [SerializeField]
    private float atkRange = 1.5f;
    [SerializeField]
    private float atkSpeed = 2;
    private float atkCharge;

    #endregion

    #region agro variables
    [SerializeField]
    private float agroRange = 7;
    [SerializeField]
    private float maxChaseRange = 10;

    private Vector3 chaseAnchor;
    private Vector3 dist;
    private float reTargetChaseThreshold = 0.1f;
    #endregion

    private EGruntAIState aiState;

    void Start()
    {
        health = 20;
        aiState = EGruntAIState.Idle;
        atkCharge = 0;
        chaseAnchor = transform.position;
    }

    void Update()
    {
        dist = PlayerControl.Instance.transform.position - chaseAnchor;
        switch (aiState)
        {
            case EGruntAIState.Idle:
                //check for player vision and range
                if(dist.magnitude < agroRange)
                {
                    Chase();
                }
                break;

            case EGruntAIState.Chasing:
                //stop if player leaves or in range of attack
                if (dist.magnitude > maxChaseRange)
                {
                    StopChase();
                } else {
                    if ((transform.position - PlayerControl.Instance.transform.position).magnitude < atkRange)
                    {
                        Attack();
                    } 
                    //re-target when chasing and player moves
                    if ((PlayerControl.Instance.transform.position - navAgent.destination).magnitude > reTargetChaseThreshold)
                    {
                        Chase();
                    }
                }

                
                break;

            case EGruntAIState.Walking:
                if (navAgent.remainingDistance < navAgent.stoppingDistance)
                {
                    aiState = EGruntAIState.Idle;
                }
                break;

            case EGruntAIState.dead:
                break;

            case EGruntAIState.Attaking:
                atkCharge += Time.deltaTime;
                if(atkCharge >= atkSpeed)
                {
                    PlayerControl.Instance.Damage(atkDamage);
                    atkCharge = 0;
                }
                if((PlayerControl.Instance.transform.position - transform.position).magnitude > atkRange)
                {
                    //out of range
                    if (dist.magnitude < maxChaseRange) Chase();
                    else StopChase();
                }
                break;
        }
    }

    public void Damage(int dmg)
    {
        health -= dmg;
        if(health <= 0)
        {
            health = 0;
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void Chase()
    {
        aiState = EGruntAIState.Chasing;
        navAgent.SetDestination(PlayerControl.Instance.transform.position);
        navAgent.isStopped = false;
        
    }

    public void StopChase()
    {
        aiState = EGruntAIState.Walking;
        navAgent.SetDestination(chaseAnchor);
        navAgent.isStopped = false;
    }

    public void Attack()
    {
        aiState = EGruntAIState.Attaking;
        navAgent.isStopped = true;
        Debug.Log("atk");
    }
}
