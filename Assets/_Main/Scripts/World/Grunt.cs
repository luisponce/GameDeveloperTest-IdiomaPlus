using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Grunt : MonoBehaviour
{
    #region REFERENCES
    public NavMeshAgent navAgent;

    public Image hpBarMask;
    #endregion

    #region combat variables
    private int health;
    private const int maxHealth = 20;

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
    private float reTargetChaseThreshold = 0.1f;
    #endregion

    #region patrol variables
    private float nextPatrolChange;
    private const float minTimeToChangePatrol = 1f;
    private const float maxTimeToChangePatrol = 5f;
    private float patrolMaxRange = 5f;
    #endregion

    private EGruntAIState aiState;

    void Start()
    {
        health = maxHealth;
        aiState = EGruntAIState.Idle;
        atkCharge = 0;
        chaseAnchor = transform.position;

        nextPatrolChange = 0;

        UpdateHpBar();
    }

    void Update()
    {
        #region ai FSM
        Vector3 distAnchor = PlayerControl.Instance.transform.position - chaseAnchor;
        Vector3 distGrunt = PlayerControl.Instance.transform.position - transform.position;
        switch (aiState)
        {
            case EGruntAIState.Idle:
                //check for player range
                if(distGrunt.magnitude < agroRange)
                {
                    Chase();
                }

                if(nextPatrolChange < Time.time)
                {
                    SetNextPatrolPath();
                    SetRandomNextPatrolTime();
                }

                break;

            case EGruntAIState.Chasing:
                //stop if player leaves or in range of attack
                if (distAnchor.magnitude > maxChaseRange)
                {
                    StopChase();
                } else {
                    if (distGrunt.magnitude < atkRange)
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
                    PlayerControl.Instance.DealDamage(atkDamage);
                    atkCharge = 0;
                }
                if(distGrunt.magnitude > atkRange)
                {
                    //out of range
                    if (distAnchor.magnitude < maxChaseRange) Chase();
                    else StopChase();
                }
                break;
        }
        #endregion
    }

    #region combat
    public void Damage(int dmg)
    {
        health -= dmg;
        UpdateHpBar();
        if (health <= 0)
        {
            health = 0;
            Die();
        }
    }

    public void UpdateHpBar()
    {
        hpBarMask.fillAmount = (float)health / (float)maxHealth;
    }

    public void Attack()
    {
        aiState = EGruntAIState.Attaking;
        navAgent.isStopped = true;
        Debug.Log("atk");
    }

    public void Die()
    {
        Destroy(gameObject);
    }
    #endregion

    #region state transitions
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
    #endregion


    private void SetRandomNextPatrolTime()
    {
        nextPatrolChange = Time.time + Random.Range(minTimeToChangePatrol, maxTimeToChangePatrol);
    }

    private void SetNextPatrolPath()
    {
        Vector3 anchorOffset = new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1));
        anchorOffset.Normalize();
        anchorOffset *= Random.Range(0, patrolMaxRange);
        navAgent.destination = chaseAnchor + anchorOffset;
    }
}
